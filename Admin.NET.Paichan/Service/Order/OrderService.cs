using Admin.NET.Core;
using Admin.NET.Core.Service;
using Admin.NET.Paichan.Const;
using Admin.NET.Paichan.Entity;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Furion;
using Furion.UnifyResult;
using AngleSharp.Dom;

namespace Admin.NET.Paichan;
/// <summary>
/// 订单列表服务
/// </summary>
[ApiDescriptionSettings(PaichanConst.GroupName, Order = 100)]
public class OrderService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<Order> _rep;
    private readonly SqlSugarRepository<OrderDetail> _repDetail;
    public OrderService(SqlSugarRepository<Order> rep, SqlSugarRepository<OrderDetail> repDetail)
    {
        _rep = rep;
        _repDetail = repDetail;

    }

    /// <summary>
    /// 分页查询订单列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<OrderOutput>> Page(OrderInput input)
    {
        var query = _rep.AsQueryable()
                .Where(u => !u.IsDelete)
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.OrderCode.Contains(input.SearchKey.Trim())
                || u.pUnit.Contains(input.SearchKey.Trim())
                || u.Customer.Contains(input.SearchKey.Trim())
            )
            .WhereIF(!string.IsNullOrWhiteSpace(input.OrderCode), u => u.OrderCode.Contains(input.OrderCode.Trim()))
            .WhereIF(input.ProduceId > 0, u => u.ProduceId == input.ProduceId)
            .WhereIF(input.OrderEndStatus != null, u => u.IsEnd == input.OrderEndStatus)
            .WhereIF(!string.IsNullOrWhiteSpace(input.ProduceName), u => u.ProduceName == input.ProduceName)
            .WhereIF(!string.IsNullOrWhiteSpace(input.pUnit), u => u.pUnit.Contains(input.pUnit.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Customer), u => u.Customer.Contains(input.Customer.Trim()))
            //处理外键和TreeSelector相关字段的连接
            .LeftJoin<Produce>((u, produceid) => u.ProduceId == produceid.Id)
                .Where((u, produceid) => !produceid.IsDelete)
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
                ProduceCode = u.ProduceCode,
                ProduceName = u.ProduceName,
                IsEnd = u.IsEnd,
                ColorRgb = produceid.ColorRgb,
                BatchNumber = u.BatchNumber,
                Quantity = u.Quantity,
                pUnit = u.pUnit,
                Customer = u.Customer,
                Remark = u.Remark,
                CreateUserName = u.CreateUserName,
                UpdateUserName = u.UpdateUserName,
            })
            ;
        if (input.OrderDateRange != null && input.OrderDateRange.Count > 0)
        {
            DateTime? start = input.OrderDateRange[0];
            query = query.WhereIF(start.HasValue, u => u.OrderDate > start);
            if (input.OrderDateRange.Count > 1 && input.OrderDateRange[1].HasValue)
            {
                var end = input.OrderDateRange[1].Value.AddDays(1);
                query = query.Where(u => u.OrderDate < end);
            }
        }
        if (input.DeliveryDateRange != null && input.DeliveryDateRange.Count > 0)
        {
            DateTime? start = input.DeliveryDateRange[0];
            query = query.WhereIF(start.HasValue, u => u.DeliveryDate > start);
            if (input.DeliveryDateRange.Count > 1 && input.DeliveryDateRange[1].HasValue)
            {
                var end = input.DeliveryDateRange[1].Value.AddDays(1);
                query = query.Where(u => u.DeliveryDate < end);
            }
        }
        query = query.OrderBuilder(input, "u.", "CreateTime");
        var orders = await query.ToPagedListAsync(input.Page, input.PageSize);

        await _rep.AsSugarClient().ThenMapperAsync(orders.Items, async o =>
        {
            o.OrderDetails = await _repDetail.AsQueryable()
                .Where(u => !u.IsDelete)
                .Where(u => u.OrderId == o.Id)
                //处理外键和TreeSelector相关字段的连接
                .LeftJoin<Order>((u, orderid) => u.OrderId == orderid.Id)
                .LeftJoin<Device>((u, orderid, deviceid) => u.DeviceId == deviceid.Id)
                .Select((u, orderid, deviceid) => new OrderDetailOutput
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
                    IsEnd = u.IsEnd,
                    Qty = u.Qty,
                    pUnit = u.pUnit,
                    Sort = u.Sort,
                    Remark = u.Remark,
                    CreateUserName = u.CreateUserName,
                    UpdateUserName = u.UpdateUserName,
                }).OrderBuilder(input, "", "Sort", false).ToListAsync();

            foreach (var detail in o.OrderDetails)
            {
                detail.OperatorUsersRealName = string.Join(",", await _rep.Context.Queryable<SysUser>().Where(u => !u.IsDelete && detail.OperatorUsers.Contains(u.Id.ToString()))
                    .Select(u => u.RealName).ToListAsync());
            }
        });
        return orders;
    }

    /// <summary>
    /// 增加订单列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task Add(AddOrderInput input)
    {
        var entity = input.Adapt<Order>();
        var produce = await _rep.Context.Queryable<Produce>()
            .FirstAsync(d => !d.IsDelete && d.Id == input.ProduceId);

        var systemUnit = await _rep.Context.Queryable<SystemUnit>()
            .FirstAsync(s => !s.IsDelete && s.Id == produce.UnitId);

        entity.ProduceCode = produce?.ProduceCode ?? string.Empty;
        entity.ProduceName = produce?.ProduceName ?? string.Empty;
        entity.DeviceTypes = produce?.DeviceTypes ?? string.Empty;
        entity.pUnit = systemUnit?.UnitName ?? string.Empty;
        await _rep.InsertAsync(entity);
    }

    /// <summary>
    /// 删除订单列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteOrderInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _rep.FakeDeleteAsync(entity);   //假删除
        //await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新订单列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateOrderInput input)
    {
        var entity = input.Adapt<Order>();
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
    }



    /// <summary>
    /// 获取未排产订单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<List<OrderOutput>> ListNotPaichanOrderByDeviceId(OrderDetailDeviceInput input)
    {
        var device = await _rep.Context.Queryable<Device>().Where(u => !u.IsDelete && u.Id == input.DeviceId).FirstAsync();

        var query = _rep.AsQueryable()
                .Where(u => !u.IsDelete && !u.IsEnd)
                //处理外键和TreeSelector相关字段的连接
                .LeftJoin<Produce>((u, produceid) => u.ProduceId == produceid.Id)
                .Where((u, produceid) => produceid.DeviceTypes.Contains(device.DeviceTypeId.ToString()) && !produceid.IsDelete)
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
                    ProduceCode = u.ProduceCode,
                    ProduceName = u.ProduceName,
                    ColorRgb = produceid.ColorRgb,
                    IsEnd = u.IsEnd,
                    BatchNumber = u.BatchNumber,
                    Quantity = u.Quantity,
                    pUnit = u.pUnit,
                    Customer = u.Customer,
                    Remark = u.Remark,
                    CreateUserName = u.CreateUserName,
                    UpdateUserName = u.UpdateUserName,
                })
            ;

        var orders = await query.ToListAsync();

        await _rep.AsSugarClient().ThenMapperAsync(orders, async o =>
        {
            var paiOrder = await _repDetail.AsQueryable()
                .Where(u => !u.IsDelete)
                .Where(u => u.OrderId == o.Id)
                .Where(u => u.DeviceTypeId == device.DeviceTypeId).ToListAsync();
            var paiQty = paiOrder.Sum(u => u.Qty);
            var isEnd = paiOrder.Any(od => od.IsEnd);
            o.OrderSurplusQuantity = o.Quantity - (paiQty ?? 0);
            o.DeviceTypeIsEnd = isEnd;

        });
        orders = orders.Where(o => o.OrderSurplusQuantity > 0 && !o.DeviceTypeIsEnd).ToList();
        return orders;
    }


    /// <summary>
    /// 获取订单列表导入模板
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "GetOrderTempExcel")]
    public async Task<IActionResult> GetOrderTempExcel()
    {
        var fileName = "订单列表导入模板.xlsx";
        var filePath = Path.Combine(App.WebHostEnvironment.WebRootPath, "Excel", "Temp");
        if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
        IImporter importer = new ExcelImporter();
        var res = await importer.GenerateTemplate<OrderDto>(Path.Combine(filePath, fileName));
        return new FileStreamResult(new FileStream(res.FileName, FileMode.OpenOrCreate), "application/octet-stream") { FileDownloadName = fileName };
    }

    /// <summary>
    /// 上传Excel导入用户
    /// </summary>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "ImportOrderExcel")]
    public async Task ImportOrderExcel([Required] IFormFile file)
    {
        var newFile = await App.GetRequiredService<SysFileService>().UploadFile(file, "Excel/Import");
        var filePath = Path.Combine(App.WebHostEnvironment.WebRootPath, newFile.FilePath, newFile.Name);
        IImporter importer = new ExcelImporter();
        var res = await importer.Import<OrderDto>(filePath);

        var orderDtos = res.Data;
        var errOrder = new List<OrderExtenDto>();
        foreach (var orderDto in orderDtos)
        {
            var order = orderDto.Adapt<Order>();
            var produce = await _rep.Context.Queryable<Produce>()
                .FirstAsync(d => !d.IsDelete && d.ProduceCode == orderDto.ProduceCode);
            order.ProduceId = produce?.Id ?? 0;
            order.ProduceCode = produce?.ProduceCode ?? string.Empty;
            order.ProduceName = produce?.ProduceName ?? string.Empty;
            order.DeviceTypes = produce?.DeviceTypes ?? string.Empty;
            if (produce != null)
            {
                var systemUnit = await _rep.Context.Queryable<SystemUnit>()
                    .FirstAsync(s => !s.IsDelete && s.Id == produce.UnitId);
                order.pUnit = systemUnit?.UnitName ?? string.Empty;
            }

            if (order.ProduceId == 0)
            {
                orderDto.Remark = "该产品不存在!";
                errOrder.Add(orderDto.Adapt<OrderExtenDto>());
            }
            else
            {
                await _rep.InsertAsync(order);
                if (!string.IsNullOrWhiteSpace(orderDto.DeviceCode))
                {
                    var codes = orderDto.DeviceCode.Split(new[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var deviceIds = await _rep.Context.Queryable<Device>()
                        .Where(d => !d.IsDelete && codes.Contains(d.DeviceCode, true)).Select(d => d.Id).ToListAsync();
                    var odListInput = deviceIds.Select(d => new AddOrderDetailInput
                    {
                        OrderId = order.Id,
                        DeviceId = d,
                        Qty = order.Quantity,
                        pUnit = order.pUnit,
                    }
                    ).ToList();
                    await App.GetRequiredService<OrderDetailService>().Add(odListInput);
                }
            }
        }

        await App.GetRequiredService<SysFileService>().DeleteFile(new DeleteFileInput { Id = newFile.Id });

        if (errOrder.Any())
        {
            IExportFileByTemplate exporter = new ExcelExporter();
            //var result = await exporter.Export($"errorOrderExcel.xlsx", errOrder);
            var tempFileName = Path.Combine(App.WebHostEnvironment.WebRootPath, "Excel", "Temp", "OrderErrorTemplate.xlsx");

            var bytes = await exporter.ExportBytesByTemplate(new OrderDtoTemp { OrderDtos = errOrder }, tempFileName);
            var base64 = Convert.ToBase64String(bytes);
            var errorExcelFile = await App.GetRequiredService<SysFileService>().UploadFileFromBase64(new UploadFileFromBase64Input()
            {
                FileDataBase64 = base64,
                ContentType = "application/vnd.ms-excel",
                FileName = $"errorOrderExcel{DateTime.Now:yyyyMMddHHmmssfff}.xlsx",
                Path = "Excel/ErrorExport"
            });
            var url = errorExcelFile.Url;
            UnifyContext.Fill(new { Message = "订单上传异常,不存在相应的产品!", url, fileName = "错误订单列表.xlsx", count = errOrder.Count });
        }
    }

    /// <summary>
    /// 设置订单完工状态
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "SetOrderEndState")]
    public async Task SetOrderEndState()
    {
        var allOrders = await _rep.AsQueryable().Where(o => !o.IsDelete && !o.IsEnd).ToListAsync();
        foreach (var order in allOrders)
        {
            var produce = await _rep.Context.Queryable<Produce>().Where(p => !p.IsDelete && p.Id == order.ProduceId).FirstAsync();
            if(produce != null)
            {
                var produceDeviceTypeIds = produce.DeviceTypes?.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(dt => Convert.ToInt64(dt)).ToList() ?? [];

                var _dtypeList = order.EndDeviceTypes?.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(ee => Convert.ToInt64(ee)).ToList() ?? [];

                int count = 0;
                foreach (var typeId in produceDeviceTypeIds)
                {
                    var od3 = await _rep.Context.Queryable<OrderDetail>().Where(od =>
                            !od.IsDelete && od.OrderId == order.Id && od.DeviceTypeId == typeId && od.EndDate != null)
                        .ToListAsync();

                    if (od3 != null && od3.Count > 0)
                    {
                        var isEnd = od3.Any(od => od.IsEnd);
                        var sumCount = od3.Sum(od => od.Qty);

                        if (isEnd || sumCount >= order.Quantity)
                        {
                            _dtypeList.Add(typeId);
                            _dtypeList = _dtypeList.Distinct().ToList();
                            count++;
                        }
                    }
                }

                if (_dtypeList.Count > 0) order.EndDeviceTypes = string.Join(",", _dtypeList);
                if (count > 0 && count >= produceDeviceTypeIds.Count) order.IsEnd = true;
                await _rep.Context.Updateable(order)
                    .IgnoreColumns(ignoreAllNullColumns: true, ignoreAllDefaultValue: true).ExecuteCommandAsync();
            }
        }
    }

    /// <summary>
    /// 删除无用工艺列表
    /// </summary>
    /// <param name="deviceTypeIds"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task DeleteDeviceTypes(List<long> deviceTypeIds)
    {
        if(deviceTypeIds != null && deviceTypeIds.Count > 0)
            foreach (var deviceTypeId in deviceTypeIds)
            {
                var oList = await _rep.AsQueryable().Where(o => !o.IsDelete && o.DeviceTypes.Contains(deviceTypeId.ToString())).ToListAsync();
                foreach (var order in oList)
                {
                    var dList = order.DeviceTypes.Split(',', StringSplitOptions.RemoveEmptyEntries).Distinct().OrderBy(d => d).ToList();
                    dList.Remove(deviceTypeId.ToString());
                    order.DeviceTypes = string.Join(',', dList);
                }
                await _rep.AsUpdateable(oList).IgnoreColumns(ignoreAllNullColumns: true, ignoreAllDefaultValue: true).ExecuteCommandAsync();
            }
    }

    /// <summary>
    /// 获取订单列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<Order> Get([FromQuery] QueryByIdOrderInput input)
    {
        return await _rep.GetFirstAsync(u => !u.IsDelete && u.Id == input.Id);
    }

    /// <summary>
    /// 获取订单列表列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<OrderOutput>> List([FromQuery] OrderInput input)
    {
        return await _rep.AsQueryable().Where(u => !u.IsDelete).Select<OrderOutput>().ToListAsync();
    }

    /// <summary>
    /// 获取产品选择列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "ProduceProduceIdDropdown"), HttpGet]
    public async Task<dynamic> ProduceProduceIdDropdown()
    {
        return await _rep.Context.Queryable<Produce>().Where(u => !u.IsDelete)
                .Select(u => new
                {
                    Label = u.ProduceCode,
                    Text = u.ProduceName,
                    Unit = u.pUnit,
                    Value = u.Id
                }
                ).ToListAsync();
    }




}

