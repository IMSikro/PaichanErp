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
/// 检验标准项服务
/// </summary>
[ApiDescriptionSettings(PaichanConst.GroupName, Order = 100)]
public class ExamineProjectService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<ExamineProject> _rep;
    public ExamineProjectService(SqlSugarRepository<ExamineProject> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询检验标准项
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<ExamineProjectOutput>> Page(ExamineProjectInput input)
    {
        var query= _rep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.ExamineProjectCode.Contains(input.SearchKey.Trim())
                || u.ExamineProjectName.Contains(input.SearchKey.Trim())
                || u.Remark.Contains(input.SearchKey.Trim())
                || u.CreateUserName.Contains(input.SearchKey.Trim())
                || u.UpdateUserName.Contains(input.SearchKey.Trim())
            )
            .WhereIF(!string.IsNullOrWhiteSpace(input.ExamineProjectCode), u => u.ExamineProjectCode.Contains(input.ExamineProjectCode.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.ExamineProjectName), u => u.ExamineProjectName.Contains(input.ExamineProjectName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Remark), u => u.Remark.Contains(input.Remark.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.CreateUserName), u => u.CreateUserName.Contains(input.CreateUserName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.UpdateUserName), u => u.UpdateUserName.Contains(input.UpdateUserName.Trim()))
            .Select<ExamineProjectOutput>()
;
        query = query.OrderBuilder(input, "", "CreateTime");
        return await query.ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加检验标准项
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task Add(AddExamineProjectInput input)
    {
        var entity = input.Adapt<ExamineProject>();
        await _rep.InsertAsync(entity);
    }

    /// <summary>
    /// 删除检验标准项
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteExamineProjectInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _rep.FakeDeleteAsync(entity);   //假删除
        //await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新检验标准项
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateExamineProjectInput input)
    {
        var entity = input.Adapt<ExamineProject>();
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取检验标准项
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<ExamineProject> Get([FromQuery] QueryByIdExamineProjectInput input)
    {
        return await _rep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 获取检验标准项列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<ExamineProjectOutput>> List([FromQuery] ExamineProjectInput input)
    {
        return await _rep.AsQueryable().Select<ExamineProjectOutput>().ToListAsync();
    }





}

