namespace Admin.NET.Paichan;

    /// <summary>
    /// 非生产类型输出参数
    /// </summary>
    public class DeviceErrorTypeDto
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// 非生产时间类型名称
        /// </summary>
        public string ErrorTypeName { get; set; }
        
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
