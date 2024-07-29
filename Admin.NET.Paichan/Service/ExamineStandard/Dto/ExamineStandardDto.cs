namespace Admin.NET.Paichan;

    /// <summary>
    /// 检验标准输出参数
    /// </summary>
    public class ExamineStandardDto
    {
        /// <summary>
        /// 产品
        /// </summary>
        public string ProduceIdProduceCode { get; set; }
        
        /// <summary>
        /// 工艺设备
        /// </summary>
        public string DeviceTypeIdTypeName { get; set; }
        
        /// <summary>
        /// 检验项目
        /// </summary>
        public string ExamineProjectIdExamineProjectCode { get; set; }
        
        /// <summary>
        /// 主键Id
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// 检验标准编号
        /// </summary>
        public string ExamineStandardCode { get; set; }
        
        /// <summary>
        /// 检验标准名称
        /// </summary>
        public string ExamineStandardName { get; set; }
        
        /// <summary>
        /// 产品
        /// </summary>
        public long? ProduceId { get; set; }
        
        /// <summary>
        /// 产品编号
        /// </summary>
        public string? ProduceCode { get; set; }
        
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProduceName { get; set; }
        
        /// <summary>
        /// 工艺设备
        /// </summary>
        public long? DeviceTypeId { get; set; }
        
        /// <summary>
        /// 工艺设备名称
        /// </summary>
        public string DeviceTypeName { get; set; }
        
        /// <summary>
        /// 检验项目
        /// </summary>
        public long? ExamineProjectId { get; set; }
        
        /// <summary>
        /// 检验项目编号
        /// </summary>
        public string? ExamineProjectCode { get; set; }
        
        /// <summary>
        /// 检验项目名称
        /// </summary>
        public string ExamineProjectName { get; set; }
        
        /// <summary>
        /// 标准值
        /// </summary>
        public double StandardValue { get; set; }
        
        /// <summary>
        /// 正公差
        /// </summary>
        public double Tolerance1 { get; set; }
        
        /// <summary>
        /// 负公差
        /// </summary>
        public double Tolerance2 { get; set; }
        
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
