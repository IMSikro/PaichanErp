using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

    /// <summary>
    /// 库存盘点基础输入参数
    /// </summary>
    public class StockTakeBaseInput
    {
        /// <summary>
        /// 盘点单号
        /// </summary>
        public virtual string? StockTakeCode { get; set; }
        
        /// <summary>
        /// 盘点日期
        /// </summary>
        public virtual DateTime? StockTakeDate { get; set; }
        
        /// <summary>
        /// 盘点数量
        /// </summary>
        public virtual double? StockTakeNumber { get; set; }
        
        /// <summary>
        /// 盈亏数量
        /// </summary>
        public virtual double? StockTakeDiffNumber { get; set; }
        
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
    /// 库存盘点分页查询输入参数
    /// </summary>
    public class StockTakeInput : BasePageInput
    {
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string? SearchKey { get; set; }

        /// <summary>
        /// 盘点单号
        /// </summary>
        public string? StockTakeCode { get; set; }
        
        /// <summary>
        /// 盘点日期
        /// </summary>
        public DateTime? StockTakeDate { get; set; }
        
        /// <summary>
         /// 盘点日期范围
         /// </summary>
         public List<DateTime?> StockTakeDateRange { get; set; } 
        /// <summary>
        /// 产品
        /// </summary>
        public long? ProduceId { get; set; }
        
        /// <summary>
        /// 仓库
        /// </summary>
        public long? StoreId { get; set; }
        
    }

    /// <summary>
    /// 库存盘点增加输入参数
    /// </summary>
    public class AddStockTakeInput : StockTakeBaseInput
    {
        /// <summary>
        /// 产品
        /// </summary>
        [Required(ErrorMessage = "产品不能为空")]
        public override long ProduceId { get; set; }
        
        /// <summary>
        /// 仓库
        /// </summary>
        [Required(ErrorMessage = "仓库不能为空")]
        public override long StoreId { get; set; }
        
    }

    /// <summary>
    /// 库存盘点删除输入参数
    /// </summary>
    public class DeleteStockTakeInput : BaseIdInput
    {
    }

    /// <summary>
    /// 库存盘点更新输入参数
    /// </summary>
    public class UpdateStockTakeInput : StockTakeBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// 库存盘点主键查询输入参数
    /// </summary>
    public class QueryByIdStockTakeInput : DeleteStockTakeInput
    {

    }
