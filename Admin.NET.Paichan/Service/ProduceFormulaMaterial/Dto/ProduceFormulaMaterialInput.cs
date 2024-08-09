using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

    /// <summary>
    /// 配方物料基础输入参数
    /// </summary>
    public class ProduceFormulaMaterialBaseInput
    {
        /// <summary>
        /// 配方
        /// </summary>
        public virtual long? ProduceFormulaId { get; set; }
        
        /// <summary>
        /// 物料
        /// </summary>
        public virtual long? MaterialId { get; set; }
        
        /// <summary>
        /// 物料编号
        /// </summary>
        public virtual string MaterialCode { get; set; }
        
        /// <summary>
        /// 物料名称
        /// </summary>
        public virtual string MaterialName { get; set; }
        
        /// <summary>
        /// 物料规格
        /// </summary>
        public virtual string MaterialNorm { get; set; }
        
        /// <summary>
        /// 采购单价
        /// </summary>
        public virtual double CostPrice { get; set; }
        
        /// <summary>
        /// 税率(%)
        /// </summary>
        public virtual double DutyRate { get; set; }
        
        /// <summary>
        /// 排序
        /// </summary>
        public virtual int? Sort { get; set; }
        
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
    /// 配方物料分页查询输入参数
    /// </summary>
    public class ProduceFormulaMaterialInput : BasePageInput
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
        /// 物料
        /// </summary>
        public long? MaterialId { get; set; }
        
    }

    /// <summary>
    /// 配方物料增加输入参数
    /// </summary>
    public class AddProduceFormulaMaterialInput : ProduceFormulaMaterialBaseInput
    {
        /// <summary>
        /// 配方
        /// </summary>
        [Required(ErrorMessage = "配方不能为空")]
        public override long? ProduceFormulaId { get; set; }
        
        /// <summary>
        /// 物料
        /// </summary>
        [Required(ErrorMessage = "物料不能为空")]
        public override long? MaterialId { get; set; }
        
    }

    /// <summary>
    /// 配方物料删除输入参数
    /// </summary>
    public class DeleteProduceFormulaMaterialInput : BaseIdInput
    {
    }

    /// <summary>
    /// 配方物料更新输入参数
    /// </summary>
    public class UpdateProduceFormulaMaterialInput : ProduceFormulaMaterialBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// 配方物料主键查询输入参数
    /// </summary>
    public class QueryByIdProduceFormulaMaterialInput : DeleteProduceFormulaMaterialInput
    {

    }
