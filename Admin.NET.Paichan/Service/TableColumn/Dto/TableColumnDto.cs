namespace Admin.NET.Paichan;

    /// <summary>
    /// 列表展示输出参数
    /// </summary>
    public class TableColumnDto
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// 页面
        /// </summary>
        public string? PageType { get; set; }
        
        /// <summary>
        /// 字段
        /// </summary>
        public string? Prop { get; set; }
        
        /// <summary>
        /// 展示
        /// </summary>
        public string? Lable { get; set; }
        
        /// <summary>
        /// 列宽
        /// </summary>
        public string? Width { get; set; }
        
        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool? IsHidden { get; set; }
        
        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }
        
        /// <summary>
        /// 创建者姓名
        /// </summary>
        public string? CreateUserName { get; set; }
        
        /// <summary>
        /// 修改者姓名
        /// </summary>
        public string? UpdateUserName { get; set; }
        
    }
