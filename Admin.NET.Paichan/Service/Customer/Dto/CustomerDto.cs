namespace Admin.NET.Paichan;

    /// <summary>
    /// 客户输出参数
    /// </summary>
    public class CustomerDto
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// 客户编号
        /// </summary>
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        
        /// <summary>
        /// 签约开始日期
        /// </summary>
        public DateTime? ContractStartDate { get; set; }
        
        /// <summary>
        /// 签约结束日期
        /// </summary>
        public DateTime? ContractEndDate { get; set; }
        
        /// <summary>
        /// 省份
        /// </summary>
        public string? Province { get; set; }
        
        /// <summary>
        /// 城市
        /// </summary>
        public string? City { get; set; }
        
        /// <summary>
        /// 区县
        /// </summary>
        public string? Area { get; set; }
        
        /// <summary>
        /// 详细地址
        /// </summary>
        public string? Address { get; set; }
        
        /// <summary>
        /// 姓名
        /// </summary>
        public string? Surname { get; set; }
        
        /// <summary>
        /// 职位
        /// </summary>
        public string? CustomerPos { get; set; }
        
        /// <summary>
        /// 固定电话
        /// </summary>
        public string? TelPhone { get; set; }
        
        /// <summary>
        /// 手机号
        /// </summary>
        public string? Mobile { get; set; }
        
        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }
        
        /// <summary>
        /// QQ
        /// </summary>
        public string? QQ { get; set; }
        
        /// <summary>
        /// 开户名称
        /// </summary>
        public string? AccountName { get; set; }
        
        /// <summary>
        /// 开户银行
        /// </summary>
        public string? AccountBank { get; set; }
        
        /// <summary>
        /// 银行账号
        /// </summary>
        public string? BankNumber { get; set; }
        
        /// <summary>
        /// 发票抬头
        /// </summary>
        public string? InvoiceHeader { get; set; }
        
        /// <summary>
        /// 纳税人识别码
        /// </summary>
        public string? TaxpayerIdentification { get; set; }
        
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
