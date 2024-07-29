namespace Admin.NET.Paichan;

    /// <summary>
    /// 库存调拨输出参数
    /// </summary>
    public class StoreDeployDto
    {
        /// <summary>
        /// 产品
        /// </summary>
        public string ProduceIdProduceCode { get; set; }
        
        /// <summary>
        /// 调出仓库
        /// </summary>
        public string OutStoreIdStoreCode { get; set; }
        
        /// <summary>
        /// 调入仓库
        /// </summary>
        public string InStoreIdStoreCode { get; set; }
        
        /// <summary>
        /// 主键Id
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// 调拨单号
        /// </summary>
        public string? StoreDeployCode { get; set; }
        
        /// <summary>
        /// 调拨日期
        /// </summary>
        public DateTime? StoreDeployDate { get; set; }
        
        /// <summary>
        /// 调拨数量
        /// </summary>
        public double? StoreDeployNumber { get; set; }
        
        /// <summary>
        /// 产品
        /// </summary>
        public long ProduceId { get; set; }
        
        /// <summary>
        /// 产品编号
        /// </summary>
        public string? ProduceCode { get; set; }
        
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProduceName { get; set; }
        
        /// <summary>
        /// 调出仓库
        /// </summary>
        public long OutStoreId { get; set; }
        
        /// <summary>
        /// 调出仓库编号
        /// </summary>
        public string OutStoreCode { get; set; }
        
        /// <summary>
        /// 调出仓库名称
        /// </summary>
        public string OutStoreName { get; set; }
        
        /// <summary>
        /// 调出仓库库位
        /// </summary>
        public string? OutStoreLocation { get; set; }
        
        /// <summary>
        /// 调入仓库
        /// </summary>
        public long InStoreId { get; set; }
        
        /// <summary>
        /// 调入仓库编号
        /// </summary>
        public string InStoreCode { get; set; }
        
        /// <summary>
        /// 调入仓库名称
        /// </summary>
        public string InStoreName { get; set; }
        
        /// <summary>
        /// 调入仓库库位
        /// </summary>
        public string? InStoreLocation { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
        
        /// <summary>
        /// 创建者姓名
        /// </summary>
        public string? CreateUserName { get; set; }
        
        /// <summary>
        /// 修改者姓名
        /// </summary>
        public string? UpdateUserName { get; set; }
        
    }
