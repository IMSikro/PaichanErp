using System.ComponentModel.DataAnnotations;
using Admin.NET.Core;
using Admin.NET.Core.Service;
using Admin.NET.Paichan.Const;
using Admin.NET.Paichan.Entity;
using Furion;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Mapster;
using Microsoft.AspNetCore.Http;
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

        List<long> groupDeviceIds = [];
        if (input.GroupId > 0)
            groupDeviceIds = (await _rep.Context.Queryable<DeviceGroup>()
                .FirstAsync(wa => !wa.IsDelete && wa.Id == input.GroupId))?.DeviceIds?.Split(',').Distinct().ToList().Select(wa => Convert.ToInt64(wa)).ToList() ?? [];
        var query= _rep.AsQueryable().Where(u => !u.IsDelete)
            .WhereIF(!string.IsNullOrWhiteSpace(input.SearchKey), u =>
                u.DeviceCode.Contains(input.SearchKey.Trim())
                || u.DeviceName.Contains(input.SearchKey.Trim())
            )
            .WhereIF(input.DeviceTypeId>0, u => u.DeviceTypeId == input.DeviceTypeId)
            .WhereIF(!string.IsNullOrWhiteSpace(input.DeviceCode), u => u.DeviceCode.Contains(input.DeviceCode.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.DeviceName), u => u.DeviceName.Contains(input.DeviceName.Trim()))
            .WhereIF(input.GroupId > 0, u => groupDeviceIds.Contains(u.Id))
            //处理外键和TreeSelector相关字段的连接
            .LeftJoin<DeviceType>((u, devicetypeid) => u.DeviceTypeId == devicetypeid.Id )
            .Select((u, devicetypeid)=> new DeviceOutput{
                Id = u.Id, 
                DeviceTypeId = u.DeviceTypeId, 
                DeviceTypeIdTypeName = devicetypeid.TypeName,
                DeviceCode = u.DeviceCode, 
                DeviceName = u.DeviceName, 
                DeviceCoefficient = u.DeviceCoefficient, 
                OperatorUsers = u.OperatorUsers,
                Sort = u.Sort,
                Remark = u.Remark, 
                CreateUserName = u.CreateUserName, 
                UpdateUserName = u.UpdateUserName, 
            })
;
        query = query.OrderBuilder(input, "u.", "Sort",false);
        var deviceList = await query.ToPagedListAsync(input.Page, input.PageSize);

        await _rep.AsSugarClient().ThenMapperAsync(deviceList.Items, async o =>
        {
            o.OperatorUsersRealName = string.Join(",", await _rep.Context.Queryable<SysUser>().Where(u => !u.IsDelete && o.OperatorUsers.Contains(u.Id.ToString()))
                .Select(u => u.RealName).ToListAsync());
        });

        return deviceList;
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

        var device = await _rep.AsQueryable().FirstAsync(d => d.Id == input.Id);
        await _rep.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true,ignoreAllDefaultValue:true).ExecuteCommandAsync();

        if (!string.IsNullOrWhiteSpace(input.OperatorUsers) && input.OperatorUsers != device.OperatorUsers)
        {
            var odlist = await _rep.Context.Queryable<OrderDetail>()
                .Where(od => !od.IsDelete && od.EndDate == null && od.DeviceId == device.Id).ToListAsync();
            foreach (var od in odlist)
            {
                od.OperatorUsers = input.OperatorUsers;
            }

            await _rep.Context.Updateable<OrderDetail>(odlist)
                .IgnoreColumns(ignoreAllNullColumns: true, ignoreAllDefaultValue: true).ExecuteCommandAsync();
        }

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
    /// 获取设备列表导入模板
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ApiDescriptionSettings(Name = "GetDeviceTempExcel")]
    public async Task<IActionResult> GetDeviceTempExcel()
    {
        var fileName = "设备列表导入模板.xlsx";
        var filePath = Path.Combine(App.WebHostEnvironment.WebRootPath, "Excel", "Temp");
        if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
        IImporter importer = new ExcelImporter();
        var res = await importer.GenerateTemplate<DeviceDto>(Path.Combine(filePath, fileName));
        return new FileStreamResult(new FileStream(res.FileName, FileMode.OpenOrCreate), "application/octet-stream") { FileDownloadName = fileName };
    }

    /// <summary>
    /// 上传Excel导入设备
    /// </summary>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "ImportDeviceExcel")]
    public async Task ImportDeviceExcel([Required] IFormFile file)
    {
        var newFile = await App.GetRequiredService<SysFileService>().UploadFile(file, "Excel/Import");
        var filePath = Path.Combine(App.WebHostEnvironment.WebRootPath, newFile.FilePath, newFile.Name);
        IImporter importer = new ExcelImporter();
        var res = await importer.Import<DeviceDto>(filePath);

        var deviceDtos = res.Data;
        var random = new Random();
        foreach (var deviceDto in deviceDtos)
        {
            var device = deviceDto.Adapt<Device>();

            device.DeviceTypeId = (await _rep.Context.Queryable<DeviceType>()
                .FirstAsync(d => !d.IsDelete && d.TypeName == deviceDto.DeviceTypeName))?.Id ?? 0;

            await _rep.InsertAsync(device);
        }

        await App.GetRequiredService<SysFileService>().DeleteFile(new DeleteFileInput { Id = newFile.Id });
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
        return await _rep.Context.Queryable<DeviceType>().Where(u => !u.IsDelete).OrderBy(u => u.Sort)
                .Select(u => new
                {
                    Label = u.TypeName,
                    Value = u.Id,
                    u.NormalType
                }
                ).ToListAsync();
    }




}

