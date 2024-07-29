using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

    /// <summary>
    /// 检验标准项基础输入参数
    /// </summary>
    public class ExamineProjectBaseInput
    {
        /// <summary>
        /// 检验项目编号
        /// </summary>
        public virtual string ExamineProjectCode { get; set; }
        
        /// <summary>
        /// 检验项目名称
        /// </summary>
        public virtual string ExamineProjectName { get; set; }
        
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
    /// 检验标准项分页查询输入参数
    /// </summary>
    public class ExamineProjectInput : BasePageInput
    {
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string? SearchKey { get; set; }

        /// <summary>
        /// 检验项目编号
        /// </summary>
        public string? ExamineProjectCode { get; set; }
        
        /// <summary>
        /// 检验项目名称
        /// </summary>
        public string? ExamineProjectName { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
        
        /// <summary>
        /// 创建者姓名
        /// </summary>
        public string? CreateUserName { get; set; }
        
        /// <summary>
        /// 修改者姓名
        /// </summary>
        public string? UpdateUserName { get; set; }
        
    }

    /// <summary>
    /// 检验标准项增加输入参数
    /// </summary>
    public class AddExamineProjectInput : ExamineProjectBaseInput
    {
        /// <summary>
        /// 检验项目编号
        /// </summary>
        [Required(ErrorMessage = "检验项目编号不能为空")]
        public override string ExamineProjectCode { get; set; }
        
        /// <summary>
        /// 检验项目名称
        /// </summary>
        [Required(ErrorMessage = "检验项目名称不能为空")]
        public override string ExamineProjectName { get; set; }
        
    }

    /// <summary>
    /// 检验标准项删除输入参数
    /// </summary>
    public class DeleteExamineProjectInput : BaseIdInput
    {
    }

    /// <summary>
    /// 检验标准项更新输入参数
    /// </summary>
    public class UpdateExamineProjectInput : ExamineProjectBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// 检验标准项主键查询输入参数
    /// </summary>
    public class QueryByIdExamineProjectInput : DeleteExamineProjectInput
    {

    }
