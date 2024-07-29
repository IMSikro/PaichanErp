using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

    /// <summary>
    /// 客户基础输入参数
    /// </summary>
    public class CustomerBaseInput
    {
        /// <summary>
        /// 客户编号
        /// </summary>
        public virtual string CustomerCode { get; set; }
        
        /// <summary>
        /// 客户名称
        /// </summary>
        public virtual string CustomerName { get; set; }
        
        /// <summary>
        /// 签约开始日期
        /// </summary>
        public virtual DateTime? ContractStartDate { get; set; }
        
        /// <summary>
        /// 签约结束日期
        /// </summary>
        public virtual DateTime? ContractEndDate { get; set; }
        
        /// <summary>
        /// 省份
        /// </summary>
        public virtual string? Province { get; set; }
        
        /// <summary>
        /// 城市
        /// </summary>
        public virtual string? City { get; set; }
        
        /// <summary>
        /// 区县
        /// </summary>
        public virtual string? Area { get; set; }
        
        /// <summary>
        /// 详细地址
        /// </summary>
        public virtual string? Address { get; set; }
        
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string? Surname { get; set; }
        
        /// <summary>
        /// 职位
        /// </summary>
        public virtual string? CustomerPos { get; set; }
        
        /// <summary>
        /// 固定电话
        /// </summary>
        public virtual string? TelPhone { get; set; }
        
        /// <summary>
        /// 手机号
        /// </summary>
        public virtual string? Mobile { get; set; }
        
        /// <summary>
        /// 邮箱
        /// </summary>
        public virtual string? Email { get; set; }
        
        /// <summary>
        /// QQ
        /// </summary>
        public virtual string? QQ { get; set; }
        
        /// <summary>
        /// 开户名称
        /// </summary>
        public virtual string? AccountName { get; set; }
        
        /// <summary>
        /// 开户银行
        /// </summary>
        public virtual string? AccountBank { get; set; }
        
        /// <summary>
        /// 银行账号
        /// </summary>
        public virtual string? BankNumber { get; set; }
        
        /// <summary>
        /// 发票抬头
        /// </summary>
        public virtual string? InvoiceHeader { get; set; }
        
        /// <summary>
        /// 纳税人识别码
        /// </summary>
        public virtual string? TaxpayerIdentification { get; set; }
        
        /// <summary>
        /// 排序
        /// </summary>
        public virtual int? Sort { get; set; }
        
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
    /// 客户分页查询输入参数
    /// </summary>
    public class CustomerInput : BasePageInput
    {
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string? SearchKey { get; set; }

        /// <summary>
        /// 客户编号
        /// </summary>
        public string? CustomerCode { get; set; }
        
        /// <summary>
        /// 客户名称
        /// </summary>
        public string? CustomerName { get; set; }
        
        /// <summary>
        /// 姓名
        /// </summary>
        public string? Surname { get; set; }
        
    }

    /// <summary>
    /// 客户增加输入参数
    /// </summary>
    public class AddCustomerInput : CustomerBaseInput
    {
        /// <summary>
        /// 客户编号
        /// </summary>
        [Required(ErrorMessage = "客户编号不能为空")]
        public override string CustomerCode { get; set; }
        
        /// <summary>
        /// 客户名称
        /// </summary>
        [Required(ErrorMessage = "客户名称不能为空")]
        public override string CustomerName { get; set; }
        
    }

    /// <summary>
    /// 客户删除输入参数
    /// </summary>
    public class DeleteCustomerInput : BaseIdInput
    {
    }

    /// <summary>
    /// 客户更新输入参数
    /// </summary>
    public class UpdateCustomerInput : CustomerBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// 客户主键查询输入参数
    /// </summary>
    public class QueryByIdCustomerInput : DeleteCustomerInput
    {

    }
