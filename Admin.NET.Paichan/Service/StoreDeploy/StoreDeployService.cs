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
/// 库存调拨服务
/// </summary>
[ApiDescriptionSettings(PaichanConst.GroupName, Order = 100)]
public class StoreDeployService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<StoreDeploy> _rep;
    public StoreDeployService(SqlSugarRepository<StoreDeploy> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询库存调拨
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<StoreDeployOutput>> Page(StoreDeployInput input)
    {
        var query= _rep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.StoreDeployCode.Contains(input.SearchKey.Trim())
            )
            .WhereIF(!string.IsNullOrWhiteSpace(input.StoreDeployCode), u => u.StoreDeployCode.Contains(input.StoreDeployCode.Trim()))
            .WhereIF(input.ProduceId>0, u => u.ProduceId == input.ProduceId)
            .WhereIF(input.OutStoreId>0, u => u.OutStoreId == input.OutStoreId)
            .WhereIF(input.InStoreId>0, u => u.InStoreId == input.InStoreId)
            //处理外键和TreeSelector相关字段的连接
            .LeftJoin<Produce>((u, produceid) => u.ProduceId == produceid.Id )
            .LeftJoin<Store>((u, produceid, outstoreid) => u.OutStoreId == outstoreid.Id )
            .LeftJoin<Store>((u, produceid, outstoreid, instoreid) => u.InStoreId == instoreid.Id )
            .Select((u, produceid, outstoreid, instoreid)=> new StoreDeployOutput{
                Id = u.Id, 
                StoreDeployCode = u.StoreDeployCode, 
                StoreDeployDate = u.StoreDeployDate, 
                StoreDeployNumber = u.StoreDeployNumber, 
                ProduceId = u.ProduceId, 
                ProduceIdProduceCode = produceid.ProduceCode,
                ProduceCode = u.ProduceCode, 
                ProduceName = u.ProduceName, 
                OutStoreId = u.OutStoreId, 
                OutStoreIdStoreCode = outstoreid.StoreCode,
                OutStoreCode = u.OutStoreCode, 
                OutStoreName = u.OutStoreName, 
                OutStoreLocation = u.OutStoreLocation, 
                InStoreId = u.InStoreId, 
                InStoreIdStoreCode = instoreid.StoreCode,
                InStoreCode = u.InStoreCode, 
                InStoreName = u.InStoreName, 
                InStoreLocation = u.InStoreLocation, 
                Remark = u.Remark, 
                CreateUserName = u.CreateUserName, 
                UpdateUserName = u.UpdateUserName, 
            })
;
        if(input.StoreDeployDateRange != null && input.StoreDeployDateRange.Count >0)
        {
            DateTime? start= input.StoreDeployDateRange[0]; 
            query = query.WhereIF(start.HasValue, u => u.StoreDeployDate > start);
            if (input.StoreDeployDateRange.Count >1 && input.StoreDeployDateRange[1].HasValue)
            {
                var end = input.StoreDeployDateRange[1].Value.AddDays(1);
                query = query.Where(u => u.StoreDeployDate < end);
            }
        } 
        query = query.OrderBuilder(input, "u.", "CreateTime");
        return await query.ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加库存调拨
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task Add(AddStoreDeployInput input)
    {
        var entity = input.Adapt<StoreDeploy>();
        var produce = await _rep.Context.Queryable<Produce>().FirstAsync(wa => wa.Id == input.ProduceId);
        entity.ProduceCode = produce.ProduceCode;
        entity.ProduceName = produce.ProduceName;
        var outStore = await _rep.Context.Queryable<Store>().FirstAsync(wa => wa.Id == input.OutStoreId);
        entity.OutStoreCode = outStore.StoreCode;
        entity.OutStoreName = outStore.StoreName;
        entity.OutStoreLocation = outStore.StoreLocation;
        var inStore = await _rep.Context.Queryable<Store>().FirstAsync(wa => wa.Id == input.InStoreId);
        entity.InStoreCode = inStore.StoreCode;
        entity.InStoreName = inStore.StoreName;
        entity.InStoreLocation = inStore.StoreLocation;
        await _rep.InsertAsync(entity);
    }

    /// <summary>
    /// 删除库存调拨
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteStoreDeployInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _rep.FakeDeleteAsync(entity);   //假删除
        //await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新库存调拨
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateStoreDeployInput input)
    {
        var entity = input.Adapt<StoreDeploy>();
        var produce = await _rep.Context.Queryable<Produce>().FirstAsync(wa => wa.Id == input.ProduceId);
        entity.ProduceCode = produce.ProduceCode;
        entity.ProduceName = produce.ProduceName;
        var outStore = await _rep.Context.Queryable<Store>().FirstAsync(wa => wa.Id == input.OutStoreId);
        entity.OutStoreCode = outStore.StoreCode;
        entity.OutStoreName = outStore.StoreName;
        entity.OutStoreLocation = outStore.StoreLocation;
        var inStore = await _rep.Context.Queryable<Store>().FirstAsync(wa => wa.Id == input.InStoreId);
        entity.InStoreCode = inStore.StoreCode;
        entity.InStoreName = inStore.StoreName;
        entity.InStoreLocation = inStore.StoreLocation;
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取库存调拨
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<StoreDeploy> Get([FromQuery] QueryByIdStoreDeployInput input)
    {
        return await _rep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 获取库存调拨列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<StoreDeployOutput>> List([FromQuery] StoreDeployInput input)
    {
        return await _rep.AsQueryable().Select<StoreDeployOutput>().ToListAsync();
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
    /// 获取调出仓库列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "StoreOutStoreIdDropdown"), HttpGet]
    public async Task<dynamic> StoreOutStoreIdDropdown()
    {
        return await _rep.Context.Queryable<Store>()
                .Select(u => new
                {
                    Label = u.StoreCode,
                    Value = u.Id
                }
                ).ToListAsync();
    }
    /// <summary>
    /// 获取调入仓库列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "StoreInStoreIdDropdown"), HttpGet]
    public async Task<dynamic> StoreInStoreIdDropdown()
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

