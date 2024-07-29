namespace Admin.NET.Paichan;

    /// <summary>
    /// 物料类别输出参数
    /// </summary>
    public class MaterialTypeDto
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// 物料类型
        /// </summary>
        public string MaterialTypeName { get; set; }
        
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
