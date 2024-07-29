using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

    /// <summary>
    /// 库存调拨基础输入参数
    /// </summary>
    public class StoreDeployBaseInput
    {
        /// <summary>
        /// 调拨单号
        /// </summary>
        public virtual string? StoreDeployCode { get; set; }
        
        /// <summary>
        /// 调拨日期
        /// </summary>
        public virtual DateTime? StoreDeployDate { get; set; }
        
        /// <summary>
        /// 调拨数量
        /// </summary>
        public virtual double? StoreDeployNumber { get; set; }
        
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
        /// 调出仓库
        /// </summary>
        public virtual long OutStoreId { get; set; }
        
        /// <summary>
        /// 调出仓库编号
        /// </summary>
        public virtual string OutStoreCode { get; set; }
        
        /// <summary>
        /// 调出仓库名称
        /// </summary>
        public virtual string OutStoreName { get; set; }
        
        /// <summary>
        /// 调出仓库库位
        /// </summary>
        public virtual string? OutStoreLocation { get; set; }
        
        /// <summary>
        /// 调入仓库
        /// </summary>
        public virtual long InStoreId { get; set; }
        
        /// <summary>
        /// 调入仓库编号
        /// </summary>
        public virtual string InStoreCode { get; set; }
        
        /// <summary>
        /// 调入仓库名称
        /// </summary>
        public virtual string InStoreName { get; set; }
        
        /// <summary>
        /// 调入仓库库位
        /// </summary>
        public virtual string? InStoreLocation { get; set; }
        
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
    /// 库存调拨分页查询输入参数
    /// </summary>
    public class StoreDeployInput : BasePageInput
    {
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string? SearchKey { get; set; }

        /// <summary>
        /// 调拨单号
        /// </summary>
        public string? StoreDeployCode { get; set; }
        
        /// <summary>
        /// 调拨日期
        /// </summary>
        public DateTime? StoreDeployDate { get; set; }
        
        /// <summary>
         /// 调拨日期范围
         /// </summary>
         public List<DateTime?> StoreDeployDateRange { get; set; } 
        /// <summary>
        /// 产品
        /// </summary>
        public long? ProduceId { get; set; }
        
        /// <summary>
        /// 调出仓库
        /// </summary>
        public long? OutStoreId { get; set; }
        
        /// <summary>
        /// 调入仓库
        /// </summary>
        public long? InStoreId { get; set; }
        
    }

    /// <summary>
    /// 库存调拨增加输入参数
    /// </summary>
    public class AddStoreDeployInput : StoreDeployBaseInput
    {
        /// <summary>
        /// 调拨单号
        /// </summary>
        [Required(ErrorMessage = "调拨单号不能为空")]
        public override string? StoreDeployCode { get; set; }
        
        /// <summary>
        /// 调拨日期
        /// </summary>
        [Required(ErrorMessage = "调拨日期不能为空")]
        public override DateTime? StoreDeployDate { get; set; }
        
        /// <summary>
        /// 调拨数量
        /// </summary>
        [Required(ErrorMessage = "调拨数量不能为空")]
        public override double? StoreDeployNumber { get; set; }
        
        /// <summary>
        /// 产品
        /// </summary>
        [Required(ErrorMessage = "产品不能为空")]
        public override long ProduceId { get; set; }
        
        /// <summary>
        /// 调出仓库
        /// </summary>
        [Required(ErrorMessage = "调出仓库不能为空")]
        public override long OutStoreId { get; set; }
        
        /// <summary>
        /// 调入仓库
        /// </summary>
        [Required(ErrorMessage = "调入仓库不能为空")]
        public override long InStoreId { get; set; }
        
    }

    /// <summary>
    /// 库存调拨删除输入参数
    /// </summary>
    public class DeleteStoreDeployInput : BaseIdInput
    {
    }

    /// <summary>
    /// 库存调拨更新输入参数
    /// </summary>
    public class UpdateStoreDeployInput : StoreDeployBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// 库存调拨主键查询输入参数
    /// </summary>
    public class QueryByIdStoreDeployInput : DeleteStoreDeployInput
    {

    }
