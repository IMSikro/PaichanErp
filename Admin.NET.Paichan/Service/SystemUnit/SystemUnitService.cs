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
/// 计量单位服务
/// </summary>
[ApiDescriptionSettings(PaichanConst.GroupName, Order = 100)]
public class SystemUnitService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<SystemUnit> _rep;
    public SystemUnitService(SqlSugarRepository<SystemUnit> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询计量单位
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<SystemUnitOutput>> Page(SystemUnitInput input)
    {
        var query= _rep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.UnitName.Contains(input.SearchKey.Trim())
            )
            .WhereIF(!string.IsNullOrWhiteSpace(input.UnitName), u => u.UnitName.Contains(input.UnitName.Trim()))
            .Select<SystemUnitOutput>()
;
        query = query.OrderBuilder(input, "", "CreateTime");
        return await query.ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加计量单位
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task Add(AddSystemUnitInput input)
    {
        var entity = input.Adapt<SystemUnit>();
        await _rep.InsertAsync(entity);
    }

    /// <summary>
    /// 删除计量单位
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteSystemUnitInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _rep.FakeDeleteAsync(entity);   //假删除
        //await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新计量单位
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateSystemUnitInput input)
    {
        var entity = input.Adapt<SystemUnit>();
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true, ignoreAllDefaultValue: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取计量单位
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<SystemUnit> Get([FromQuery] QueryByIdSystemUnitInput input)
    {
        return await _rep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 获取计量单位列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<SystemUnitOutput>> List([FromQuery] SystemUnitInput input)
    {
        return await _rep.AsQueryable().Select<SystemUnitOutput>().ToListAsync();
    }





}

