using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

/// <summary>
/// 列表展示基础输入参数
/// </summary>
public class TableColumnBaseInput
{
    /// <summary>
    /// 页面
    /// </summary>
    public virtual string? PageType { get; set; }

    /// <summary>
    /// 字段
    /// </summary>
    public virtual string? Prop { get; set; }

    /// <summary>
    /// 展示
    /// </summary>
    public virtual string? Lable { get; set; }

    /// <summary>
    /// 列宽
    /// </summary>
    public virtual string? Width { get; set; }

    /// <summary>
    /// 是否隐藏
    /// </summary>
    public virtual bool? IsHidden { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public virtual int? Sort { get; set; }

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
/// 列表展示分页查询输入参数
/// </summary>
public class TableColumnInput : BasePageInput
{
    /// <summary>
    /// 关键字查询
    /// </summary>
    public string? SearchKey { get; set; }

    /// <summary>
    /// 页面
    /// </summary>
    public string? PageType { get; set; }

    /// <summary>
    /// 字段
    /// </summary>
    public string? Prop { get; set; }

    /// <summary>
    /// 展示
    /// </summary>
    public string? Lable { get; set; }

}

/// <summary>
/// 列表展示分页查询输入参数
/// </summary>
public class QueryTableColumnInput
{

    /// <summary>
    /// 页面
    /// </summary>
    public string? PageType { get; set; }

    /// <summary>
    /// 字段
    /// </summary>
    public string? Prop { get; set; }

    /// <summary>
    /// 展示
    /// </summary>
    public string? Lable { get; set; }

}

/// <summary>
/// 列表展示增加输入参数
/// </summary>
public class AddTableColumnInput : TableColumnBaseInput
{
}

/// <summary>
/// 列表展示删除输入参数
/// </summary>
public class DeleteTableColumnInput : BaseIdInput
{
}

/// <summary>
/// 列表展示更新输入参数
/// </summary>
public class UpdateTableColumnInput : TableColumnBaseInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "主键Id不能为空")]
    public long Id { get; set; }

}

/// <summary>
/// 列表展示更新输入参数
/// </summary>
public class UpdateTableColumnsInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long? Id { get; set; }
    /// <summary>
    /// 字段
    /// </summary>
    public virtual string? Prop { get; set; }

    /// <summary>
    /// 展示
    /// </summary>
    public virtual string? Lable { get; set; }

    /// <summary>
    /// 列宽
    /// </summary>
    public virtual string? Width { get; set; }

    /// <summary>
    /// 是否隐藏
    /// </summary>
    public virtual bool? IsHidden { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public virtual int? Sort { get; set; }

}

/// <summary>
/// 列表展示主键查询输入参数
/// </summary>
public class QueryByIdTableColumnInput : DeleteTableColumnInput
{

}
