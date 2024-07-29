using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

    /// <summary>
    /// 物料基础输入参数
    /// </summary>
    public class MaterialBaseInput
    {
        /// <summary>
        /// 物料类型
        /// </summary>
        public virtual long MaterialTypeId { get; set; }
        
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
        /// 供应商
        /// </summary>
        public virtual long? SupplierId { get; set; }
        
        /// <summary>
        /// 安全库存
        /// </summary>
        public virtual double SafetyStock { get; set; }
        
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
    /// 物料分页查询输入参数
    /// </summary>
    public class MaterialInput : BasePageInput
    {
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string? SearchKey { get; set; }

        /// <summary>
        /// 物料类型
        /// </summary>
        public long? MaterialTypeId { get; set; }
        
        /// <summary>
        /// 物料编号
        /// </summary>
        public string? MaterialCode { get; set; }
        
        /// <summary>
        /// 物料名称
        /// </summary>
        public string? MaterialName { get; set; }
        
        /// <summary>
        /// 供应商
        /// </summary>
        public long? SupplierId { get; set; }
        
    }

    /// <summary>
    /// 物料增加输入参数
    /// </summary>
    public class AddMaterialInput : MaterialBaseInput
    {
        /// <summary>
        /// 物料类型
        /// </summary>
        [Required(ErrorMessage = "物料类型不能为空")]
        public override long MaterialTypeId { get; set; }
        
        /// <summary>
        /// 物料编号
        /// </summary>
        [Required(ErrorMessage = "物料编号不能为空")]
        public override string MaterialCode { get; set; }
        
        /// <summary>
        /// 物料名称
        /// </summary>
        [Required(ErrorMessage = "物料名称不能为空")]
        public override string MaterialName { get; set; }
        
        /// <summary>
        /// 物料规格
        /// </summary>
        [Required(ErrorMessage = "物料规格不能为空")]
        public override string MaterialNorm { get; set; }
        
        /// <summary>
        /// 采购单价
        /// </summary>
        [Required(ErrorMessage = "采购单价不能为空")]
        public override double CostPrice { get; set; }
        
        /// <summary>
        /// 税率(%)
        /// </summary>
        [Required(ErrorMessage = "税率(%)不能为空")]
        public override double DutyRate { get; set; }
        
        /// <summary>
        /// 安全库存
        /// </summary>
        [Required(ErrorMessage = "安全库存不能为空")]
        public override double SafetyStock { get; set; }
        
    }

    /// <summary>
    /// 物料删除输入参数
    /// </summary>
    public class DeleteMaterialInput : BaseIdInput
    {
    }

    /// <summary>
    /// 物料更新输入参数
    /// </summary>
    public class UpdateMaterialInput : MaterialBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// 物料主键查询输入参数
    /// </summary>
    public class QueryByIdMaterialInput : DeleteMaterialInput
    {

    }
