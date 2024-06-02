using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using Admin.NET.Core.Service;
using Admin.NET.Paichan.Const;
using Admin.NET.Paichan.Entity;
using Furion;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Admin.NET.Paichan;
/// <summary>
/// 产品列表服务
/// </summary>
[ApiDescriptionSettings(PaichanConst.GroupName, Order = 100)]
public class ProduceService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<Produce> _rep;
    public ProduceService(SqlSugarRepository<Produce> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询产品列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<ProduceOutput>> Page(ProduceInput input)
    {
        var query = _rep.AsQueryable().Where(u => !u.IsDelete)
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.ProduceCode.Contains(input.SearchKey.Trim())
                || u.ProduceName.Contains(input.SearchKey.Trim())
            )
            .WhereIF(input.ProduceType > 0, u => u.ProduceType == input.ProduceType)
            .WhereIF(!string.IsNullOrWhiteSpace(input.ProduceCode), u => u.ProduceCode.Contains(input.ProduceCode.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.ProduceName), u => u.ProduceName.Contains(input.ProduceName.Trim()))
            //处理外键和TreeSelector相关字段的连接
            .LeftJoin<ProduceType>((u, producetype) => u.ProduceType == producetype.Id)
            .LeftJoin<SystemUnit>((u, producetype,systemunit) => u.UnitId == systemunit.Id)
            .Select((u, producetype,systemunit) => new ProduceOutput
            {
                Id = u.Id,
                ProduceType = u.ProduceType,
                ProduceTypeTypeName = producetype.TypeName,
                ProduceCode = u.ProduceCode,
                ProduceName = u.ProduceName,
                ColorLab = u.ColorLab,
                ColorRgb = u.ColorRgb,
                DeviceTypes = u.DeviceTypes,
                ProduceCoefficient = u.ProduceCoefficient,
                ProduceSeries = u.ProduceSeries,
                UnitId = u.UnitId,
                pUnit = systemunit.UnitName,
                Remark = u.Remark,
                CreateUserName = u.CreateUserName,
                UpdateUserName = u.UpdateUserName,
            })
;
        query = query.OrderBuilder(input, "u.", "CreateTime", false);
        return await query.ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加产品列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task Add(AddProduceInput input)
    {
        var entity = input.Adapt<Produce>();
        await _rep.InsertAsync(entity);
    }

    /// <summary>
    /// 删除产品列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteProduceInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _rep.FakeDeleteAsync(entity);   //假删除
        //await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新产品列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateProduceInput input)
    {
        var entity = input.Adapt<Produce>();
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
    }


    /// <summary>
    /// 获取产品列表导入模板
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "GetProduceTempExcel")]
    public async Task<IActionResult> GetProduceTempExcel()
    {
        var fileName = "产品列表导入模板.xlsx";
        var filePath = Path.Combine(App.WebHostEnvironment.WebRootPath, "Excel", "Temp");
        if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
        IImporter importer = new ExcelImporter();
        var res = await importer.GenerateTemplate<ProduceDto>(Path.Combine(filePath, fileName));
        return new FileStreamResult(new FileStream(res.FileName, FileMode.OpenOrCreate), "application/octet-stream") { FileDownloadName = fileName };
    }

    /// <summary>
    /// 上传Excel导入产品
    /// </summary>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "ImportProduceExcel")]
    public async Task ImportProduceExcel([Required] IFormFile file)
    {
        var newFile = await App.GetRequiredService<SysFileService>().UploadFile(file, "Excel/Import");
        var filePath = Path.Combine(App.WebHostEnvironment.WebRootPath, newFile.FilePath, newFile.Name);
        IImporter importer = new ExcelImporter();
        var res = await importer.Import<ProduceDto>(filePath);

        var produceDtos = res.Data;
        foreach (var produceDto in produceDtos)
        {
            var produce = produceDto.Adapt<Produce>();
            var produceType = await _rep.Context.Queryable<ProduceType>()
                .FirstAsync(d => !d.IsDelete && d.TypeName == produceDto.ProduceTypeName);

            produce.ColorRgb = produce.ColorLab.LabToRgb();

            produce.ProduceType = produceType?.Id ?? 0;
            produce.ProduceSeries = produceType?.ProduceSeries ?? "";

            produce.UnitId = (await _rep.Context.Queryable<SystemUnit>()
                .FirstAsync(su => !su.IsDelete && su.UnitName == produce.pUnit))?.Id;

            var dtList = produceDto.DeviceTypes.Split(new[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
            var dtIds = _rep.Context.Queryable<DeviceType>()
                .Where(dt => !dt.IsDelete && dtList.Contains(dt.TypeName, true));
            produce.DeviceTypes = string.Join(',', await dtIds.Select(dt => dt.Id).ToListAsync());

            await _rep.InsertAsync(produce);
        }

        await App.GetRequiredService<SysFileService>().DeleteFile(new DeleteFileInput { Id = newFile.Id });
    }

    /// <summary>
    /// 获取产品列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<Produce> Get([FromQuery] QueryByIdProduceInput input)
    {
        return await _rep.GetFirstAsync(u => !u.IsDelete && u.Id == input.Id);
    }

    /// <summary>
    /// 获取产品列表列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<ProduceOutput>> List([FromQuery] ProduceInput input)
    {
        return await _rep.AsQueryable()
            .Where(u => !u.IsDelete).Select<ProduceOutput>().ToListAsync();
    }

    /// <summary>
    /// 获取产品类型列表
    /// </summary>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "ProduceTypeProduceTypeDropdown"), HttpGet]
    public async Task<dynamic> ProduceTypeProduceTypeDropdown()
    {
        return await _rep.Context.Queryable<ProduceType>().Where(u => !u.IsDelete)
                .Select(u => new
                {
                    Label = u.TypeName,
                    Value = u.Id,
                    u.ProduceSeries,
                }).ToListAsync();
    }

    /// <summary>
    /// 获取计量单位列表
    /// </summary>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "SystemUnitDropdown"), HttpGet]
    public async Task<dynamic> SystemUnitDropdown()
    {
        return await _rep.Context.Queryable<SystemUnit>().Where(u => !u.IsDelete)
                .Select(u => new
                {
                    Label = u.UnitName,
                    Value = u.Id,
                }).ToListAsync();
    }
}

