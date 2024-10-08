﻿using Admin.NET.Core;
using Admin.NET.Paichan.Const;
using Admin.NET.Paichan.Entity;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Admin.NET.Paichan;
/// <summary>
/// 产品类型服务
/// </summary>
[ApiDescriptionSettings(PaichanConst.GroupName, Order = 100)]
public class ProduceTypeService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<ProduceType> _rep;
    public ProduceTypeService(SqlSugarRepository<ProduceType> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询产品类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<ProduceTypeOutput>> Page(ProduceTypeInput input)
    {
        var query= _rep.AsQueryable().Where(u => !u.IsDelete)
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.TypeName.Contains(input.SearchKey.Trim())
            )
            .WhereIF(!string.IsNullOrWhiteSpace(input.TypeName), u => u.TypeName.Contains(input.TypeName.Trim()))
            .Select<ProduceTypeOutput>()
;
        query = query.OrderBuilder(input, "", "CreateTime", false);
        return await query.ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加产品类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task Add(AddProduceTypeInput input)
    {
        var entity = input.Adapt<ProduceType>();
        await _rep.InsertAsync(entity);
    }

    /// <summary>
    /// 删除产品类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteProduceTypeInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _rep.FakeDeleteAsync(entity);   //假删除
        //await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新产品类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateProduceTypeInput input)
    {
        var entity = input.Adapt<ProduceType>();
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取产品类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<ProduceType> Get([FromQuery] QueryByIdProduceTypeInput input)
    {
        return await _rep.GetFirstAsync(u => !u.IsDelete && u.Id == input.Id);
    }

    /// <summary>
    /// 获取产品类型列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<ProduceTypeOutput>> List([FromQuery] ProduceTypeInput input)
    {
        return await _rep.AsQueryable().Where(u => !u.IsDelete).Select<ProduceTypeOutput>().ToListAsync();
    }





}

