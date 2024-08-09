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
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Admin.NET.Paichan;
/// <summary>
/// 库位服务
/// </summary>
[ApiDescriptionSettings(PaichanConst.GroupName, Order = 100)]
public class StoreLocationService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<StoreLocation> _rep;
    public StoreLocationService(SqlSugarRepository<StoreLocation> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询库位
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<StoreLocationOutput>> Page(StoreLocationInput input)
    {
        var query= _rep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.StoreLocationCode.Contains(input.SearchKey.Trim())
                || u.StoreLocationName.Contains(input.SearchKey.Trim())
            )
            .WhereIF(!string.IsNullOrWhiteSpace(input.StoreLocationCode), u => u.StoreLocationCode.Contains(input.StoreLocationCode.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.StoreLocationName), u => u.StoreLocationName.Contains(input.StoreLocationName.Trim()))
            .WhereIF(input.StoreId>0, u => u.StoreId == input.StoreId)
            .Select<StoreLocationOutput>()
;
        query = query.OrderBuilder(input, "", "CreateTime", false);
        return await query.ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加库位
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task Add(AddStoreLocationInput input)
    {
        var entity = input.Adapt<StoreLocation>();
        await _rep.InsertAsync(entity);
    }

    /// <summary>
    /// 删除库位
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteStoreLocationInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _rep.FakeDeleteAsync(entity);   //假删除
        //await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新库位
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateStoreLocationInput input)
    {
        var entity = input.Adapt<StoreLocation>();
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
    }




    /// <summary>
    /// 获取库位列表导入模板
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "GetStoreLocationTempExcel")]
    public async Task<IActionResult> GetStoreLocationTempExcel()
    {
        var fileName = "库位列表导入模板.xlsx";
        var filePath = Path.Combine(App.WebHostEnvironment.WebRootPath, "Excel", "Temp");
        if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
        IImporter importer = new ExcelImporter();
        var res = await importer.GenerateTemplate<StoreLocationDto>(Path.Combine(filePath, fileName));
        return new FileStreamResult(new FileStream(res.FileName, FileMode.OpenOrCreate), "application/octet-stream") { FileDownloadName = fileName };
    }

    /// <summary>
    /// 上传Excel导入库位
    /// </summary>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "ImportStoreLocationExcel")]
    public async Task ImportStoreLocationExcel([Required] IFormFile file, [Required(ErrorMessage = "仓库Id不能为空")] long storeId)
    {
        var newFile = await App.GetRequiredService<SysFileService>().UploadFile(file, "Excel/Import");
        var filePath = Path.Combine(App.WebHostEnvironment.WebRootPath, newFile.FilePath, newFile.Name);
        IImporter importer = new ExcelImporter();
        var res = await importer.Import<StoreLocationDto>(filePath);

        var storeLocationDtos = res.Data;
        foreach (var storeLocationDto in storeLocationDtos)
        {
            var storeLocation = storeLocationDto.Adapt<StoreLocation>();
            var store = await _rep.Context.Queryable<Store>()
                .FirstAsync(d => !d.IsDelete && d.Id == storeId);

            if (store != null)
            {
                storeLocation.StoreId = store.Id;
                storeLocation.StoreCode = store.StoreCode;
                storeLocation.StoreName = store.StoreName;
            }

            await _rep.InsertAsync(storeLocation);
        }

        await App.GetRequiredService<SysFileService>().DeleteFile(new DeleteFileInput { Id = newFile.Id });
    }

    /// <summary>
    /// 获取库位
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<StoreLocation> Get([FromQuery] QueryByIdStoreLocationInput input)
    {
        return await _rep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 获取库位列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<StoreLocationOutput>> List([FromQuery] StoreLocationInput input)
    {
        return await _rep.AsQueryable().Select<StoreLocationOutput>().ToListAsync();
    }





}

