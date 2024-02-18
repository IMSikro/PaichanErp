namespace Admin.NET.Paichan;

    /// <summary>
    /// 产品列表输出参数
    /// </summary>
    public class ProduceDto
    {
        /// <summary>
        /// 产品类型
        /// </summary>
        public string? ProduceTypeTypeName { get; set; }
        
        /// <summary>
        /// 主键Id
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// 产品类型
        /// </summary>
        public long ProduceType { get; set; }
        
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProduceCode { get; set; }
        
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProduceName { get; set; }
        
        /// <summary>
        /// 产品LAB颜色
        /// </summary>
        public string ColorLab { get; set; }
        
        /// <summary>
        /// 产品RGB颜色
        /// </summary>
        public string ColorRgb { get; set; }
        
        /// <summary>
        /// 是否搅拌
        /// </summary>
        public bool IsMix { get; set; }
        
        /// <summary>
        /// 是否挤出
        /// </summary>
        public bool IsExtrusion { get; set; }
        
        /// <summary>
        /// 是否搅拌
        /// </summary>
        public bool IsMill { get; set; }
        
        /// <summary>
        /// 产品系数
        /// </summary>
        public string? ProduceCoefficient { get; set; }
        
        /// <summary>
        /// 产品系列
        /// </summary>
        public string? ProduceSeries { get; set; }
        
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
