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
/// 物料服务
/// </summary>
[ApiDescriptionSettings(PaichanConst.GroupName, Order = 100)]
public class MaterialService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<Material> _rep;
    public MaterialService(SqlSugarRepository<Material> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询物料
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<MaterialOutput>> Page(MaterialInput input)
    {
        var query= _rep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.MaterialCode.Contains(input.SearchKey.Trim())
                || u.MaterialName.Contains(input.SearchKey.Trim())
            )
            .WhereIF(input.MaterialTypeId>0, u => u.MaterialTypeId == input.MaterialTypeId)
            .WhereIF(!string.IsNullOrWhiteSpace(input.MaterialCode), u => u.MaterialCode.Contains(input.MaterialCode.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.MaterialName), u => u.MaterialName.Contains(input.MaterialName.Trim()))
            .WhereIF(input.SupplierId>0, u => u.SupplierId == input.SupplierId)
            //处理外键和TreeSelector相关字段的连接
            .LeftJoin<MaterialType>((u, materialtypeid) => u.MaterialTypeId == materialtypeid.Id )
            .LeftJoin<Supplier>((u, materialtypeid, supplierid) => u.SupplierId == supplierid.Id )
            .Select((u, materialtypeid, supplierid)=> new MaterialOutput{
                Id = u.Id, 
                MaterialTypeId = u.MaterialTypeId, 
                MaterialTypeIdMaterialTypeName = materialtypeid.MaterialTypeName,
                MaterialCode = u.MaterialCode, 
                MaterialName = u.MaterialName, 
                MaterialNorm = u.MaterialNorm, 
                CostPrice = u.CostPrice, 
                DutyRate = u.DutyRate, 
                SupplierId = u.SupplierId, 
                SupplierIdSupplierName = supplierid.SupplierName,
                SafetyStock = u.SafetyStock, 
                Sort = u.Sort, 
                Remark = u.Remark, 
                CreateUserName = u.CreateUserName, 
                UpdateUserName = u.UpdateUserName, 
            })
;
        query = query.OrderBuilder(input, "u.", "CreateTime");
        return await query.ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加物料
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task Add(AddMaterialInput input)
    {
        var entity = input.Adapt<Material>();
        await _rep.InsertAsync(entity);
    }

    /// <summary>
    /// 删除物料
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteMaterialInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _rep.FakeDeleteAsync(entity);   //假删除
        //await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新物料
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateMaterialInput input)
    {
        var entity = input.Adapt<Material>();
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取物料
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<Material> Get([FromQuery] QueryByIdMaterialInput input)
    {
        return await _rep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 获取物料列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<MaterialOutput>> List([FromQuery] MaterialInput input)
    {
        return await _rep.AsQueryable().Select<MaterialOutput>().ToListAsync();
    }

    /// <summary>
    /// 获取物料类型列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "MaterialTypeMaterialTypeIdDropdown"), HttpGet]
    public async Task<dynamic> MaterialTypeMaterialTypeIdDropdown()
    {
        return await _rep.Context.Queryable<MaterialType>()
                .Select(u => new
                {
                    Label = u.MaterialTypeName,
                    Value = u.Id
                }
                ).ToListAsync();
    }
    /// <summary>
    /// 获取供应商列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "SupplierSupplierIdDropdown"), HttpGet]
    public async Task<dynamic> SupplierSupplierIdDropdown()
    {
        return await _rep.Context.Queryable<Supplier>()
                .Select(u => new
                {
                    Label = u.SupplierName,
                    Value = u.Id
                }
                ).ToListAsync();
    }




}

