using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

    /// <summary>
    /// 设备类型基础输入参数
    /// </summary>
    public class DeviceTypeBaseInput
    {
        /// <summary>
        /// 设备类型
        /// </summary>
        public virtual string TypeName { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string? Remark { get; set; }
        
        /// <summary>
        /// 创建者姓名
        /// </summary>
        public virtual string? CreateUserName { get; set; }
        
        /// <summary>
        /// 修改者姓名
        /// </summary>
        public virtual string? UpdateUserName { get; set; }
        
    }

    /// <summary>
    /// 设备类型分页查询输入参数
    /// </summary>
    public class DeviceTypeInput : BasePageInput
    {
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string? SearchKey { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string? TypeName { get; set; }
        
    }

    /// <summary>
    /// 设备类型增加输入参数
    /// </summary>
    public class AddDeviceTypeInput : DeviceTypeBaseInput
    {
        /// <summary>
        /// 设备类型
        /// </summary>
        [Required(ErrorMessage = "设备类型不能为空")]
        public override string TypeName { get; set; }
        
    }

    /// <summary>
    /// 设备类型删除输入参数
    /// </summary>
    public class DeleteDeviceTypeInput : BaseIdInput
    {
    }

    /// <summary>
    /// 设备类型更新输入参数
    /// </summary>
    public class UpdateDeviceTypeInput : DeviceTypeBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// 设备类型主键查询输入参数
    /// </summary>
    public class QueryByIdDeviceTypeInput : DeleteDeviceTypeInput
    {

    }
