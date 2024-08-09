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
/// 产品配方服务
/// </summary>
[ApiDescriptionSettings(PaichanConst.GroupName, Order = 100)]
public class ProduceFormulaService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<ProduceFormula> _rep;
    public ProduceFormulaService(SqlSugarRepository<ProduceFormula> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询产品配方
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<ProduceFormulaOutput>> Page(ProduceFormulaInput input)
    {
        var query= _rep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.ProduceFormulaCode.Contains(input.SearchKey.Trim())
                || u.ProduceFormulaName.Contains(input.SearchKey.Trim())
                || u.FormulaVersion.Contains(input.SearchKey.Trim())
            )
            .WhereIF(!string.IsNullOrWhiteSpace(input.ProduceFormulaCode), u => u.ProduceFormulaCode.Contains(input.ProduceFormulaCode.Trim()))
            .WhereIF(input.ProduceId>0, u => u.ProduceId == input.ProduceId)
            .WhereIF(!string.IsNullOrWhiteSpace(input.ProduceFormulaName), u => u.ProduceFormulaName.Contains(input.ProduceFormulaName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.FormulaVersion), u => u.FormulaVersion.Contains(input.FormulaVersion.Trim()))
            //处理外键和TreeSelector相关字段的连接
            .LeftJoin<Produce>((u, produceid) => u.ProduceId == produceid.Id )
            .Select((u, produceid)=> new ProduceFormulaOutput{
                Id = u.Id, 
                ProduceFormulaCode = u.ProduceFormulaCode, 
                ProduceFormulaName = u.ProduceFormulaName, 
                ProduceId = u.ProduceId, 
                ProduceIdProduceCode = produceid.ProduceCode,
                ProduceCode = produceid.ProduceCode,
                ProduceName = produceid.ProduceName,
                FormulaVersion = u.FormulaVersion, 
                IsEnable = u.IsEnable, 
                FormulaCosts = u.FormulaCosts, 
                Remark = u.Remark, 
                CreateUserName = u.CreateUserName, 
                UpdateUserName = u.UpdateUserName, 
            })
;
        query = query.OrderBuilder(input, "u.", "CreateTime");
        return await query.ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加产品配方
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task Add(AddProduceFormulaInput input)
    {
        var entity = input.Adapt<ProduceFormula>();
        await _rep.InsertAsync(entity);
    }

    /// <summary>
    /// 删除产品配方
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteProduceFormulaInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _rep.FakeDeleteAsync(entity);   //假删除
        //await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新产品配方
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateProduceFormulaInput input)
    {
        var entity = input.Adapt<ProduceFormula>();
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取产品配方
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<ProduceFormula> Get([FromQuery] QueryByIdProduceFormulaInput input)
    {
        return await _rep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 获取产品配方列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<ProduceFormulaOutput>> List([FromQuery] ProduceFormulaInput input)
    {
        return await _rep.AsQueryable().Select<ProduceFormulaOutput>().ToListAsync();
    }

    /// <summary>
    /// 获取产品外键列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "ProduceProduceIdDropdown"), HttpGet]
    public async Task<dynamic> ProduceProduceIdDropdown()
    {
        return await _rep.Context.Queryable<Produce>()
                .Select(u => new
                {
                    Label = u.ProduceCode,
                    Value = u.Id
                }
                ).ToListAsync();
    }




}

