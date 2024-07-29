namespace Admin.NET.Paichan;

/// <summary>
/// 物料输出参数
/// </summary>
public class MaterialOutput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// 物料类型
    /// </summary>
    public long MaterialTypeId { get; set; } 
    
    /// <summary>
    /// 物料类型 描述
    /// </summary>
    public string MaterialTypeIdMaterialTypeName { get; set; } 
    
    /// <summary>
    /// 物料编号
    /// </summary>
    public string MaterialCode { get; set; }
    
    /// <summary>
    /// 物料名称
    /// </summary>
    public string MaterialName { get; set; }
    
    /// <summary>
    /// 物料规格
    /// </summary>
    public string MaterialNorm { get; set; }
    
    /// <summary>
    /// 采购单价
    /// </summary>
    public double CostPrice { get; set; }
    
    /// <summary>
    /// 税率(%)
    /// </summary>
    public double DutyRate { get; set; }
    
    /// <summary>
    /// 供应商
    /// </summary>
    public long? SupplierId { get; set; } 
    
    /// <summary>
    /// 供应商 描述
    /// </summary>
    public string SupplierIdSupplierName { get; set; } 
    
    /// <summary>
    /// 安全库存
    /// </summary>
    public double SafetyStock { get; set; }
    
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
 

