using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

    /// <summary>
    /// 工艺标准项基础输入参数
    /// </summary>
    public class ProcessProjectBaseInput
    {
        /// <summary>
        /// 工艺项目编号
        /// </summary>
        public virtual string ProcessProjectCode { get; set; }
        
        /// <summary>
        /// 工艺项目名称
        /// </summary>
        public virtual string ProcessProjectName { get; set; }
        
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
    /// 工艺标准项分页查询输入参数
    /// </summary>
    public class ProcessProjectInput : BasePageInput
    {
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string? SearchKey { get; set; }

        /// <summary>
        /// 工艺项目编号
        /// </summary>
        public string? ProcessProjectCode { get; set; }
        
        /// <summary>
        /// 工艺项目名称
        /// </summary>
        public string? ProcessProjectName { get; set; }
        
    }

    /// <summary>
    /// 工艺标准项增加输入参数
    /// </summary>
    public class AddProcessProjectInput : ProcessProjectBaseInput
    {
        /// <summary>
        /// 工艺项目编号
        /// </summary>
        [Required(ErrorMessage = "工艺项目编号不能为空")]
        public override string ProcessProjectCode { get; set; }
        
        /// <summary>
        /// 工艺项目名称
        /// </summary>
        [Required(ErrorMessage = "工艺项目名称不能为空")]
        public override string ProcessProjectName { get; set; }
        
    }

    /// <summary>
    /// 工艺标准项删除输入参数
    /// </summary>
    public class DeleteProcessProjectInput : BaseIdInput
    {
    }

    /// <summary>
    /// 工艺标准项更新输入参数
    /// </summary>
    public class UpdateProcessProjectInput : ProcessProjectBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// 工艺标准项主键查询输入参数
    /// </summary>
    public class QueryByIdProcessProjectInput : DeleteProcessProjectInput
    {

    }
