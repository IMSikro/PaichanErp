namespace Admin.NET.Paichan;

/// <summary>
/// 供应商输出参数
/// </summary>
public class SupplierOutput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// 供应商编号
    /// </summary>
    public string SupplierCode { get; set; }
    
    /// <summary>
    /// 供应商名称
    /// </summary>
    public string SupplierName { get; set; }
    
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
    public string? SupplierPos { get; set; }
    
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
 

