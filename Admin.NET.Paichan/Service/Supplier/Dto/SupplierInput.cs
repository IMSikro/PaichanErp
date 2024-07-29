using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

    /// <summary>
    /// 供应商基础输入参数
    /// </summary>
    public class SupplierBaseInput
    {
        /// <summary>
        /// 供应商编号
        /// </summary>
        public virtual string SupplierCode { get; set; }
        
        /// <summary>
        /// 供应商名称
        /// </summary>
        public virtual string SupplierName { get; set; }
        
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
        public virtual string? SupplierPos { get; set; }
        
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
    /// 供应商分页查询输入参数
    /// </summary>
    public class SupplierInput : BasePageInput
    {
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string? SearchKey { get; set; }

        /// <summary>
        /// 供应商编号
        /// </summary>
        public string? SupplierCode { get; set; }
        
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string? SupplierName { get; set; }
        
        /// <summary>
        /// 姓名
        /// </summary>
        public string? Surname { get; set; }
        
        /// <summary>
        /// 手机号
        /// </summary>
        public string? Mobile { get; set; }
        
    }

    /// <summary>
    /// 供应商增加输入参数
    /// </summary>
    public class AddSupplierInput : SupplierBaseInput
    {
        /// <summary>
        /// 供应商编号
        /// </summary>
        [Required(ErrorMessage = "供应商编号不能为空")]
        public override string SupplierCode { get; set; }
        
        /// <summary>
        /// 供应商名称
        /// </summary>
        [Required(ErrorMessage = "供应商名称不能为空")]
        public override string SupplierName { get; set; }
        
        /// <summary>
        /// 手机号
        /// </summary>
        [Required(ErrorMessage = "手机号不能为空")]
        public override string? Mobile { get; set; }
        
    }

    /// <summary>
    /// 供应商删除输入参数
    /// </summary>
    public class DeleteSupplierInput : BaseIdInput
    {
    }

    /// <summary>
    /// 供应商更新输入参数
    /// </summary>
    public class UpdateSupplierInput : SupplierBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// 供应商主键查询输入参数
    /// </summary>
    public class QueryByIdSupplierInput : DeleteSupplierInput
    {

    }
