namespace Admin.NET.Paichan;

    /// <summary>
    /// 计量单位输出参数
    /// </summary>
    public class SystemUnitDto
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// 计量单位
        /// </summary>
        public string UnitName { get; set; }
        
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
