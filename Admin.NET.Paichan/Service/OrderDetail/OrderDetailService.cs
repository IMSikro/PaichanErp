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
/// 订单排产服务
/// </summary>
[ApiDescriptionSettings(PaichanConst.GroupName, Order = 100)]
public class OrderDetailService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<OrderDetail> _rep;
    public OrderDetailService(SqlSugarRepository<OrderDetail> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询订单排产
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<OrderDetailOutput>> Page(OrderDetailInput input)
    {
        var query = _rep.AsQueryable()
                .Where(u => !u.IsDelete)
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.OrderDetailCode.Contains(input.SearchKey.Trim())
            )
            .WhereIF(input.OrderId > 0, u => u.OrderId == input.OrderId)
            .WhereIF(!string.IsNullOrWhiteSpace(input.OrderDetailCode), u => u.OrderDetailCode.Contains(input.OrderDetailCode.Trim()))
            //处理外键和TreeSelector相关字段的连接
            .LeftJoin<Order>((u, orderid) => u.OrderId == orderid.Id)
            .LeftJoin<Device>((u, orderid, deviceid) => u.DeviceId == deviceid.Id)
            .LeftJoin<DeviceErrorType>((u, orderid, deviceid,deviceerrortype) => u.DeviceErrorTypeId == deviceerrortype.Id)
            .LeftJoin<Produce>((u, orderid, deviceid,deviceerrortype,produceid) => orderid.ProduceId == produceid.Id)
                .Where((u, orderid, deviceid,deviceerrortype, produceid) => !orderid.IsDelete && !deviceid.IsDelete && !produceid.IsDelete)
            .Select((u, orderid, deviceid,deviceerrortype,produceid) => new OrderDetailOutput
            {
                Id = u.Id,
                OrderId = u.OrderId,
                OrderIdBatchNumber = orderid.BatchNumber,
                OrderDetailCode = u.OrderDetailCode,
                StartDate = u.StartDate,
                EndDate = u.EndDate,
                DeliveryDate = orderid.DeliveryDate,
                DeviceId = u.DeviceId,
                DeviceIdDeviceCode = deviceid.DeviceCode,
                DeviceErrorTime = u.DeviceErrorTime,
                DeviceErrorTypeId = u.DeviceErrorTypeId,
                DeviceErrorType = deviceerrortype.ErrorTypeName,
                DeviceTypeId = u.DeviceTypeId,
                OperatorUsers = u.OperatorUsers,
                OperatorUsersRealName = "",
                Qty = u.Qty,
                pUnit = u.pUnit,
                Sort = u.Sort,
                Remark = u.Remark,
                CreateUserName = u.CreateUserName,
                UpdateUserName = u.UpdateUserName,
            })
;
        query = query.OrderBuilder(input, "u.", "Sort", false);
        var orderDetails = await query.ToPagedListAsync(input.Page, input.PageSize);

        await _rep.AsSugarClient().ThenMapperAsync(orderDetails.Items, async o =>
        {
            o.OperatorUsersRealName = string.Join(",", await _rep.Context.Queryable<SysUser>().Where(u => !u.IsDelete && o.OperatorUsers.Contains(u.Id.ToString()))
                .Select(u => u.RealName).ToListAsync());
        });
        return orderDetails;
    }

    /// <summary>
    /// 查询设备所有排产
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<List<OrderDetailOutput>> ListOrderDetailByDeviceId(OrderDetailDeviceInput input)
    {
        var query = _rep.AsQueryable()
                .Where(u => !u.IsDelete && u.EndDate == null)
            .WhereIF(input.DeviceId > 0, u => u.DeviceId == input.DeviceId)
            //处理外键和TreeSelector相关字段的连接
            .LeftJoin<Order>((u, orderid) => u.OrderId == orderid.Id )
            .LeftJoin<Device>((u, orderid, deviceid) => u.DeviceId == deviceid.Id)
            .LeftJoin<Produce>((u, orderid, deviceid, produceid) => orderid.ProduceId == produceid.Id )
                .Where((u, orderid, deviceid, produceid) => !orderid.IsDelete && !deviceid.IsDelete && !produceid.IsDelete)
                .Select((u, orderid, deviceid, produceid) => new OrderDetailOutput
                {
                    Id = u.Id,
                    OrderId = u.OrderId,
                    OrderIdBatchNumber = orderid.BatchNumber,
                    OrderDetailCode = u.OrderDetailCode,
                    SN = u.SN,
                    StartDate = u.StartDate,
                    EndDate = u.EndDate,
                    DeliveryDate = orderid.DeliveryDate,
                    DeviceId = u.DeviceId,
                    DeviceIdDeviceCode = deviceid.DeviceCode,
                    DeviceErrorTime = u.DeviceErrorTime,
                    DeviceTypeId = u.DeviceTypeId,
                    OperatorUsers = u.OperatorUsers,
                    OperatorUsersRealName = "",
                    Qty = u.Qty,
                    pUnit = u.pUnit,
                    ProduceId = orderid.ProduceId,
                    ProduceIdProduceName = produceid.ProduceCode,
                    ProduceName = orderid.ProduceName,
                    ColorRgb = produceid.ColorRgb,
                    Sort = u.Sort,
                    Remark = u.Remark,
                    CreateUserName = u.CreateUserName,
                    UpdateUserName = u.UpdateUserName,
                })
;
        query = query.OrderBy(u => u.Sort);
        var orderDetails = await query.ToListAsync();

        await _rep.AsSugarClient().ThenMapperAsync(orderDetails, async o =>
        {
            o.OperatorUsersRealName = string.Join(",", await _rep.Context.Queryable<SysUser>().Where(u => !u.IsDelete && o.OperatorUsers.Contains(u.Id.ToString()))
                .Select(u => u.RealName).ToListAsync());
        });
        return orderDetails;
    }

    /// <summary>
    /// 查询所有排产(三级目录)
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<List<OrderDetailByDeviceTypeOutput>> ListOrderDetailAll(long GroupId = 0)
    {
        var query = _rep.AsQueryable()
                .Where(u => !u.IsDelete && u.EndDate == null)
            //处理外键和TreeSelector相关字段的连接
            .LeftJoin<Order>((u, orderid) => u.OrderId == orderid.Id)
            .LeftJoin<Device>((u, orderid, deviceid) => u.DeviceId == deviceid.Id)
            .LeftJoin<Produce>((u, orderid, deviceid, produceid) => orderid.ProduceId == produceid.Id)
                .Where((u, orderid, deviceid, produceid) => !orderid.IsDelete && !deviceid.IsDelete && !produceid.IsDelete)
                .Select((u, orderid, deviceid, produceid) => new OrderDetailOutput
                {
                    Id = u.Id,
                    OrderId = u.OrderId,
                    OrderIdBatchNumber = orderid.BatchNumber,
                    OrderDetailCode = u.OrderDetailCode,
                    SN = u.SN,
                    StartDate = u.StartDate,
                    EndDate = u.EndDate,
                    DeliveryDate = orderid.DeliveryDate,
                    DeviceId = u.DeviceId,
                    DeviceIdDeviceCode = deviceid.DeviceCode,
                    DeviceErrorTime = u.DeviceErrorTime,
                    DeviceTypeId = u.DeviceTypeId,
                    OperatorUsers = u.OperatorUsers,
                    OperatorUsersRealName = "",
                    Qty = u.Qty,
                    pUnit = u.pUnit,
                    ProduceId = orderid.ProduceId,
                    ProduceIdProduceName = produceid.ProduceCode,
                    ProduceName = orderid.ProduceName,
                    ColorRgb = produceid.ColorRgb,
                    Sort = u.Sort,
                    Remark = u.Remark,
                    CreateUserName = u.CreateUserName,
                    UpdateUserName = u.UpdateUserName,
                })
;
        query = query.OrderBy(u => u.Sort);
        var orderDetails = await query.ToListAsync();

        await _rep.AsSugarClient().ThenMapperAsync(orderDetails, async o =>
        {
            o.OperatorUsersRealName = string.Join(",", await _rep.Context.Queryable<SysUser>().Where(u => !u.IsDelete && o.OperatorUsers.Contains(u.Id.ToString()))
                .Select(u => u.RealName).ToListAsync());
        });

        var devices = await _rep.Context.Queryable<Device>()
            .Where(u => !u.IsDelete)
            .OrderBy(u => u.Sort)
            .Select(u => new OrderDetailByDeviceOutput
            {
                DeviceId = u.Id,
                DeviceCode = u.DeviceCode,
                DeviceName = u.DeviceName,
                DeviceTypeId = u.DeviceTypeId,
                OperatorUsers = u.OperatorUsers,
            }).ToListAsync();
        await _rep.AsSugarClient().ThenMapperAsync(devices, async o =>
        {
            o.OperatorUsersRealName = string.Join(",", await _rep.Context.Queryable<SysUser>().Where(u => !u.IsDelete && o.OperatorUsers.Contains(u.Id.ToString()))
                .Select(u => u.RealName).ToListAsync());
            o.OrderDetails = orderDetails.Where(wa => wa.DeviceId == o.DeviceId).ToList();
        });

        var deviceTypes = await _rep.Context.Queryable<DeviceType>()
            .Where(u => !u.IsDelete)
            .OrderBy(u => u.Sort)
            .Select(u => new OrderDetailByDeviceTypeOutput
            {
                DeviceTypeId = u.Id,
                DeviceTypeName = u.TypeName
            }).ToListAsync();

        await _rep.AsSugarClient().ThenMapperAsync(deviceTypes, async o =>
        {
            o.Devices = devices.Where(wa => wa.DeviceTypeId == o.DeviceTypeId).ToList();
        });
        return deviceTypes;
    }

    /// <summary>
    /// 查询当前工艺所有排产(二级目录)
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<List<OrderDetailByDeviceOutput>> ListOrderDetailByDeviceTypeId(OrderDetailDeviceTypeInput input)
    {
        List<long> groupDeviceIds = [];
        if (input.GroupId > 0)
            groupDeviceIds = (await _rep.Context.Queryable<DeviceGroup>()
                .FirstAsync(wa => !wa.IsDelete && wa.Id == input.GroupId))?.DeviceIds?.Split(',').Distinct().ToList().Select(wa => Convert.ToInt64(wa)).ToList() ?? [];
        var query = _rep.AsQueryable()
                .Where(u => !u.IsDelete && u.EndDate == null)
                .WhereIF(input.GroupId > 0, u => groupDeviceIds.Contains(u.Id))
            //处理外键和TreeSelector相关字段的连接
            .LeftJoin<Order>((u, orderid) => u.OrderId == orderid.Id)
            .LeftJoin<Device>((u, orderid, deviceid) => u.DeviceId == deviceid.Id)
            .WhereIF(input.DeviceTypeId > 0, u => u.DeviceTypeId == input.DeviceTypeId)
            .LeftJoin<Produce>((u, orderid, deviceid, produceid) => orderid.ProduceId == produceid.Id)
                .Where((u, orderid, deviceid, produceid) => !orderid.IsDelete && !deviceid.IsDelete && !produceid.IsDelete)
                .Select((u, orderid, deviceid, produceid) => new OrderDetailOutput
                {
                    Id = u.Id,
                    OrderId = u.OrderId,
                    OrderIdBatchNumber = orderid.BatchNumber,
                    OrderDetailCode = u.OrderDetailCode,
                    SN = u.SN,
                    StartDate = u.StartDate,
                    EndDate = u.EndDate,
                    DeliveryDate = orderid.DeliveryDate,
                    DeviceId = u.DeviceId,
                    DeviceIdDeviceCode = deviceid.DeviceCode,
                    DeviceErrorTime = u.DeviceErrorTime,
                    DeviceTypeId = u.DeviceTypeId,
                    OperatorUsers = u.OperatorUsers,
                    OperatorUsersRealName = "",
                    Qty = u.Qty,
                    pUnit = u.pUnit,
                    ProduceId = orderid.ProduceId,
                    ProduceIdProduceName = produceid.ProduceCode,
                    ProduceName = orderid.ProduceName,
                    ColorRgb = produceid.ColorRgb,
                    Sort = u.Sort,
                    Remark = u.Remark,
                    CreateUserName = u.CreateUserName,
                    UpdateUserName = u.UpdateUserName,
                })
;
        query = query.OrderBy(u => u.Sort);
        var orderDetails = await query.ToListAsync();

        await _rep.AsSugarClient().ThenMapperAsync(orderDetails, async o =>
        {
            o.OperatorUsersRealName = string.Join(",", await _rep.Context.Queryable<SysUser>().Where(u => !u.IsDelete && o.OperatorUsers.Contains(u.Id.ToString()))
                .Select(u => u.RealName).ToListAsync());
        });

        var devices = await _rep.Context.Queryable<Device>()
            .Where(u => !u.IsDelete)
            .WhereIF(input.DeviceTypeId > 0, u => u.DeviceTypeId == input.DeviceTypeId)
            .Select(u => new OrderDetailByDeviceOutput
            {
                DeviceId = u.Id,
                DeviceCode = u.DeviceCode,
                DeviceName = u.DeviceName,
                DeviceTypeId = u.DeviceTypeId,
                OperatorUsers = u.OperatorUsers,
            }).ToListAsync();
        await _rep.AsSugarClient().ThenMapperAsync(devices, async o =>
        {
            o.OperatorUsersRealName = string.Join(",", await _rep.Context.Queryable<SysUser>().Where(u => !u.IsDelete && o.OperatorUsers.Contains(u.Id.ToString()))
                .Select(u => u.RealName).ToListAsync());
            o.OrderDetails = orderDetails.Where(wa => wa.DeviceId == o.DeviceId).ToList();
        });
        return devices;
    }

    /// <summary>
    /// 增加订单排产
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task Add(AddOrderDetailInput input)
    {
        var entity = input.Adapt<OrderDetail>();
        var order = await _rep.Context.Queryable<Order>().FirstAsync(u => u.Id == input.OrderId);
        var lastSN = await _rep.AsQueryable().Where(u => u.OrderId == input.OrderId).MaxAsync(u => u.SN) ?? 0;
        if (entity.EndDate != null) entity.EndDate = null;
        if (input.DeviceId > 0)
        {
            var device = await _rep.Context.Queryable<Device>().Where(u => !u.IsDelete && u.Id == input.DeviceId).FirstAsync();
            entity.DeviceTypeId = device.DeviceTypeId;
            if(string.IsNullOrWhiteSpace(device.OperatorUsers)) throw Oops.Oh("请先设置设备操作人员!");
            entity.OperatorUsers = device.OperatorUsers;
        }
        entity.SN = lastSN + 1;
        entity.OrderDetailCode = $"{order.BatchNumber}-{entity.SN}";
        await _rep.InsertAsync(entity);
    }

    /// <summary>
    /// 批量增加订单排产
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "BatchAdd")]
    public async Task Add(List<AddOrderDetailInput> input)
    {
        var entity = input.Adapt<List<OrderDetail>>();
        entity = entity.OrderBy(e => e.OrderId).ToList();
        var orderId = 0L;
        foreach (var orderDetail in entity)
        {
            Order order = null;
            var lastSN = 0;
            if (orderId != orderDetail.OrderId)
            {
                order = await _rep.Context.Queryable<Order>().FirstAsync(u => u.Id == orderDetail.OrderId);
                lastSN = await _rep.AsQueryable().Where(u => u.OrderId == orderDetail.OrderId).MaxAsync(u => u.SN) ?? 0;
            }
            if (orderDetail.DeviceId > 0)
            {
                var device = await _rep.Context.Queryable<Device>().Where(u => !u.IsDelete && u.Id == orderDetail.DeviceId).FirstAsync();
                orderDetail.DeviceTypeId = device.DeviceTypeId;
                if (!string.IsNullOrWhiteSpace(device.OperatorUsers)) orderDetail.OperatorUsers = device.OperatorUsers;
            }
            orderDetail.Sort ??= 999;
            if (orderDetail.Sort <= 0) orderDetail.Sort = 999;
            if (orderDetail.EndDate != null) orderDetail.EndDate = null;
            orderDetail.SN = ++lastSN;
            orderDetail.OrderDetailCode = $"{order.BatchNumber}-{orderDetail.SN}";
        }

        await _rep.InsertRangeAsync(entity);
    }

    /// <summary>
    /// 删除订单排产
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteOrderDetailInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _rep.FakeDeleteAsync(entity);   //假删除
        //await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新订单排产
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateOrderDetailInput input)
    {
        if (input.DeviceErrorTime > 0 && string.IsNullOrWhiteSpace(input.DeviceErrorType)) throw Oops.Oh("请填写设备未生产类型");
        var entity = input.Adapt<OrderDetail>();
        if (input.DeviceId > 0)
        {
            var device = await _rep.Context.Queryable<Device>().Where(u => !u.IsDelete && u.Id == input.DeviceId).FirstAsync();
            entity.DeviceTypeId = device.DeviceTypeId;
            if (!string.IsNullOrWhiteSpace(device.OperatorUsers)) entity.OperatorUsers = device.OperatorUsers;
        }

        if (input.EndDate != null)
        {
            if(input.EndDate > new DateTime(1970,1,1))
                input.EndDate = DateTime.Now;
            else input.EndDate = null;
        }
        
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true, ignoreAllDefaultValue: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 小计完工
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "DoneAndNext")]
    public async Task DoneAndNext(DoneOrderDetailInput input)
    {
        //if (input.DeviceErrorTime > 0 && string.IsNullOrWhiteSpace(input.DeviceErrorType)) throw Oops.Oh("请填写设备未生产类型");
        var entity = await _rep.AsQueryable().FirstAsync(od => od.Id == input.Id);
        var yuanQty = entity.Qty;

        entity.EndDate = DateTime.Now;
        entity.Qty = input.Qty;
        entity.DeviceErrorTime = input.DeviceErrorTime;
        entity.DeviceErrorTypeId = input.DeviceErrorTypeId;
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true, ignoreAllDefaultValue: true).ExecuteCommandAsync();
        var sn = (await _rep.AsQueryable().Where(od => od.OrderId == entity.OrderId).MaxAsync(od => od.SN)) ?? 0;
        var batchCode = (await _rep.Context.Queryable<Order>().FirstAsync(o => o.Id == entity.OrderId)).BatchNumber;
        if (yuanQty > input.Qty)
        {
            var orderDetail = new OrderDetail
            {
                TenantId = entity.TenantId,
                OrderId = entity.OrderId,
                OrderDetailCode = $"{batchCode}-{sn+1}",
                Qty = yuanQty - input.Qty,
                pUnit = entity.pUnit,
                Sort = entity.Sort,
                DeviceId = entity.DeviceId,
                DeviceTypeId = entity.DeviceTypeId,
                OperatorUsers = entity.OperatorUsers,
                Remark = entity.Remark,
            };

            await _rep.InsertAsync(orderDetail);
        }
    }

    /// <summary>
    /// 终结完工
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Done")]
    public async Task Done(DoneOrderDetailInput input)
    {
        var entity = await _rep.AsQueryable().FirstAsync(od => od.Id == input.Id);
        //var order = await _rep.Context.Queryable<Order>().FirstAsync(u => u.Id == entity.OrderId);
        //order.EndDate = DateTime.Now;
        //await _rep.Context.Updateable(order)
        //    .IgnoreColumns(ignoreAllNullColumns: true, ignoreAllDefaultValue: true).ExecuteCommandAsync();
        entity.EndDate = DateTime.Now;
        entity.Qty = input.Qty;
        entity.DeviceErrorTime = input.DeviceErrorTime;
        entity.DeviceErrorTypeId = input.DeviceErrorTypeId;
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true, ignoreAllDefaultValue: true).ExecuteCommandAsync();
        
    }


    /// <summary>
    /// 设置设备排产排序
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task SetOrderDetailSort(List<SetOrderDetailSortInput> input)
    {
        var entity = input.Adapt<List<OrderDetail>>();
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true, ignoreAllDefaultValue: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取订单排产
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<OrderDetailOutput> Get([FromQuery] QueryByIdOrderDetailInput input)
    {
        var res = await _rep.AsQueryable().Where(u => !u.IsDelete && u.Id == input.Id)//处理外键和TreeSelector相关字段的连接
            .LeftJoin<Order>((u, orderid) => u.OrderId == orderid.Id)
            .LeftJoin<Device>((u, orderid, deviceid) => u.DeviceId == deviceid.Id)
            .LeftJoin<Produce>((u, orderid, deviceid, produceid) => orderid.ProduceId == produceid.Id)
            .Where((u, orderid, deviceid, produceid) => !orderid.IsDelete && !deviceid.IsDelete && !produceid.IsDelete)
            .Select((u, orderid, deviceid, produceid) => new OrderDetailOutput
            {
                Id = u.Id,
                OrderId = u.OrderId,
                OrderIdBatchNumber = orderid.BatchNumber,
                OrderDetailCode = u.OrderDetailCode,
                SN = u.SN,
                StartDate = u.StartDate,
                EndDate = u.EndDate,
                DeliveryDate = orderid.DeliveryDate,
                DeviceId = u.DeviceId,
                DeviceIdDeviceCode = deviceid.DeviceCode,
                DeviceErrorTime = u.DeviceErrorTime,
                DeviceTypeId = u.DeviceTypeId,
                OperatorUsers = u.OperatorUsers,
                OperatorUsersRealName = "",
                Qty = u.Qty,
                pUnit = u.pUnit,
                ProduceId = orderid.ProduceId,
                ProduceIdProduceName = produceid.ProduceCode,
                ProduceName = orderid.ProduceName,
                ColorRgb = produceid.ColorRgb,
                Sort = u.Sort,
                Remark = u.Remark,
                CreateUserName = u.CreateUserName,
                UpdateUserName = u.UpdateUserName,
            }).ToListAsync();

        return res?.FirstOrDefault() ?? new OrderDetailOutput();
    }

    /// <summary>
    /// 获取订单排产列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<OrderDetailOutput>> List([FromQuery] OrderDetailInput input)
    {
        return await _rep.AsQueryable()
            .Where(u => !u.IsDelete).Select<OrderDetailOutput>().ToListAsync();
    }

    /// <summary>
    /// 获取订单批号列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "OrderOrderIdDropdown"), HttpGet]
    public async Task<dynamic> OrderOrderIdDropdown()
    {
        return await _rep.Context.Queryable<Order>()
            .Where(u => !u.IsDelete)
                .Select(u => new
                {
                    Label = u.BatchNumber,
                    Value = u.Id
                }
                ).ToListAsync();
    }
    /// <summary>
    /// 获取设备外键列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "DeviceDeviceIdDropdown"), HttpGet]
    public async Task<dynamic> DeviceDeviceIdDropdown()
    {
        return await _rep.Context.Queryable<Device>()
            .Where(u => !u.IsDelete).OrderBy(u => u.Sort)
            .Select(u => new
            {
                Label = u.DeviceCode,
                Value = u.Id,
                u.DeviceTypeId,
            }).ToListAsync();
    }
    /// <summary>
    /// 获取操作人员列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "SysUserOperatorUsersDropdown"), HttpGet]
    public async Task<dynamic> SysUserOperatorUsersDropdown()
    {
        return await _rep.Context.Queryable<SysUser>()
            .Where(u => !u.IsDelete)
            .Select(u => new
            {
                Label = u.RealName,
                Value = u.Id
            }).ToListAsync();
    }

    /// <summary>
    /// 获取设备未生产类型列表
    /// </summary>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "DeviceErrorTypeDropdown"), HttpGet]
    public async Task<dynamic> DeviceErrorTypeDropdown()
    {
        return await _rep.Context.Queryable<DeviceErrorType>()
            .Where(u => !u.IsDelete)
            .Select(u => new
            {
                Label = u.ErrorTypeName,
                Value = u.Id
            }).ToListAsync();
    }

}

