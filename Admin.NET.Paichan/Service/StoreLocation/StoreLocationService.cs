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
/// 库位管理服务
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
    /// 分页查询库位管理
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
        query = query.OrderBuilder(input, "", "CreateTime");
        return await query.ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加库位管理
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
    /// 删除库位管理
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
    /// 更新库位管理
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
    /// 获取库位管理
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
    /// 获取库位管理列表
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

