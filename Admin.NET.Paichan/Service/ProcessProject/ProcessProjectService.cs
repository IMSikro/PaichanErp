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
/// 工艺标准项服务
/// </summary>
[ApiDescriptionSettings(PaichanConst.GroupName, Order = 100)]
public class ProcessProjectService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<ProcessProject> _rep;
    public ProcessProjectService(SqlSugarRepository<ProcessProject> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询工艺标准项
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<ProcessProjectOutput>> Page(ProcessProjectInput input)
    {
        var query= _rep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.ProcessProjectCode.Contains(input.SearchKey.Trim())
                || u.ProcessProjectName.Contains(input.SearchKey.Trim())
            )
            .WhereIF(!string.IsNullOrWhiteSpace(input.ProcessProjectCode), u => u.ProcessProjectCode.Contains(input.ProcessProjectCode.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.ProcessProjectName), u => u.ProcessProjectName.Contains(input.ProcessProjectName.Trim()))
            .Select<ProcessProjectOutput>()
;
        query = query.OrderBuilder(input, "", "CreateTime");
        return await query.ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加工艺标准项
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task Add(AddProcessProjectInput input)
    {
        var entity = input.Adapt<ProcessProject>();
        await _rep.InsertAsync(entity);
    }

    /// <summary>
    /// 删除工艺标准项
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteProcessProjectInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _rep.FakeDeleteAsync(entity);   //假删除
        //await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新工艺标准项
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateProcessProjectInput input)
    {
        var entity = input.Adapt<ProcessProject>();
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取工艺标准项
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<ProcessProject> Get([FromQuery] QueryByIdProcessProjectInput input)
    {
        return await _rep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 获取工艺标准项列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<ProcessProjectOutput>> List([FromQuery] ProcessProjectInput input)
    {
        return await _rep.AsQueryable().Select<ProcessProjectOutput>().ToListAsync();
    }





}

