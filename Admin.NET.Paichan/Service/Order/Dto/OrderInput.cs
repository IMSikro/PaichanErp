using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

    /// <summary>
    /// 订单列表基础输入参数
    /// </summary>
    public class OrderBaseInput
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public virtual string OrderCode { get; set; }
        
        /// <summary>
        /// 下单日期
        /// </summary>
        public virtual DateTime OrderDate { get; set; }
        
        /// <summary>
        /// 交货日期
        /// </summary>
        public virtual DateTime DeliveryDate { get; set; }
        
        /// <summary>
        /// 实际开工时间
        /// </summary>
        public virtual DateTime? StartDate { get; set; }
        
        /// <summary>
        /// 实际完成时间
        /// </summary>
        public virtual DateTime? EndDate { get; set; }
        
        /// <summary>
        /// 产品选择
        /// </summary>
        public virtual long ProduceId { get; set; }
        
        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string ProduceName { get; set; }
        
        /// <summary>
        /// 批次号
        /// </summary>
        public virtual string BatchNumber { get; set; }
        
        /// <summary>
        /// 批次总量
        /// </summary>
        public virtual double Quantity { get; set; }
        
        /// <summary>
        /// 计量单位
        /// </summary>
        public virtual string pUnit { get; set; }
        
        /// <summary>
        /// 客户
        /// </summary>
        public virtual string Customer { get; set; }
        
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
    /// 订单列表分页查询输入参数
    /// </summary>
    public class OrderInput : BasePageInput
    {
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string? SearchKey { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string? OrderCode { get; set; }
        
        /// <summary>
        /// 下单日期
        /// </summary>
        public DateTime? OrderDate { get; set; }
        
        /// <summary>
         /// 下单日期范围
         /// </summary>
         public List<DateTime?> OrderDateRange { get; set; } 
        /// <summary>
        /// 交货日期
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        
        /// <summary>
         /// 交货日期范围
         /// </summary>
         public List<DateTime?> DeliveryDateRange { get; set; } 
        /// <summary>
        /// 产品选择
        /// </summary>
        public long? ProduceId { get; set; }
        
        /// <summary>
        /// 产品名称
        /// </summary>
        public string? ProduceName { get; set; }
        
        /// <summary>
        /// 批次号
        /// </summary>
        public string? BatchNumber { get; set; }
        
        /// <summary>
        /// 批次总量
        /// </summary>
        public double? Quantity { get; set; }
        
        /// <summary>
        /// 计量单位
        /// </summary>
        public string? pUnit { get; set; }
        
        /// <summary>
        /// 客户
        /// </summary>
        public string? Customer { get; set; }
        
    }

    /// <summary>
    /// 订单列表增加输入参数
    /// </summary>
    public class AddOrderInput : OrderBaseInput
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        [Required(ErrorMessage = "订单编号不能为空")]
        public override string OrderCode { get; set; }
        
        /// <summary>
        /// 下单日期
        /// </summary>
        [Required(ErrorMessage = "下单日期不能为空")]
        public override DateTime OrderDate { get; set; }
        
        /// <summary>
        /// 交货日期
        /// </summary>
        [Required(ErrorMessage = "交货日期不能为空")]
        public override DateTime DeliveryDate { get; set; }
        
        /// <summary>
        /// 产品选择
        /// </summary>
        [Required(ErrorMessage = "产品选择不能为空")]
        public override long ProduceId { get; set; }
        
        /// <summary>
        /// 批次号
        /// </summary>
        [Required(ErrorMessage = "批次号不能为空")]
        public override string BatchNumber { get; set; }
        
        /// <summary>
        /// 批次总量
        /// </summary>
        [Required(ErrorMessage = "批次总量不能为空")]
        public override double Quantity { get; set; }
        
    }

    /// <summary>
    /// 订单列表删除输入参数
    /// </summary>
    public class DeleteOrderInput : BaseIdInput
    {
    }

    /// <summary>
    /// 订单列表更新输入参数
    /// </summary>
    public class UpdateOrderInput : OrderBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// 订单列表主键查询输入参数
    /// </summary>
    public class QueryByIdOrderInput : DeleteOrderInput
    {

    }
