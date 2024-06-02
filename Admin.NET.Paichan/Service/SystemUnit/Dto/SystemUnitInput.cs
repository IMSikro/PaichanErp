using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

    /// <summary>
    /// 计量单位基础输入参数
    /// </summary>
    public class SystemUnitBaseInput
    {
        /// <summary>
        /// 计量单位
        /// </summary>
        public virtual string UnitName { get; set; }
        
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
    /// 计量单位分页查询输入参数
    /// </summary>
    public class SystemUnitInput : BasePageInput
    {
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string? SearchKey { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string? UnitName { get; set; }
        
    }

    /// <summary>
    /// 计量单位增加输入参数
    /// </summary>
    public class AddSystemUnitInput : SystemUnitBaseInput
    {
        /// <summary>
        /// 计量单位
        /// </summary>
        [Required(ErrorMessage = "计量单位不能为空")]
        public override string UnitName { get; set; }
        
    }

    /// <summary>
    /// 计量单位删除输入参数
    /// </summary>
    public class DeleteSystemUnitInput : BaseIdInput
    {
    }

    /// <summary>
    /// 计量单位更新输入参数
    /// </summary>
    public class UpdateSystemUnitInput : SystemUnitBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// 计量单位主键查询输入参数
    /// </summary>
    public class QueryByIdSystemUnitInput : DeleteSystemUnitInput
    {

    }
