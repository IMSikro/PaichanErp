using Admin.NET.Core;
using Admin.NET.Paichan.Const;
using Admin.NET.Paichan.Entity;
using Furion;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Admin.NET.Paichan;
/// <summary>
/// 仓库服务
/// </summary>
[ApiDescriptionSettings(PaichanConst.GroupName, Order = 100)]
public class StoreService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<Store> _rep;
    public StoreService(SqlSugarRepository<Store> rep)
    {
        _rep = rep;
    }

    /// <summary>
    /// 分页查询仓库
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<SqlSugarPagedList<StoreOutput>> Page(StoreInput input)
    {
        var query= _rep.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.StoreCode.Contains(input.SearchKey.Trim())
                || u.StoreName.Contains(input.SearchKey.Trim())
                || u.StoreLocation.Contains(input.SearchKey.Trim())
            )
            .WhereIF(!string.IsNullOrWhiteSpace(input.StoreCode), u => u.StoreCode.Contains(input.StoreCode.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.StoreName), u => u.StoreName.Contains(input.StoreName.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.StoreLocation), u => u.StoreLocation.Contains(input.StoreLocation.Trim()))
            .Select<StoreOutput>()
;
        query = query.OrderBuilder(input, "", "CreateTime");

        var output = await query.ToPagedListAsync(input.Page, input.PageSize);

        await _rep.AsSugarClient().ThenMapperAsync(output.Items, async o =>
        {
            o.StoreLocations = await _rep.Context.Queryable<StoreLocation>()
                .Where(u => !u.IsDelete)
                .Where(u => u.StoreId == o.Id)
                //处理外键和TreeSelector相关字段的连接
                .LeftJoin<Store>((u, storeid) => u.StoreId == storeid.Id)
                .Select<StoreLocationOutput>().ToListAsync();

        });
        return output;
    }

    /// <summary>
    /// 增加仓库
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    public async Task Add(AddStoreInput input)
    {
        var entity = input.Adapt<Store>();
        await _rep.InsertAsync(entity);

        var storeId = entity.Id;
        foreach (var location in input.StoreLocations ?? new List<StoreLocationBaseInput>())
        {
            var lo = location.Adapt<AddStoreLocationInput>();
            lo.StoreId = storeId;
            lo.StoreCode = input.StoreCode;
            lo.StoreName = input.StoreName;

            await App.GetService<StoreLocationService>().Add(lo);
        }
    }

    /// <summary>
    /// 删除仓库
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    public async Task Delete(DeleteStoreInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _rep.FakeDeleteAsync(entity);   //假删除
        //await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新仓库
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    public async Task Update(UpdateStoreInput input)
    {
        var entity = input.Adapt<Store>();
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();

        var storeId = entity.Id;
        foreach (var location in input.StoreLocations ?? new List<UpdateStoreLocationInput>())
        {
            if (location.Id > 0)
            {
                location.StoreId = storeId;
                location.StoreCode = input.StoreCode;
                location.StoreName = input.StoreName;

                await App.GetService<StoreLocationService>().Update(location);
            }
            else
            {
                var lo = location.Adapt<AddStoreLocationInput>();
                lo.StoreId = storeId;
                lo.StoreCode = input.StoreCode;
                lo.StoreName = input.StoreName;

                await App.GetService<StoreLocationService>().Add(lo);
            }
        }
    }

    /// <summary>
    /// 获取仓库
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<Store> Get([FromQuery] QueryByIdStoreInput input)
    {
        return await _rep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 获取仓库列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<StoreOutput>> List([FromQuery] StoreInput input)
    {
        return await _rep.AsQueryable().Select<StoreOutput>().ToListAsync();
    }





}

