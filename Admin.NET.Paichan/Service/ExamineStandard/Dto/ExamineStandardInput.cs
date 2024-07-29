using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

    /// <summary>
    /// 检验标准基础输入参数
    /// </summary>
    public class ExamineStandardBaseInput
    {
        /// <summary>
        /// 检验标准编号
        /// </summary>
        public virtual string ExamineStandardCode { get; set; }
        
        /// <summary>
        /// 检验标准名称
        /// </summary>
        public virtual string ExamineStandardName { get; set; }
        
        /// <summary>
        /// 产品
        /// </summary>
        public virtual long? ProduceId { get; set; }
        
        /// <summary>
        /// 产品编号
        /// </summary>
        public virtual string? ProduceCode { get; set; }
        
        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string ProduceName { get; set; }
        
        /// <summary>
        /// 工艺设备
        /// </summary>
        public virtual long? DeviceTypeId { get; set; }
        
        /// <summary>
        /// 工艺设备名称
        /// </summary>
        public virtual string DeviceTypeName { get; set; }
        
        /// <summary>
        /// 检验项目
        /// </summary>
        public virtual long? ExamineProjectId { get; set; }
        
        /// <summary>
        /// 检验项目编号
        /// </summary>
        public virtual string? ExamineProjectCode { get; set; }
        
        /// <summary>
        /// 检验项目名称
        /// </summary>
        public virtual string ExamineProjectName { get; set; }
        
        /// <summary>
        /// 标准值
        /// </summary>
        public virtual double StandardValue { get; set; }
        
        /// <summary>
        /// 正公差
        /// </summary>
        public virtual double Tolerance1 { get; set; }
        
        /// <summary>
        /// 负公差
        /// </summary>
        public virtual double Tolerance2 { get; set; }
        
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
    /// 检验标准分页查询输入参数
    /// </summary>
    public class ExamineStandardInput : BasePageInput
    {
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string? SearchKey { get; set; }

        /// <summary>
        /// 检验标准编号
        /// </summary>
        public string? ExamineStandardCode { get; set; }
        
        /// <summary>
        /// 检验标准名称
        /// </summary>
        public string? ExamineStandardName { get; set; }
        
        /// <summary>
        /// 产品
        /// </summary>
        public long? ProduceId { get; set; }
        
        /// <summary>
        /// 工艺设备
        /// </summary>
        public long? DeviceTypeId { get; set; }
        
        /// <summary>
        /// 检验项目
        /// </summary>
        public long? ExamineProjectId { get; set; }
        
    }

    /// <summary>
    /// 检验标准增加输入参数
    /// </summary>
    public class AddExamineStandardInput : ExamineStandardBaseInput
    {
        /// <summary>
        /// 检验标准编号
        /// </summary>
        [Required(ErrorMessage = "检验标准编号不能为空")]
        public override string ExamineStandardCode { get; set; }
        
        /// <summary>
        /// 检验标准名称
        /// </summary>
        [Required(ErrorMessage = "检验标准名称不能为空")]
        public override string ExamineStandardName { get; set; }
        
        /// <summary>
        /// 产品
        /// </summary>
        [Required(ErrorMessage = "产品不能为空")]
        public override long? ProduceId { get; set; }
        
        /// <summary>
        /// 工艺设备
        /// </summary>
        [Required(ErrorMessage = "工艺设备不能为空")]
        public override long? DeviceTypeId { get; set; }
        
        /// <summary>
        /// 检验项目
        /// </summary>
        [Required(ErrorMessage = "检验项目不能为空")]
        public override long? ExamineProjectId { get; set; }
        
        /// <summary>
        /// 标准值
        /// </summary>
        [Required(ErrorMessage = "标准值不能为空")]
        public override double StandardValue { get; set; }
        
        /// <summary>
        /// 正公差
        /// </summary>
        [Required(ErrorMessage = "正公差不能为空")]
        public override double Tolerance1 { get; set; }
        
        /// <summary>
        /// 负公差
        /// </summary>
        [Required(ErrorMessage = "负公差不能为空")]
        public override double Tolerance2 { get; set; }
        
    }

    /// <summary>
    /// 检验标准删除输入参数
    /// </summary>
    public class DeleteExamineStandardInput : BaseIdInput
    {
    }

    /// <summary>
    /// 检验标准更新输入参数
    /// </summary>
    public class UpdateExamineStandardInput : ExamineStandardBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// 检验标准主键查询输入参数
    /// </summary>
    public class QueryByIdExamineStandardInput : DeleteExamineStandardInput
    {

    }
