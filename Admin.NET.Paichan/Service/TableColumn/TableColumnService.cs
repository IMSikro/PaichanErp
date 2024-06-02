using Admin.NET.Core;
using Admin.NET.Paichan.Const;
using Admin.NET.Paichan.Entity;
using AngleSharp.Dom;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Admin.NET.Paichan;
/// <summary>
/// 列表展示服务
/// </summary>
[ApiDescriptionSettings(PaichanConst.GroupName, Order = 100)]
public class TableColumnService : IDynamicApiController, ITransient
{
    private readonly SqlSugarRepository<TableColumn> _rep;
    private readonly UserManager _userManager;

    private List<TableColumnOutput> _output;

    public TableColumnService(UserManager userManager, SqlSugarRepository<TableColumn> rep)
    {
        _userManager = userManager;
        _rep = rep;
        InitOutput();
    }

    private void InitOutput()
    {
        _output = new List<TableColumnOutput>
        {
            new(){ Lable = "颜色", Prop = "colorRgb", IsHidden = false,PageType = "控制台",Width = "120"},
            new(){ Lable = "产品编号", Prop = "produceIdProduceName", IsHidden = false,PageType = "控制台",Width = "120"},
            new(){ Lable = "数量", Prop = "qty", IsHidden = false,PageType = "控制台",Width = "120"},
            new(){ Lable = "交期", Prop = "deliveryDate", IsHidden = false,PageType = "控制台",Width = "120"},
            new(){ Lable = "单位", Prop = "pUnit", IsHidden = true,PageType = "控制台",Width = "120"},
            new(){ Lable = "订单批号", Prop = "orderIdBatchNumber", IsHidden = true,PageType = "控制台",Width = "120"},
            new(){ Lable = "排产序号", Prop = "orderDetailCode", IsHidden = true,PageType = "控制台",Width = "120"},
            new(){ Lable = "设备编号", Prop = "deviceIdDeviceCode", IsHidden = true,PageType = "控制台",Width = "120"},
            new(){ Lable = "操作人员", Prop = "operatorUsersRealName", IsHidden = true,PageType = "控制台",Width = "120"},
        };
    }

    /// <summary>
    /// 查询列表展示
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Page")]
    public async Task<List<TableColumnOutput>> Page(QueryTableColumnInput input)
    {
        if (string.IsNullOrWhiteSpace(input.PageType)) input.PageType = "控制台";
        var query = _rep.AsQueryable()
                .Where(u => u.PageType.Contains(input.PageType.Trim()))
                .WhereIF(!string.IsNullOrWhiteSpace(input.Prop), u => u.Prop.Contains(input.Prop.Trim()))
                .WhereIF(!string.IsNullOrWhiteSpace(input.Lable), u => u.Lable.Contains(input.Lable.Trim()))
                .WhereIF(_userManager.SuperAdmin, u => u.TenantId == _userManager.TenantId)
                .Select<TableColumnOutput>()
            ;
        query = query.OrderBy(t => t.Sort);
        if (!await query.AnyAsync()) return _output;
        return await query.ToListAsync();
    }

    /// <summary>
    /// 增加列表展示
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Add")]
    [Obsolete]
    public async Task Add(AddTableColumnInput input)
    {
        var entity = input.Adapt<TableColumn>();
        await _rep.InsertAsync(entity);
    }

    /// <summary>
    /// 删除列表展示
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Delete")]
    [Obsolete]
    public async Task Delete(DeleteTableColumnInput input)
    {
        var entity = await _rep.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _rep.FakeDeleteAsync(entity);   //假删除
        //await _rep.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 更新列表展示
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "UpdateList")]
    public async Task UpdateList(List<UpdateTableColumnsInput> input)
    {
        var result = _rep.AsQueryable().Where(tc => tc.PageType == "控制台");
        if (_userManager.SuperAdmin) result.Where(tc => _userManager.TenantId == tc.TenantId);
        var list = await result.ToListAsync();
        if (list.Count == 0)
        {
            var data = _output.Adapt<List<TableColumn>>();
            await _rep.InsertRangeAsync(data);
        }
        else
        {
            foreach (var inp in input)
            {
                var entity = list.FirstOrDefault(tc => tc.Prop == inp.Prop);
                if (entity != null)
                {
                    entity.Lable = inp.Lable;
                    entity.IsHidden = inp.IsHidden;
                    entity.Sort = inp.Sort;
                    entity.Width = inp.Width;
                }
            }
            await _rep.AsUpdateable(list).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
        }
        
    }

    /// <summary>
    /// 重置列表展示
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Reset")]
    public async Task Reset()
    {
        var result = _rep.AsQueryable().Where(tc => tc.PageType == "控制台");
        if (_userManager.SuperAdmin) result.Where(tc => _userManager.TenantId == tc.TenantId);
        var list = await result.ToListAsync();
        if (list.Count == 0)
        {
            var data = _output.Adapt<List<TableColumn>>();
            await _rep.InsertRangeAsync(data);
        }
        else
        {
            foreach (var inp in _output)
            {
                var entity = list.FirstOrDefault(tc => tc.Prop == inp.Prop);
                if (entity != null)
                {
                    entity.Lable = inp.Lable;
                    entity.IsHidden = inp.IsHidden;
                    entity.Sort = inp.Sort;
                    entity.Width = inp.Width;
                }
            }
            await _rep.AsUpdateable(list).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
        }
    }

    /// <summary>
    /// 更新列表展示
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiDescriptionSettings(Name = "Update")]
    [Obsolete]
    public async Task Update(UpdateTableColumnInput input)
    {
        var entity = input.Adapt<TableColumn>();
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true, ignoreAllDefaultValue: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取列表展示
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "Detail")]
    public async Task<TableColumn> Get([FromQuery] QueryByIdTableColumnInput input)
    {
        return await _rep.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 获取列表展示列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "List")]
    public async Task<List<TableColumnOutput>> List([FromQuery] TableColumnInput input)
    {
        return await _rep.AsQueryable().Select<TableColumnOutput>().ToListAsync();
    }





}

