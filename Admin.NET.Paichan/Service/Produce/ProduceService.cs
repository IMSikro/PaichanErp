using Admin.NET.Core;
using Admin.NET.Paichan.Const;
using Admin.NET.Paichan.Entity;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Mapster;
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
            .Select((u, producetype) => new ProduceOutput
            {
                Id = u.Id,
                ProduceType = u.ProduceType,
                ProduceTypeTypeName = producetype.TypeName,
                ProduceCode = u.ProduceCode,
                ProduceName = u.ProduceName,
                ColorLab = u.ColorLab,
                ColorRgb = u.ColorRgb,
                IsMix = u.IsMix,
                IsExtrusion = u.IsExtrusion,
                IsMill = u.IsMill,
                ProduceCoefficient = u.ProduceCoefficient,
                ProduceSeries = u.ProduceSeries,
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
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "ProduceTypeProduceTypeDropdown"), HttpGet]
    public async Task<dynamic> ProduceTypeProduceTypeDropdown()
    {
        return await _rep.Context.Queryable<ProduceType>().Where(u => !u.IsDelete)
                .Select(u => new
                {
                    Label = u.TypeName,
                    Value = u.Id
                }
                ).ToListAsync();
    }




}

