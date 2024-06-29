using Admin.NET.Core;
using Admin.NET.Paichan.Const;
using Admin.NET.Paichan.Entity;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Furion;

namespace Admin.NET.Paichan;
/// <summary>
/// 设备类型服务
/// </summary>
[ApiDescriptionSettings(PaichanConst.GroupName, Order = 100)]
public class DeviceTypeService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<DeviceType> _rep;
    public DeviceTypeService(SqlSugarRepository<DeviceType> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询设备类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<DeviceTypeOutput>> Page(DeviceTypeInput input)
    {
        List<long> groupDeviceTypeIds = [];
        if(input.GroupId > 0)
            groupDeviceTypeIds = (await _rep.Context.Queryable<DeviceGroup>()
                .FirstAsync(wa => !wa.IsDelete && wa.Id == input.GroupId))?.DeviceTypeIds?.Split(',').Distinct().ToList().Select(wa => Convert.ToInt64(wa)).ToList() ?? [];
        var query= _rep.AsQueryable().Where(u => !u.IsDelete)
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u => u.TypeName.Contains(input.SearchKey.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.TypeName), u => u.TypeName.Contains(input.TypeName.Trim()))
            .WhereIF(input.GroupId > 0, u => groupDeviceTypeIds.Contains(u.Id))

            .Select<DeviceTypeOutput>()
;
        query = query.OrderBuilder(input, "", "Sort", false);
        var pageList = await query.ToPagedListAsync(input.Page, input.PageSize);


        foreach (var d in pageList.Items)
        {
            var orders = await _rep.Context.Queryable<Order>()
                .Where(u => !u.IsDelete)
                //处理外键和TreeSelector相关字段的连接
                .LeftJoin<Produce>((u, produceid) => u.ProduceId == produceid.Id)
                .Where((u, produceid) => produceid.DeviceTypes.Contains(d.Id.ToString()) && !produceid.IsDelete)
                .Select((u, produceid) => new OrderOutput
                {
                    Id = u.Id,
                    OrderCode = u.OrderCode,
                    OrderDate = u.OrderDate,
                    DeliveryDate = u.DeliveryDate,
                    StartDate = u.StartDate,
                    EndDate = u.EndDate,
                    ProduceId = u.ProduceId,
                    ProduceIdProduceName = produceid.ProduceCode,
                    ProduceName = u.ProduceName,
                    ColorRgb = produceid.ColorRgb,
                    BatchNumber = u.BatchNumber,
                    Quantity = u.Quantity,
                    IsEnd = u.IsEnd,
                    pUnit = u.pUnit,
                    Customer = u.Customer,
                    Remark = u.Remark,
                    CreateUserName = u.CreateUserName,
                    UpdateUserName = u.UpdateUserName,
                }).ToListAsync();

            foreach (var o in orders)
            {
                var paiOrder = await _rep.Context.Queryable<OrderDetail>()
                    .Where(u => !u.IsDelete)
                    .Where(u => u.OrderId == o.Id)
                    .Where(u => u.DeviceTypeId == d.Id)
                    //处理外键和TreeSelector相关字段的连接
                    .LeftJoin<Order>((u, orderid) => u.OrderId == orderid.Id)
                    .LeftJoin<Produce>((u, orderid, produceid) => orderid.ProduceId == produceid.Id)
                    .Where((u, orderid, produceid) => !orderid.IsDelete && !produceid.IsDelete).ToListAsync();

                var paiQty = paiOrder.Sum(u => u.Qty);
                var isEnd = paiOrder.Any(od => od.IsEnd);
                o.OrderSurplusQuantity = o.Quantity - (paiQty ?? 0);
                o.DeviceTypeIsEnd = isEnd;
            }

            orders = orders.Where(o => o.OrderSurplusQuantity > 0 && !o.DeviceTypeIsEnd).ToList();
            
            // 获取工艺未排产数量及批数
            d.UnOrderBatchNum = orders.Count;
            d.UnOrderNumber = orders.Sum(u => u.OrderSurplusQuantity > 0 ? u.OrderSurplusQuantity : 0) ?? 0;

            var paiOrder2 = await _rep.Context.Queryable<OrderDetail>()
                .Where(u => !u.IsDelete)
                .Where(u => u.DeviceTypeId == d.Id)
                //处理外键和TreeSelector相关字段的连接
                .LeftJoin<Order>((u, orderid) => u.OrderId == orderid.Id)
                .Where((u, orderid) => !orderid.IsDelete)
                .LeftJoin<Produce>((u, orderid, produceid) => orderid.ProduceId == produceid.Id)
                .Where((u, orderid, produceid) => !orderid.IsDelete && !produceid.IsDelete)
                .ToListAsync();
            // 获取工艺已排产数量及批数
            d.OrderBatchNum = paiOrder2.Where(u => u.EndDate == null).Select(u => u.OrderId).Distinct().Count();
            d.OrderNumber = paiOrder2.Where(u => u.EndDate == null).Sum(u => u.Qty) ?? 0;

            paiOrder2 = paiOrder2.Where(u => u.EndDate != null && u.EndDate.Value.Date == DateTime.Today).ToList();
            // 获取工艺已完工数量及批数
            d.EndOrderBatchNum = paiOrder2.Select(u => u.OrderId).Distinct().Count();
            d.EndOrderNumber = paiOrder2.Sum(u => u.Qty) ?? 0;
        }
        return pageList;
    }


    /// <summary>
    /// 首页图表显示
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "HomeIndex")]
    public async Task<dynamic> HomeIndex()
    {
        var query = _rep.AsQueryable().Where(u => !u.IsDelete)
            .Select<DeviceTypeOutput>();
        query = query.OrderBy(i => i.Sort);
        var dtList = await query.ToListAsync();

        // 获取工艺数量及批数
        foreach (var d in dtList)
        {
            var orders = await _rep.Context.Queryable<Order>()
                .Where(u => !u.IsDelete)
                //处理外键和TreeSelector相关字段的连接
                .LeftJoin<Produce>((u, produceid) => u.ProduceId == produceid.Id)
                .Where((u, produceid) => produceid.DeviceTypes.Contains(d.Id.ToString()) && !produceid.IsDelete)
                .Select((u, produceid) => new OrderOutput
                {
                    Id = u.Id,
                    OrderCode = u.OrderCode,
                    OrderDate = u.OrderDate,
                    DeliveryDate = u.DeliveryDate,
                    StartDate = u.StartDate,
                    EndDate = u.EndDate,
                    ProduceId = u.ProduceId,
                    ProduceIdProduceName = produceid.ProduceCode,
                    ProduceName = u.ProduceName,
                    ColorRgb = produceid.ColorRgb,
                    BatchNumber = u.BatchNumber,
                    Quantity = u.Quantity,
                    IsEnd = u.IsEnd,
                    pUnit = u.pUnit,
                    Customer = u.Customer,
                    Remark = u.Remark,
                    CreateUserName = u.CreateUserName,
                    UpdateUserName = u.UpdateUserName,
                }).ToListAsync();

            foreach (var o in orders)
            {
                var paiOrder = await _rep.Context.Queryable<OrderDetail>()
                    .Where(u => !u.IsDelete)
                    .Where(u => u.OrderId == o.Id)
                    .Where(u => u.DeviceTypeId == d.Id)
                    //处理外键和TreeSelector相关字段的连接
                    .LeftJoin<Order>((u, orderid) => u.OrderId == orderid.Id)
                    .Where((u, orderid) => !orderid.IsDelete)
                    .LeftJoin<Produce>((u, orderid, produceid) => orderid.ProduceId == produceid.Id)
                    .Where((u, orderid, produceid) => !produceid.IsDelete)
                    .ToListAsync();

                var paiQty = paiOrder.Sum(u => u.Qty);
                var isEnd = paiOrder.Any(od => od.IsEnd);
                o.OrderSurplusQuantity = o.Quantity - (paiQty ?? 0);
                o.DeviceTypeIsEnd = isEnd;
            }

            orders = orders.Where(o => o.OrderSurplusQuantity > 0).ToList();

            // 获取工艺未排产数量及批数
            d.UnOrderBatchNum = orders.Count;
            d.UnOrderNumber = orders.Sum(u => u.OrderSurplusQuantity > 0 ? u.OrderSurplusQuantity : 0) ?? 0;

            var paiOrder2 = await _rep.Context.Queryable<OrderDetail>()
                .Where(u => !u.IsDelete)
                .Where(u => u.DeviceTypeId == d.Id)
                //处理外键和TreeSelector相关字段的连接
                .LeftJoin<Order>((u, orderid) => u.OrderId == orderid.Id)
                .Where((u, orderid) => !orderid.IsDelete)
                .LeftJoin<Produce>((u, orderid, produceid) => orderid.ProduceId == produceid.Id)
                .Where((u, orderid, produceid) => !produceid.IsDelete)
                .ToListAsync();
            // 获取工艺已排产数量及批数
            d.OrderBatchNum = paiOrder2.Where(u => u.EndDate == null).Select(u => u.OrderId).Distinct().Count();
            d.OrderNumber = paiOrder2.Where(u => u.EndDate == null).Sum(u => u.Qty) ?? 0;

            paiOrder2 = paiOrder2.Where(u => u.EndDate != null && u.EndDate.Value.Date == DateTime.Today).ToList();
            // 获取工艺已完工数量及批数
            d.EndOrderBatchNum = paiOrder2.Select(u => u.OrderId).Distinct().Count();
            d.EndOrderNumber = paiOrder2.Sum(u => u.Qty) ?? 0;
        }

        var sumorders = await _rep.Context.Queryable<Order>()
            //处理外键和TreeSelector相关字段的连接
            .LeftJoin<Produce>((u, produceid) => u.ProduceId == produceid.Id)
            .Where((u, produceid) => !u.IsDelete && !u.IsEnd && !produceid.IsDelete)
            .ToListAsync();

        foreach (var o in sumorders)
        {
            if (!string.IsNullOrWhiteSpace(o.DeviceTypes))
            {
                var deviceTypes = await _rep.Context.Queryable<DeviceType>()
                    .Where(dt => !dt.IsDelete && o.DeviceTypes.Contains(dt.Id.ToString())).Select(dt => dt.Id).ToListAsync();
                o.DeviceTypes = deviceTypes.Any() ? string.Join(',', deviceTypes) : string.Empty;
            }
        }

        sumorders = sumorders.Where(o => !string.IsNullOrWhiteSpace(o.DeviceTypes)).ToList();

        var data = new
        {
            dtList,
            UnHome = new
            {
                BatchNum = dtList.Sum(u => u.UnOrderBatchNum),
                Number = dtList.Sum(u => u.UnOrderNumber),
            },
            PaiHome = new
            {
                BatchNum = dtList.Sum(u => u.OrderBatchNum),
                Number = dtList.Sum(u => u.OrderNumber),
            },
            EndHome = new
            {
                BatchNum = dtList.Sum(u => u.EndOrderBatchNum),
                Number = dtList.Sum(u => u.EndOrderNumber),
            },
            SumHome = new
            {
                BatchNum = sumorders.Count,
                Number = sumorders.Sum(u => u.Quantity),
            },
        };

        return data;
    }

    /// <summary>
    /// 增加设备类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task Add(AddDeviceTypeInput input)
    {
        var entity = input.Adapt<DeviceType>();
        await _rep.InsertAsync(entity);
    }

    /// <summary>
    /// 删除设备类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteDeviceTypeInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _rep.FakeDeleteAsync(entity);   //假删除
        await App.GetService<ProduceService>().DeleteDeviceTypes([entity.Id]);
        await App.GetService<OrderService>().DeleteDeviceTypes([entity.Id]);
        //await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 清除已失效设备类型
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "ClearIsDelete")]
    public async Task ClearIsDelete()
    {
        // 获取已删除工艺
        var list = await _rep.AsQueryable().Where(u => u.IsDelete).Select(u => u.Id).ToListAsync();
        if(list.Count > 0)
        {
            await App.GetService<ProduceService>().DeleteDeviceTypes(list);
            await App.GetService<OrderService>().DeleteDeviceTypes(list);
        }
    }

    /// <summary>
    /// 更新设备类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateDeviceTypeInput input)
    {
        var entity = input.Adapt<DeviceType>();
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取设备类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<DeviceType> Get([FromQuery] QueryByIdDeviceTypeInput input)
    {
        return await _rep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 获取设备类型列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<DeviceTypeOutput>> List([FromQuery] DeviceTypeInput input)
    {
        return await _rep.AsQueryable().Where(u => !u.IsDelete).Select<DeviceTypeOutput>().ToListAsync();
    }

    /// <summary>
    /// 查询工艺设备及设备列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<DeviceTypeOutput>> ListDeviceTypeAndChild()
    {
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

        await _rep.AsSugarClient().ThenMapperAsync(deviceTypes, async o =>
        {
            o.Devices = devices.Where(u => o.Id == u.DeviceTypeId).ToList();
        });
        return deviceTypes;
    }

}

