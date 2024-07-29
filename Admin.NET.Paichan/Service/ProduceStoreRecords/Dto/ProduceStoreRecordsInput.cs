using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

    /// <summary>
    /// 生产入库单基础输入参数
    /// </summary>
    public class ProduceStoreRecordsBaseInput
    {
        /// <summary>
        /// 订单批号
        /// </summary>
        public virtual long OrderId { get; set; }
        
        /// <summary>
        /// 订单批号
        /// </summary>
        public virtual string BatchNumber { get; set; }
        
        /// <summary>
        /// 批次总量
        /// </summary>
        public virtual double Quantity { get; set; }
        
        /// <summary>
        /// 生产总量
        /// </summary>
        public virtual double RealQuantity { get; set; }
        
        /// <summary>
        /// 批数
        /// </summary>
        public virtual int BatchCount { get; set; }
        
        /// <summary>
        /// 产品
        /// </summary>
        public virtual long ProduceId { get; set; }
        
        /// <summary>
        /// 产品编号
        /// </summary>
        public virtual string? ProduceCode { get; set; }
        
        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string ProduceName { get; set; }
        
        /// <summary>
        /// 仓库
        /// </summary>
        public virtual long StoreId { get; set; }
        
        /// <summary>
        /// 仓库编号
        /// </summary>
        public virtual string StoreCode { get; set; }
        
        /// <summary>
        /// 仓库名称
        /// </summary>
        public virtual string StoreName { get; set; }
        
        /// <summary>
        /// 库位
        /// </summary>
        public virtual string? StoreLocation { get; set; }
        
        /// <summary>
        /// 入库产值
        /// </summary>
        public virtual double? TotalPrice { get; set; }
        
        /// <summary>
        /// 入库日期
        /// </summary>
        public virtual DateTime? InStoreDate { get; set; }
        
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
    /// 生产入库单分页查询输入参数
    /// </summary>
    public class ProduceStoreRecordsInput : BasePageInput
    {
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string? SearchKey { get; set; }

        /// <summary>
        /// 订单批号
        /// </summary>
        public long? OrderId { get; set; }
        
        /// <summary>
        /// 订单批号
        /// </summary>
        public string? BatchNumber { get; set; }
        
        /// <summary>
        /// 产品
        /// </summary>
        public long? ProduceId { get; set; }
        
        /// <summary>
        /// 仓库
        /// </summary>
        public long? StoreId { get; set; }
        
        /// <summary>
        /// 入库日期
        /// </summary>
        public DateTime? InStoreDate { get; set; }
        
        /// <summary>
         /// 入库日期范围
         /// </summary>
         public List<DateTime?> InStoreDateRange { get; set; } 
    }

    /// <summary>
    /// 生产入库单增加输入参数
    /// </summary>
    public class AddProduceStoreRecordsInput : ProduceStoreRecordsBaseInput
    {
        /// <summary>
        /// 订单批号
        /// </summary>
        [Required(ErrorMessage = "订单批号不能为空")]
        public override long OrderId { get; set; }
        
        /// <summary>
        /// 生产总量
        /// </summary>
        [Required(ErrorMessage = "生产总量不能为空")]
        public override double RealQuantity { get; set; }
        
        /// <summary>
        /// 批数
        /// </summary>
        [Required(ErrorMessage = "批数不能为空")]
        public override int BatchCount { get; set; }
        
        /// <summary>
        /// 仓库
        /// </summary>
        [Required(ErrorMessage = "仓库不能为空")]
        public override long StoreId { get; set; }
        
    }

    /// <summary>
    /// 生产入库单删除输入参数
    /// </summary>
    public class DeleteProduceStoreRecordsInput : BaseIdInput
    {
    }

    /// <summary>
    /// 生产入库单更新输入参数
    /// </summary>
    public class UpdateProduceStoreRecordsInput : ProduceStoreRecordsBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// 生产入库单主键查询输入参数
    /// </summary>
    public class QueryByIdProduceStoreRecordsInput : DeleteProduceStoreRecordsInput
    {

    }
