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
/// 工艺标准服务
/// </summary>
[ApiDescriptionSettings(PaichanConst.GroupName, Order = 100)]
public class ProcessStandardService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<ProcessStandard> _rep;
    public ProcessStandardService(SqlSugarRepository<ProcessStandard> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询工艺标准
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<ProcessStandardOutput>> Page(ProcessStandardInput input)
    {
        var query= _rep.AsQueryable()
            .WhereIF(input.ProduceFormulaId>0, u => u.ProduceFormulaId == input.ProduceFormulaId)
            .WhereIF(input.ProduceId>0, u => u.ProduceId == input.ProduceId)
            .WhereIF(input.DeviceTypeId>0, u => u.DeviceTypeId == input.DeviceTypeId)
            .WhereIF(input.ProcessProjectId>0, u => u.ProcessProjectId == input.ProcessProjectId)
            //处理外键和TreeSelector相关字段的连接
            .LeftJoin<ProduceFormula>((u, produceformulaid) => u.ProduceFormulaId == produceformulaid.Id )
            .LeftJoin<Produce>((u, produceformulaid, produceid) => u.ProduceId == produceid.Id )
            .LeftJoin<DeviceType>((u, produceformulaid, produceid, devicetypeid) => u.DeviceTypeId == devicetypeid.Id )
            .LeftJoin<ProcessProject>((u, produceformulaid, produceid, devicetypeid, processprojectid) => u.ProcessProjectId == processprojectid.Id )
            .Select((u, produceformulaid, produceid, devicetypeid, processprojectid)=> new ProcessStandardOutput{
                Id = u.Id, 
                ProduceFormulaId = u.ProduceFormulaId, 
                ProduceFormulaIdProduceFormulaCode = produceformulaid.ProduceFormulaCode,
                ProcessStandardCode = u.ProcessStandardCode, 
                ProcessStandardName = u.ProcessStandardName, 
                ProduceId = u.ProduceId, 
                ProduceIdProduceCode = produceid.ProduceCode,
                ProduceCode = u.ProduceCode, 
                ProduceName = u.ProduceName, 
                DeviceTypeId = u.DeviceTypeId, 
                DeviceTypeIdTypeName = devicetypeid.TypeName,
                DeviceTypeName = u.DeviceTypeName, 
                ProcessProjectId = u.ProcessProjectId, 
                ProcessProjectIdProcessProjectName = processprojectid.ProcessProjectName,
                ProcessProjectCode = u.ProcessProjectCode, 
                ProcessProjectName = u.ProcessProjectName, 
                StandardValue = u.StandardValue, 
                Tolerance1 = u.Tolerance1, 
                Tolerance2 = u.Tolerance2, 
                Remark = u.Remark, 
                CreateUserName = u.CreateUserName, 
                UpdateUserName = u.UpdateUserName, 
            })
;
        query = query.OrderBuilder(input, "", "CreateTime");
        return await query.ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加工艺标准
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task Add(AddProcessStandardInput input)
    {
        var entity = input.Adapt<ProcessStandard>();
        await _rep.InsertAsync(entity);
    }

    /// <summary>
    /// 删除工艺标准
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteProcessStandardInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _rep.FakeDeleteAsync(entity);   //假删除
        //await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新工艺标准
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateProcessStandardInput input)
    {
        var entity = input.Adapt<ProcessStandard>();
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取工艺标准
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<ProcessStandard> Get([FromQuery] QueryByIdProcessStandardInput input)
    {
        return await _rep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 获取工艺标准列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<ProcessStandardOutput>> List([FromQuery] ProcessStandardInput input)
    {
        return await _rep.AsQueryable().Select<ProcessStandardOutput>().ToListAsync();
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
    /// 获取工艺项目列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "ProcessProjectProcessProjectIdDropdown"), HttpGet]
    public async Task<dynamic> ProcessProjectProcessProjectIdDropdown()
    {
        return await _rep.Context.Queryable<ProcessProject>()
                .Select(u => new
                {
                    Label = u.ProcessProjectName,
                    Value = u.Id
                }
                ).ToListAsync();
    }




}

