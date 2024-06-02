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
/// 非生产类型服务
/// </summary>
[ApiDescriptionSettings(PaichanConst.GroupName, Order = 100)]
public class DeviceErrorTypeService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<DeviceErrorType> _rep;
    public DeviceErrorTypeService(SqlSugarRepository<DeviceErrorType> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询非生产类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<DeviceErrorTypeOutput>> Page(DeviceErrorTypeInput input)
    {
        var query= _rep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.ErrorTypeName.Contains(input.SearchKey.Trim())
            )
            .WhereIF(!string.IsNullOrWhiteSpace(input.ErrorTypeName), u => u.ErrorTypeName.Contains(input.ErrorTypeName.Trim()))
            .Select<DeviceErrorTypeOutput>()
;
        query = query.OrderBuilder(input, "", "CreateTime");
        return await query.ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加非生产类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task Add(AddDeviceErrorTypeInput input)
    {
        var entity = input.Adapt<DeviceErrorType>();
        await _rep.InsertAsync(entity);
    }

    /// <summary>
    /// 删除非生产类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteDeviceErrorTypeInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _rep.FakeDeleteAsync(entity);   //假删除
        //await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新非生产类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateDeviceErrorTypeInput input)
    {
        var entity = input.Adapt<DeviceErrorType>();
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true, ignoreAllDefaultValue: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取非生产类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<DeviceErrorType> Get([FromQuery] QueryByIdDeviceErrorTypeInput input)
    {
        return await _rep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 获取非生产类型列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<DeviceErrorTypeOutput>> List([FromQuery] DeviceErrorTypeInput input)
    {
        return await _rep.AsQueryable().Select<DeviceErrorTypeOutput>().ToListAsync();
    }

}

