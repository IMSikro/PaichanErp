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
/// 检验标准服务
/// </summary>
[ApiDescriptionSettings(PaichanConst.GroupName, Order = 100)]
public class ExamineStandardService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<ExamineStandard> _rep;
    public ExamineStandardService(SqlSugarRepository<ExamineStandard> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询检验标准
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<ExamineStandardOutput>> Page(ExamineStandardInput input)
    {
        var query= _rep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.ExamineStandardCode.Contains(input.SearchKey.Trim())
                || u.ExamineStandardName.Contains(input.SearchKey.Trim())
            )
            .WhereIF(!string.IsNullOrWhiteSpace(input.ExamineStandardCode), u => u.ExamineStandardCode.Contains(input.ExamineStandardCode.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.ExamineStandardName), u => u.ExamineStandardName.Contains(input.ExamineStandardName.Trim()))
            .WhereIF(input.ProduceId>0, u => u.ProduceId == input.ProduceId)
            .WhereIF(input.DeviceTypeId>0, u => u.DeviceTypeId == input.DeviceTypeId)
            .WhereIF(input.ExamineProjectId>0, u => u.ExamineProjectId == input.ExamineProjectId)
            //处理外键和TreeSelector相关字段的连接
            .LeftJoin<Produce>((u, produceid) => u.ProduceId == produceid.Id )
            .LeftJoin<DeviceType>((u, produceid, devicetypeid) => u.DeviceTypeId == devicetypeid.Id )
            .LeftJoin<ExamineProject>((u, produceid, devicetypeid, examineprojectid) => u.ExamineProjectId == examineprojectid.Id )
            .Select((u, produceid, devicetypeid, examineprojectid)=> new ExamineStandardOutput{
                Id = u.Id, 
                ExamineStandardCode = u.ExamineStandardCode, 
                ExamineStandardName = u.ExamineStandardName, 
                ProduceId = u.ProduceId, 
                ProduceIdProduceCode = produceid.ProduceCode,
                ProduceCode = u.ProduceCode, 
                ProduceName = u.ProduceName, 
                DeviceTypeId = u.DeviceTypeId, 
                DeviceTypeIdTypeName = devicetypeid.TypeName,
                DeviceTypeName = u.DeviceTypeName, 
                ExamineProjectId = u.ExamineProjectId, 
                ExamineProjectIdExamineProjectCode = examineprojectid.ExamineProjectCode,
                ExamineProjectCode = u.ExamineProjectCode, 
                ExamineProjectName = u.ExamineProjectName, 
                StandardValue = u.StandardValue, 
                Tolerance1 = u.Tolerance1, 
                Tolerance2 = u.Tolerance2, 
                Remark = u.Remark, 
                CreateUserName = u.CreateUserName, 
                UpdateUserName = u.UpdateUserName, 
            })
;
        query = query.OrderBuilder(input, "u.", "CreateTime");
        return await query.ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加检验标准
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task Add(AddExamineStandardInput input)
    {
        var entity = input.Adapt<ExamineStandard>();
        var project = await _rep.Context.Queryable<ExamineProject>().FirstAsync(wa => wa.Id == input.ExamineProjectId);
        entity.ExamineProjectCode = project.ExamineProjectCode;
        entity.ExamineProjectName = project.ExamineProjectName;
        var produce = await _rep.Context.Queryable<Produce>().FirstAsync(wa => wa.Id == input.ProduceId);
        entity.ProduceCode = produce.ProduceCode;
        entity.ProduceName = produce.ProduceName;
        var deviceType = await _rep.Context.Queryable<DeviceType>().FirstAsync(wa => wa.Id == input.DeviceTypeId);
        entity.DeviceTypeName = deviceType.TypeName;
        await _rep.InsertAsync(entity);
    }

    /// <summary>
    /// 删除检验标准
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteExamineStandardInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _rep.FakeDeleteAsync(entity);   //假删除
        //await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新检验标准
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateExamineStandardInput input)
    {
        var entity = input.Adapt<ExamineStandard>();
        var project = await _rep.Context.Queryable<ExamineProject>().FirstAsync(wa => wa.Id == input.ExamineProjectId);
        entity.ExamineProjectCode = project.ExamineProjectCode;
        entity.ExamineProjectName = project.ExamineProjectName;
        var produce = await _rep.Context.Queryable<Produce>().FirstAsync(wa => wa.Id == input.ProduceId);
        entity.ProduceCode = produce.ProduceCode;
        entity.ProduceName = produce.ProduceName;
        var deviceType = await _rep.Context.Queryable<DeviceType>().FirstAsync(wa => wa.Id == input.DeviceTypeId);
        entity.DeviceTypeName = deviceType.TypeName;
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取检验标准
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<ExamineStandard> Get([FromQuery] QueryByIdExamineStandardInput input)
    {
        return await _rep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 获取检验标准列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<ExamineStandardOutput>> List([FromQuery] ExamineStandardInput input)
    {
        return await _rep.AsQueryable().Select<ExamineStandardOutput>().ToListAsync();
    }

    /// <summary>
    /// 获取产品列表
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
    /// <summary>
    /// 获取工艺设备列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "DeviceTypeDeviceTypeIdDropdown"), HttpGet]
    public async Task<dynamic> DeviceTypeDeviceTypeIdDropdown()
    {
        return await _rep.Context.Queryable<DeviceType>()
                .Select(u => new
                {
                    Label = u.TypeName,
                    Value = u.Id
                }
                ).ToListAsync();
    }
    /// <summary>
    /// 获取检验项目列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "ExamineProjectExamineProjectIdDropdown"), HttpGet]
    public async Task<dynamic> ExamineProjectExamineProjectIdDropdown()
    {
        return await _rep.Context.Queryable<ExamineProject>()
                .Select(u => new
                {
                    Label = u.ExamineProjectName,
                    Value = u.Id
                }
                ).ToListAsync();
    }




}

