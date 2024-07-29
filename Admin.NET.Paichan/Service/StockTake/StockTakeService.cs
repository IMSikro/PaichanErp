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
/// 库存盘点服务
/// </summary>
[ApiDescriptionSettings(PaichanConst.GroupName, Order = 100)]
public class StockTakeService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<StockTake> _rep;
    public StockTakeService(SqlSugarRepository<StockTake> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询库存盘点
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<StockTakeOutput>> Page(StockTakeInput input)
    {
        var query= _rep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.StockTakeCode.Contains(input.SearchKey.Trim())
            )
            .WhereIF(!string.IsNullOrWhiteSpace(input.StockTakeCode), u => u.StockTakeCode.Contains(input.StockTakeCode.Trim()))
            .WhereIF(input.ProduceId>0, u => u.ProduceId == input.ProduceId)
            .WhereIF(input.StoreId>0, u => u.StoreId == input.StoreId)
            //处理外键和TreeSelector相关字段的连接
            .LeftJoin<Produce>((u, produceid) => u.ProduceId == produceid.Id )
            .LeftJoin<Store>((u, produceid, storeid) => u.StoreId == storeid.Id )
            .Select((u, produceid, storeid)=> new StockTakeOutput{
                Id = u.Id, 
                StockTakeCode = u.StockTakeCode, 
                StockTakeDate = u.StockTakeDate, 
                StockTakeNumber = u.StockTakeNumber, 
                StockTakeDiffNumber = u.StockTakeDiffNumber, 
                ProduceId = u.ProduceId, 
                ProduceIdProduceCode = produceid.ProduceCode,
                ProduceCode = u.ProduceCode, 
                ProduceName = u.ProduceName, 
                StoreId = u.StoreId, 
                StoreIdStoreCode = storeid.StoreCode,
                StoreCode = u.StoreCode, 
                StoreName = u.StoreName, 
                StoreLocation = u.StoreLocation, 
                Remark = u.Remark, 
                CreateUserName = u.CreateUserName, 
                UpdateUserName = u.UpdateUserName, 
            })
;
        if(input.StockTakeDateRange != null && input.StockTakeDateRange.Count >0)
        {
            DateTime? start= input.StockTakeDateRange[0]; 
            query = query.WhereIF(start.HasValue, u => u.StockTakeDate > start);
            if (input.StockTakeDateRange.Count >1 && input.StockTakeDateRange[1].HasValue)
            {
                var end = input.StockTakeDateRange[1].Value.AddDays(1);
                query = query.Where(u => u.StockTakeDate < end);
            }
        } 
        query = query.OrderBuilder(input, "u.", "CreateTime");
        return await query.ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加库存盘点
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task Add(AddStockTakeInput input)
    {
        var entity = input.Adapt<StockTake>();
        var produce = await _rep.Context.Queryable<Produce>().FirstAsync(wa => wa.Id == input.ProduceId);
        entity.ProduceCode = produce.ProduceCode;
        entity.ProduceName = produce.ProduceName;
        var store = await _rep.Context.Queryable<Store>().FirstAsync(wa => wa.Id == input.StoreId);
        entity.StoreCode = store.StoreCode;
        entity.StoreName = store.StoreName;
        await _rep.InsertAsync(entity);
    }

    /// <summary>
    /// 删除库存盘点
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteStockTakeInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _rep.FakeDeleteAsync(entity);   //假删除
        //await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新库存盘点
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateStockTakeInput input)
    {
        var entity = input.Adapt<StockTake>();
        var produce = await _rep.Context.Queryable<Produce>().FirstAsync(wa => wa.Id == input.ProduceId);
        entity.ProduceCode = produce.ProduceCode;
        entity.ProduceName = produce.ProduceName;
        var store = await _rep.Context.Queryable<Store>().FirstAsync(wa => wa.Id == input.StoreId);
        entity.StoreCode = store.StoreCode;
        entity.StoreName = store.StoreName;
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取库存盘点
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<StockTake> Get([FromQuery] QueryByIdStockTakeInput input)
    {
        return await _rep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 获取库存盘点列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<StockTakeOutput>> List([FromQuery] StockTakeInput input)
    {
        return await _rep.AsQueryable().Select<StockTakeOutput>().ToListAsync();
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
    /// 获取仓库列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "StoreStoreIdDropdown"), HttpGet]
    public async Task<dynamic> StoreStoreIdDropdown()
    {
        return await _rep.Context.Queryable<Store>()
                .Select(u => new
                {
                    Label = u.StoreCode,
                    Value = u.Id
                }
                ).ToListAsync();
    }




}

