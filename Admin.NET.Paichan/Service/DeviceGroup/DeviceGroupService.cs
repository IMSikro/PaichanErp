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
/// 设备分组服务
/// </summary>
[ApiDescriptionSettings(PaichanConst.GroupName, Order = 100)]
public class DeviceGroupService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<DeviceGroup> _rep;
    public DeviceGroupService(SqlSugarRepository<DeviceGroup> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询设备分组
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<List<DeviceGroupOutput>> Page(DeviceGroupInput input)
    {
        var query= _rep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.GroupName.Contains(input.SearchKey.Trim())
            )
            .WhereIF(!string.IsNullOrWhiteSpace(input.GroupName), u => u.GroupName.Contains(input.GroupName.Trim()))
            .Select((u)=> new DeviceGroupOutput{
                Id = u.Id,
                GroupCode = u.GroupCode, 
                GroupName = u.GroupName,
                DeviceTypeIds = u.DeviceTypeIds,
                DeviceIds = u.DeviceIds,
                Remark = u.Remark,
                CreateUserName = u.CreateUserName, 
                UpdateUserName = u.UpdateUserName, 
            })
;
        var deviceGroups = await query.ToListAsync();


        var deviceTypes = await _rep.Context.Queryable<DeviceType>()
            .Where(u => !u.IsDelete)
            .OrderBy(u => u.Sort)
            .Select(u => new DeviceTypeOutput
            {
                Id = u.Id,
                TypeName = u.TypeName,
                Sort = u.Sort,
                Remark = u.Remark,
            }).ToListAsync();

        var devices = await _rep.Context.Queryable<Device>()
            .Where(u => !u.IsDelete)
            .OrderBy(u => u.Sort)
            .Select(u => new DeviceOutput
            {
                Id = u.Id,
                DeviceTypeId = u.DeviceTypeId,
                DeviceCode = u.DeviceCode,
                DeviceName = u.DeviceName,
                DeviceCoefficient = u.DeviceCoefficient,
                Sort = u.Sort,
                Remark = u.Remark,
            }).ToListAsync();


        await _rep.AsSugarClient().ThenMapperAsync(deviceGroups, async o =>
        {
            if (!string.IsNullOrWhiteSpace(o.DeviceTypeIds))
                o.DeviceTypes = deviceTypes.Where(u => o.DeviceTypeIds.Contains(u.Id.ToString())).ToList();
            if (!string.IsNullOrWhiteSpace(o.DeviceIds))
                o.Devices = devices.Where(u => o.DeviceIds.Contains(u.Id.ToString())).ToList();
        });

        return deviceGroups;
    }

    /// <summary>
    /// 增加设备分组
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task Add(AddDeviceGroupInput input)
    {
        var entity = input.Adapt<DeviceGroup>();
        await _rep.InsertAsync(entity);
    }

    /// <summary>
    /// 删除设备分组
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteDeviceGroupInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _rep.FakeDeleteAsync(entity);   //假删除
        //await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新设备分组
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateDeviceGroupInput input)
    {
        var entity = input.Adapt<DeviceGroup>();
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true, ignoreAllDefaultValue: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取设备分组
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<DeviceGroup> Get([FromQuery] QueryByIdDeviceGroupInput input)
    {
        return await _rep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 获取设备分组列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<DeviceGroupOutput>> List([FromQuery] DeviceGroupInput input)
    {
        return await _rep.AsQueryable().Select<DeviceGroupOutput>().ToListAsync();
    }

}

