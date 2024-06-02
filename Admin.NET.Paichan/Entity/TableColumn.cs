using Admin.NET.Core;
using SqlSugar;

namespace Admin.NET.Paichan.Entity;

/// <summary>
/// 列表展示
/// </summary>
[SugarTable("PC_TableColumn","列表展示")]
[IncreTable]
public class TableColumn  : EntityTenant
{
    /// <summary>
    /// 页面
    /// </summary>
    [SugarColumn(ColumnName = "PageType", ColumnDescription = "页面", Length = 255)]
    public string? PageType { get; set; }
    
    /// <summary>
    /// 字段
    /// </summary>
    [SugarColumn(ColumnName = "Prop", ColumnDescription = "字段", Length = 32)]
    public string? Prop { get; set; }
    
    /// <summary>
    /// 展示
    /// </summary>
    [SugarColumn(ColumnName = "Lable", ColumnDescription = "展示", Length = 32)]
    public string? Lable { get; set; }
    
    /// <summary>
    /// 列宽
    /// </summary>
    [SugarColumn(ColumnName = "Width", ColumnDescription = "列宽", Length = 32)]
    public string? Width { get; set; }
    
    /// <summary>
    /// 是否隐藏
    /// </summary>
    [SugarColumn(ColumnName = "IsHidden", ColumnDescription = "是否隐藏")]
    public bool? IsHidden { get; set; }
    
    /// <summary>
    /// 排序
    /// </summary>
    [SugarColumn(ColumnName = "Sort", ColumnDescription = "排序")]
    public int? Sort { get; set; }
    
}
