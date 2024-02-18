using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

    /// <summary>
    /// 产品列表基础输入参数
    /// </summary>
    public class ProduceBaseInput
    {
        /// <summary>
        /// 产品类型
        /// </summary>
        public virtual long ProduceType { get; set; }
        
        /// <summary>
        /// 产品编号
        /// </summary>
        public virtual string ProduceCode { get; set; }
        
        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string ProduceName { get; set; }
        
        /// <summary>
        /// 产品LAB颜色
        /// </summary>
        public virtual string ColorLab { get; set; }
        
        /// <summary>
        /// 产品RGB颜色
        /// </summary>
        public virtual string ColorRgb { get; set; }
        
        /// <summary>
        /// 是否搅拌
        /// </summary>
        public virtual bool IsMix { get; set; }
        
        /// <summary>
        /// 是否挤出
        /// </summary>
        public virtual bool IsExtrusion { get; set; }
        
        /// <summary>
        /// 是否搅拌
        /// </summary>
        public virtual bool IsMill { get; set; }
        
        /// <summary>
        /// 产品系数
        /// </summary>
        public virtual string? ProduceCoefficient { get; set; }
        
        /// <summary>
        /// 产品系列
        /// </summary>
        public virtual string? ProduceSeries { get; set; }
        
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
    /// 产品列表分页查询输入参数
    /// </summary>
    public class ProduceInput : BasePageInput
    {
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string? SearchKey { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public long? ProduceType { get; set; }
        
        /// <summary>
        /// 产品编号
        /// </summary>
        public string? ProduceCode { get; set; }
        
        /// <summary>
        /// 产品名称
        /// </summary>
        public string? ProduceName { get; set; }
        
    }

    /// <summary>
    /// 产品列表增加输入参数
    /// </summary>
    public class AddProduceInput : ProduceBaseInput
    {
        /// <summary>
        /// 产品类型
        /// </summary>
        [Required(ErrorMessage = "产品类型不能为空")]
        public override long ProduceType { get; set; }
        
        /// <summary>
        /// 产品编号
        /// </summary>
        [Required(ErrorMessage = "产品编号不能为空")]
        public override string ProduceCode { get; set; }
        
        /// <summary>
        /// 产品名称
        /// </summary>
        [Required(ErrorMessage = "产品名称不能为空")]
        public override string ProduceName { get; set; }
        
        /// <summary>
        /// 产品LAB颜色
        /// </summary>
        [Required(ErrorMessage = "产品LAB颜色不能为空")]
        public override string ColorLab { get; set; }
        
    }

    /// <summary>
    /// 产品列表删除输入参数
    /// </summary>
    public class DeleteProduceInput : BaseIdInput
    {
    }

    /// <summary>
    /// 产品列表更新输入参数
    /// </summary>
    public class UpdateProduceInput : ProduceBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// 产品列表主键查询输入参数
    /// </summary>
    public class QueryByIdProduceInput : DeleteProduceInput
    {

    }
