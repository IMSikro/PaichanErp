namespace Admin.NET.Paichan;

    /// <summary>
    /// 仓库输出参数
    /// </summary>
    public class StoreDto
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string StoreCode { get; set; }
        
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string StoreName { get; set; }
        
        /// <summary>
        /// 库位
        /// </summary>
        public string? StoreLocation { get; set; }
        
        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }
        
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
