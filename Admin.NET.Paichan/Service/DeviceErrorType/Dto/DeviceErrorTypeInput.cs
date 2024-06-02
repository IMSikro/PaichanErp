using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

    /// <summary>
    /// 非生产类型基础输入参数
    /// </summary>
    public class DeviceErrorTypeBaseInput
    {
        /// <summary>
        /// 非生产时间类型名称
        /// </summary>
        public virtual string ErrorTypeName { get; set; }
        
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
    /// 非生产类型分页查询输入参数
    /// </summary>
    public class DeviceErrorTypeInput : BasePageInput
    {
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string? SearchKey { get; set; }

        /// <summary>
        /// 非生产时间类型名称
        /// </summary>
        public string? ErrorTypeName { get; set; }
        
    }

    /// <summary>
    /// 非生产类型增加输入参数
    /// </summary>
    public class AddDeviceErrorTypeInput : DeviceErrorTypeBaseInput
    {
        /// <summary>
        /// 非生产时间类型名称
        /// </summary>
        [Required(ErrorMessage = "非生产时间类型名称不能为空")]
        public override string ErrorTypeName { get; set; }
        
    }

    /// <summary>
    /// 非生产类型删除输入参数
    /// </summary>
    public class DeleteDeviceErrorTypeInput : BaseIdInput
    {
    }

    /// <summary>
    /// 非生产类型更新输入参数
    /// </summary>
    public class UpdateDeviceErrorTypeInput : DeviceErrorTypeBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// 非生产类型主键查询输入参数
    /// </summary>
    public class QueryByIdDeviceErrorTypeInput : DeleteDeviceErrorTypeInput
    {

    }
