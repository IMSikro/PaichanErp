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
/// 配方物料服务
/// </summary>
[ApiDescriptionSettings(PaichanConst.GroupName, Order = 100)]
public class ProduceFormulaMaterialService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<ProduceFormulaMaterial> _rep;
    public ProduceFormulaMaterialService(SqlSugarRepository<ProduceFormulaMaterial> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询配方物料
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<ProduceFormulaMaterialOutput>> Page(ProduceFormulaMaterialInput input)
    {
        var query= _rep.AsQueryable()
            .WhereIF(input.ProduceFormulaId>0, u => u.ProduceFormulaId == input.ProduceFormulaId)
            .WhereIF(input.MaterialId>0, u => u.MaterialId == input.MaterialId)
            //处理外键和TreeSelector相关字段的连接
            .LeftJoin<ProduceFormula>((u, produceformulaid) => u.ProduceFormulaId == produceformulaid.Id )
            .LeftJoin<Material>((u, produceformulaid, materialid) => u.MaterialId == materialid.Id )
            .Select((u, produceformulaid, materialid)=> new ProduceFormulaMaterialOutput{
                Id = u.Id, 
                ProduceFormulaId = u.ProduceFormulaId, 
                ProduceFormulaIdProduceFormulaCode = produceformulaid.ProduceFormulaCode,
                MaterialId = u.MaterialId, 
                MaterialIdMaterialCode = materialid.MaterialCode,
                MaterialCode = u.MaterialCode, 
                MaterialName = u.MaterialName, 
                MaterialNorm = u.MaterialNorm, 
                CostPrice = u.CostPrice, 
                DutyRate = u.DutyRate, 
                Sort = u.Sort, 
                Remark = u.Remark, 
                CreateUserName = u.CreateUserName, 
                UpdateUserName = u.UpdateUserName, 
            })
;
        query = query.OrderBuilder(input, "", "CreateTime");
        return await query.ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加配方物料
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task Add(AddProduceFormulaMaterialInput input)
    {
        var entity = input.Adapt<ProduceFormulaMaterial>();
        await _rep.InsertAsync(entity);
    }

    /// <summary>
    /// 删除配方物料
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteProduceFormulaMaterialInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _rep.FakeDeleteAsync(entity);   //假删除
        //await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新配方物料
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateProduceFormulaMaterialInput input)
    {
        var entity = input.Adapt<ProduceFormulaMaterial>();
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取配方物料
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<ProduceFormulaMaterial> Get([FromQuery] QueryByIdProduceFormulaMaterialInput input)
    {
        return await _rep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 获取配方物料列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<ProduceFormulaMaterialOutput>> List([FromQuery] ProduceFormulaMaterialInput input)
    {
        return await _rep.AsQueryable().Select<ProduceFormulaMaterialOutput>().ToListAsync();
    }

    /// <summary>
    /// 获取配方列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "ProduceFormulaProduceFormulaIdDropdown"), HttpGet]
    public async Task<dynamic> ProduceFormulaProduceFormulaIdDropdown()
    {
        return await _rep.Context.Queryable<ProduceFormula>()
                .Select(u => new
                {
                    Label = u.ProduceFormulaCode,
                    Value = u.Id
                }
                ).ToListAsync();
    }
    /// <summary>
    /// 获取物料列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "MaterialMaterialIdDropdown"), HttpGet]
    public async Task<dynamic> MaterialMaterialIdDropdown()
    {
        return await _rep.Context.Queryable<Material>()
                .Select(u => new
                {
                    Label = u.MaterialCode,
                    Value = u.Id
                }
                ).ToListAsync();
    }




}

