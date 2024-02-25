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
        var query= _rep.AsQueryable()
                .Where(u => !u.IsDelete)
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.OrderDetailCode.Contains(input.SearchKey.Trim())
            )
            .WhereIF(input.OrderId>0, u => u.OrderId == input.OrderId)
            .WhereIF(!string.IsNullOrWhiteSpace(input.OrderDetailCode), u => u.OrderDetailCode.Contains(input.OrderDetailCode.Trim()))
            //处理外键和TreeSelector相关字段的连接
            .LeftJoin<Order>((u, orderid) => u.OrderId == orderid.Id )
            .LeftJoin<Device>((u, orderid, deviceid) => u.DeviceId == deviceid.Id )
            .Select((u, orderid, deviceid)=> new OrderDetailOutput{
                Id = u.Id, 
                OrderId = u.OrderId, 
                OrderIdBatchNumber = orderid.BatchNumber,
                OrderDetailCode = u.OrderDetailCode, 
                StartDate = u.StartDate, 
                EndDate = u.EndDate, 
                DeviceId = u.DeviceId, 
                DeviceIdDeviceCode = deviceid.DeviceCode,
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
        query = query.OrderBuilder(input, "", "Sort",false);
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
                .Where(u => !u.IsDelete && u.StartDate != null && u.EndDate == null)
            .WhereIF(input.DeviceId > 0, u => u.DeviceId == input.DeviceId)
            //处理外键和TreeSelector相关字段的连接
            .LeftJoin<Order>((u, orderid) => u.OrderId == orderid.Id)
            .LeftJoin<Device>((u, orderid, deviceid) => u.DeviceId == deviceid.Id)
            .LeftJoin<Produce>((u, orderid, deviceid,produceid) => orderid.ProduceId == produceid.Id)
                .Select((u, orderid, deviceid, produceid) => new OrderDetailOutput
            {
                Id = u.Id,
                OrderId = u.OrderId,
                OrderIdBatchNumber = orderid.BatchNumber,
                OrderDetailCode = u.OrderDetailCode,
                StartDate = u.StartDate,
                EndDate = u.EndDate,
                DeviceId = u.DeviceId,
                DeviceIdDeviceCode = deviceid.DeviceCode,
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
        query = query.OrderBuilder(input, "", "Sort", false);
        var orderDetails = await query.ToListAsync();

        await _rep.AsSugarClient().ThenMapperAsync(orderDetails, async o =>
        {
            o.OperatorUsersRealName = string.Join(",", await _rep.Context.Queryable<SysUser>().Where(u => !u.IsDelete && o.OperatorUsers.Contains(u.Id.ToString()))
                .Select(u => u.RealName).ToListAsync());
        });
        return orderDetails;
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
        await _rep.InsertAsync(entity);
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
        var entity = input.Adapt<OrderDetail>();
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取订单排产
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<OrderDetail> Get([FromQuery] QueryByIdOrderDetailInput input)
    {
        return await _rep.GetFirstAsync(u => !u.IsDelete && u.Id == input.Id);
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
            .Where(u => !u.IsDelete)
                .Select(u => new
                {
                    Label = u.DeviceCode,
                    Value = u.Id
                }
                ).ToListAsync();
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
                }
                ).ToListAsync();
    }




}

