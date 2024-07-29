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
/// 供应商服务
/// </summary>
[ApiDescriptionSettings(PaichanConst.GroupName, Order = 100)]
public class SupplierService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<Supplier> _rep;
    public SupplierService(SqlSugarRepository<Supplier> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询供应商
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<SupplierOutput>> Page(SupplierInput input)
    {
        var query= _rep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.SupplierCode.Contains(input.SearchKey.Trim())
                || u.SupplierName.Contains(input.SearchKey.Trim())
                || u.Surname.Contains(input.SearchKey.Trim())
                || u.Mobile.Contains(input.SearchKey.Trim())
            )
            .WhereIF(!string.IsNullOrWhiteSpace(input.SupplierCode), u => u.SupplierCode.Contains(input.SupplierCode.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.SupplierName), u => u.SupplierName.Contains(input.SupplierName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Surname), u => u.Surname.Contains(input.Surname.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Mobile), u => u.Mobile.Contains(input.Mobile.Trim()))
            .Select<SupplierOutput>()
;
        query = query.OrderBuilder(input, "", "CreateTime");
        return await query.ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加供应商
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task Add(AddSupplierInput input)
    {
        var entity = input.Adapt<Supplier>();
        await _rep.InsertAsync(entity);
    }

    /// <summary>
    /// 删除供应商
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteSupplierInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _rep.FakeDeleteAsync(entity);   //假删除
        //await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新供应商
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateSupplierInput input)
    {
        var entity = input.Adapt<Supplier>();
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取供应商
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<Supplier> Get([FromQuery] QueryByIdSupplierInput input)
    {
        return await _rep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 获取供应商列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<SupplierOutput>> List([FromQuery] SupplierInput input)
    {
        return await _rep.AsQueryable().Select<SupplierOutput>().ToListAsync();
    }





}

