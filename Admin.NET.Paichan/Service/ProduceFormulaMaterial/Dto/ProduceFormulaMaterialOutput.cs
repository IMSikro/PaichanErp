namespace Admin.NET.Paichan;

/// <summary>
/// 配方物料输出参数
/// </summary>
public class ProduceFormulaMaterialOutput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// 配方
    /// </summary>
    public long? ProduceFormulaId { get; set; } 
    
    /// <summary>
    /// 配方 描述
    /// </summary>
    public string ProduceFormulaIdProduceFormulaCode { get; set; } 
    
    /// <summary>
    /// 物料
    /// </summary>
    public long? MaterialId { get; set; } 
    
    /// <summary>
    /// 物料 描述
    /// </summary>
    public string MaterialIdMaterialCode { get; set; } 
    
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
 

