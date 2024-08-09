using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

    /// <summary>
    /// 工艺标准基础输入参数
    /// </summary>
    public class ProcessStandardBaseInput
    {
        /// <summary>
        /// 配方
        /// </summary>
        public virtual long? ProduceFormulaId { get; set; }
        
        /// <summary>
        /// 工艺标准编号
        /// </summary>
        public virtual string ProcessStandardCode { get; set; }
        
        /// <summary>
        /// 工艺标准名称
        /// </summary>
        public virtual string ProcessStandardName { get; set; }
        
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
        /// 工艺项目
        /// </summary>
        public virtual long? ProcessProjectId { get; set; }
        
        /// <summary>
        /// 工艺项目编号
        /// </summary>
        public virtual string? ProcessProjectCode { get; set; }
        
        /// <summary>
        /// 工艺项目名称
        /// </summary>
        public virtual string ProcessProjectName { get; set; }
        
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
    /// 工艺标准分页查询输入参数
    /// </summary>
    public class ProcessStandardInput : BasePageInput
    {
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string? SearchKey { get; set; }

        /// <summary>
        /// 配方
        /// </summary>
        public long? ProduceFormulaId { get; set; }
        
        /// <summary>
        /// 产品
        /// </summary>
        public long? ProduceId { get; set; }
        
        /// <summary>
        /// 工艺设备
        /// </summary>
        public long? DeviceTypeId { get; set; }
        
        /// <summary>
        /// 工艺项目
        /// </summary>
        public long? ProcessProjectId { get; set; }
        
    }

    /// <summary>
    /// 工艺标准增加输入参数
    /// </summary>
    public class AddProcessStandardInput : ProcessStandardBaseInput
    {
        /// <summary>
        /// 工艺标准编号
        /// </summary>
        [Required(ErrorMessage = "工艺标准编号不能为空")]
        public override string ProcessStandardCode { get; set; }
        
        /// <summary>
        /// 工艺标准名称
        /// </summary>
        [Required(ErrorMessage = "工艺标准名称不能为空")]
        public override string ProcessStandardName { get; set; }
        
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
        /// 工艺项目
        /// </summary>
        [Required(ErrorMessage = "工艺项目不能为空")]
        public override long? ProcessProjectId { get; set; }
        
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
    /// 工艺标准删除输入参数
    /// </summary>
    public class DeleteProcessStandardInput : BaseIdInput
    {
    }

    /// <summary>
    /// 工艺标准更新输入参数
    /// </summary>
    public class UpdateProcessStandardInput : ProcessStandardBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// 工艺标准主键查询输入参数
    /// </summary>
    public class QueryByIdProcessStandardInput : DeleteProcessStandardInput
    {

    }
