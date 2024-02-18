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
/// 设备列表服务
/// </summary>
[ApiDescriptionSettings(PaichanConst.GroupName, Order = 100)]
public class DeviceService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<Device> _rep;
    public DeviceService(SqlSugarRepository<Device> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询设备列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<DeviceOutput>> Page(DeviceInput input)
    {
        var query= _rep.AsQueryable().Where(u => !u.IsDelete)
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.DeviceCode.Contains(input.SearchKey.Trim())
                || u.DeviceName.Contains(input.SearchKey.Trim())
            )
            .WhereIF(input.DeviceTypeId>0, u => u.DeviceTypeId == input.DeviceTypeId)
            .WhereIF(!string.IsNullOrWhiteSpace(input.DeviceCode), u => u.DeviceCode.Contains(input.DeviceCode.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.DeviceName), u => u.DeviceName.Contains(input.DeviceName.Trim()))
            //处理外键和TreeSelector相关字段的连接
            .LeftJoin<DeviceType>((u, devicetypeid) => u.DeviceTypeId == devicetypeid.Id )
            .Select((u, devicetypeid)=> new DeviceOutput{
                Id = u.Id, 
                DeviceTypeId = u.DeviceTypeId, 
                DeviceTypeIdTypeName = devicetypeid.TypeName,
                DeviceCode = u.DeviceCode, 
                DeviceName = u.DeviceName, 
                DeviceCoefficient = u.DeviceCoefficient, 
                Remark = u.Remark, 
                CreateUserName = u.CreateUserName, 
                UpdateUserName = u.UpdateUserName, 
            })
;
        query = query.OrderBuilder(input, "u.", "CreateTime",false);
        return await query.ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加设备列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task Add(AddDeviceInput input)
    {
        var entity = input.Adapt<Device>();
        await _rep.InsertAsync(entity);
    }

    /// <summary>
    /// 删除设备列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteDeviceInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _rep.FakeDeleteAsync(entity);   //假删除
        //await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新设备列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateDeviceInput input)
    {
        var entity = input.Adapt<Device>();
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取设备列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<Device> Get([FromQuery] QueryByIdDeviceInput input)
    {
        return await _rep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 获取设备列表列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<DeviceOutput>> List([FromQuery] DeviceInput input)
    {
        return await _rep.AsQueryable().Where(u => !u.IsDelete).Select<DeviceOutput>().ToListAsync();
    }

    /// <summary>
    /// 获取设备类型列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "DeviceTypeDeviceTypeIdDropdown"), HttpGet]
    public async Task<dynamic> DeviceTypeDeviceTypeIdDropdown()
    {
        return await _rep.Context.Queryable<DeviceType>().Where(u => !u.IsDelete)
                .Select(u => new
                {
                    Label = u.TypeName,
                    Value = u.Id
                }
                ).ToListAsync();
    }




}

