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
/// 生产入库单服务
/// </summary>
[ApiDescriptionSettings(PaichanConst.GroupName, Order = 100)]
public class ProduceStoreRecordsService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<ProduceStoreRecords> _rep;
    public ProduceStoreRecordsService(SqlSugarRepository<ProduceStoreRecords> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询生产入库单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<ProduceStoreRecordsOutput>> Page(ProduceStoreRecordsInput input)
    {
        var query= _rep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.BatchNumber.Contains(input.SearchKey.Trim())
            )
            .WhereIF(input.OrderId>0, u => u.OrderId == input.OrderId)
            .WhereIF(!string.IsNullOrWhiteSpace(input.BatchNumber), u => u.BatchNumber.Contains(input.BatchNumber.Trim()))
            .WhereIF(input.ProduceId>0, u => u.ProduceId == input.ProduceId)
            .WhereIF(input.StoreId>0, u => u.StoreId == input.StoreId)
            //处理外键和TreeSelector相关字段的连接
            .LeftJoin<Order>((u, orderid) => u.OrderId == orderid.Id )
            .LeftJoin<Order>((u, orderid, produceid) => u.ProduceId == produceid.Id )
            .Select((u, orderid, produceid)=> new ProduceStoreRecordsOutput{
                Id = u.Id, 
                OrderId = u.OrderId, 
                OrderIdBatchNumber = orderid.BatchNumber,
                BatchNumber = u.BatchNumber, 
                Quantity = u.Quantity, 
                RealQuantity = u.RealQuantity, 
                BatchCount = u.BatchCount, 
                ProduceId = u.ProduceId, 
                ProduceIdOrderCode = produceid.OrderCode,
                ProduceCode = u.ProduceCode, 
                ProduceName = u.ProduceName, 
                StoreId = u.StoreId, 
                StoreCode = u.StoreCode, 
                StoreName = u.StoreName, 
                StoreLocation = u.StoreLocation, 
                TotalPrice = u.TotalPrice, 
                InStoreDate = u.InStoreDate, 
                Remark = u.Remark, 
                CreateUserName = u.CreateUserName, 
                UpdateUserName = u.UpdateUserName, 
            })
;
        if(input.InStoreDateRange != null && input.InStoreDateRange.Count >0)
        {
            DateTime? start= input.InStoreDateRange[0]; 
            query = query.WhereIF(start.HasValue, u => u.InStoreDate > start);
            if (input.InStoreDateRange.Count >1 && input.InStoreDateRange[1].HasValue)
            {
                var end = input.InStoreDateRange[1].Value.AddDays(1);
                query = query.Where(u => u.InStoreDate < end);
            }
        } 
        query = query.OrderBuilder(input, "u.", "CreateTime");
        return await query.ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加生产入库单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task Add(AddProduceStoreRecordsInput input)
    {
        var entity = input.Adapt<ProduceStoreRecords>();
        var order = await _rep.Context.Queryable<Order>().FirstAsync(wa => wa.Id == input.OrderId);
        entity.BatchNumber = order.BatchNumber;
        entity.Quantity = order.Quantity;
        entity.ProduceId = order.ProduceId;
        entity.ProduceCode = order.ProduceCode;
        entity.ProduceName = order.ProduceName;
        var store = await _rep.Context.Queryable<Store>().FirstAsync(wa => wa.Id == input.StoreId);
        entity.StoreCode = store.StoreCode;
        entity.StoreName = store.StoreName;
        entity.StoreLocation = store.StoreLocation;

        await _rep.InsertAsync(entity);
    }

    /// <summary>
    /// 删除生产入库单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteProduceStoreRecordsInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _rep.FakeDeleteAsync(entity);   //假删除
        //await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新生产入库单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateProduceStoreRecordsInput input)
    {
        var entity = input.Adapt<ProduceStoreRecords>();
        var order = await _rep.Context.Queryable<Order>().FirstAsync(wa => wa.Id == input.OrderId);
        entity.BatchNumber = order.BatchNumber;
        entity.Quantity = order.Quantity;
        entity.ProduceId = order.ProduceId;
        entity.ProduceCode = order.ProduceCode;
        entity.ProduceName = order.ProduceName;
        var store = await _rep.Context.Queryable<Store>().FirstAsync(wa => wa.Id == input.StoreId);
        entity.StoreCode = store.StoreCode;
        entity.StoreName = store.StoreName;
        entity.StoreLocation = store.StoreLocation;
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取生产入库单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<ProduceStoreRecords> Get([FromQuery] QueryByIdProduceStoreRecordsInput input)
    {
        return await _rep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 获取生产入库单列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<ProduceStoreRecordsOutput>> List([FromQuery] ProduceStoreRecordsInput input)
    {
        return await _rep.AsQueryable().Select<ProduceStoreRecordsOutput>().ToListAsync();
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
                .Select(u => new
                {
                    Label = u.BatchNumber,
                    Value = u.Id
                }
                ).ToListAsync();
    }
    /// <summary>
    /// 获取产品列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "OrderProduceIdDropdown"), HttpGet]
    public async Task<dynamic> OrderProduceIdDropdown()
    {
        return await _rep.Context.Queryable<Order>()
                .Select(u => new
                {
                    Label = u.OrderCode,
                    Value = u.Id
                }
                ).ToListAsync();
    }




}

