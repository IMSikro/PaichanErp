using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

    /// <summary>
    /// 产品配方基础输入参数
    /// </summary>
    public class ProduceFormulaBaseInput
    {
        /// <summary>
        /// 配方编号
        /// </summary>
        public virtual string ProduceFormulaCode { get; set; }
        
        /// <summary>
        /// 产品外键
        /// </summary>
        public virtual long ProduceId { get; set; }
        
        /// <summary>
        /// 配方名称
        /// </summary>
        public virtual string ProduceFormulaName { get; set; }
        
        /// <summary>
        /// 配方版本
        /// </summary>
        public virtual string FormulaVersion { get; set; }
        
        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool IsEnable { get; set; }
        
        /// <summary>
        /// 配方成本
        /// </summary>
        public virtual double FormulaCosts { get; set; }
        
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
    /// 产品配方分页查询输入参数
    /// </summary>
    public class ProduceFormulaInput : BasePageInput
    {
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string? SearchKey { get; set; }

        /// <summary>
        /// 配方编号
        /// </summary>
        public string? ProduceFormulaCode { get; set; }
        
        /// <summary>
        /// 产品外键
        /// </summary>
        public long? ProduceId { get; set; }
        
        /// <summary>
        /// 配方名称
        /// </summary>
        public string? ProduceFormulaName { get; set; }
        
        /// <summary>
        /// 配方版本
        /// </summary>
        public string? FormulaVersion { get; set; }
        
    }

    /// <summary>
    /// 产品配方增加输入参数
    /// </summary>
    public class AddProduceFormulaInput : ProduceFormulaBaseInput
    {
        /// <summary>
        /// 配方编号
        /// </summary>
        [Required(ErrorMessage = "配方编号不能为空")]
        public override string ProduceFormulaCode { get; set; }
        
        /// <summary>
        /// 产品外键
        /// </summary>
        [Required(ErrorMessage = "产品外键不能为空")]
        public override long ProduceId { get; set; }
        
        /// <summary>
        /// 配方名称
        /// </summary>
        [Required(ErrorMessage = "配方名称不能为空")]
        public override string ProduceFormulaName { get; set; }
        
        /// <summary>
        /// 配方版本
        /// </summary>
        [Required(ErrorMessage = "配方版本不能为空")]
        public override string FormulaVersion { get; set; }
        
        /// <summary>
        /// 是否启用
        /// </summary>
        [Required(ErrorMessage = "是否启用不能为空")]
        public override bool IsEnable { get; set; }
        
    }

    /// <summary>
    /// 产品配方删除输入参数
    /// </summary>
    public class DeleteProduceFormulaInput : BaseIdInput
    {
    }

    /// <summary>
    /// 产品配方更新输入参数
    /// </summary>
    public class UpdateProduceFormulaInput : ProduceFormulaBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// 产品配方主键查询输入参数
    /// </summary>
    public class QueryByIdProduceFormulaInput : DeleteProduceFormulaInput
    {

    }
