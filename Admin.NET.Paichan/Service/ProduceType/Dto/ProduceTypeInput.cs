using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

/// <summary>
/// 产品类型基础输入参数
/// </summary>
public class ProduceTypeBaseInput
{
    /// <summary>
    /// 设备类型
    /// </summary>
    public virtual string TypeName { get; set; }

    /// <summary>
    /// 产品系列
    /// </summary>
    public virtual string? ProduceSeries { get; set; }

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
/// 产品类型分页查询输入参数
/// </summary>
public class ProduceTypeInput : BasePageInput
{
    /// <summary>
    /// 关键字查询
    /// </summary>
    public string? SearchKey { get; set; }

    /// <summary>
    /// 设备类型
    /// </summary>
    public string? TypeName { get; set; }

}

/// <summary>
/// 产品类型增加输入参数
/// </summary>
public class AddProduceTypeInput : ProduceTypeBaseInput
{
    /// <summary>
    /// 设备类型
    /// </summary>
    [Required(ErrorMessage = "设备类型不能为空")]
    public override string TypeName { get; set; }
}

/// <summary>
/// 产品类型删除输入参数
/// </summary>
public class DeleteProduceTypeInput : BaseIdInput
{
}

/// <summary>
/// 产品类型更新输入参数
/// </summary>
public class UpdateProduceTypeInput : ProduceTypeBaseInput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [Required(ErrorMessage = "主键Id不能为空")]
    public long Id { get; set; }

}

/// <summary>
/// 产品类型主键查询输入参数
/// </summary>
public class QueryByIdProduceTypeInput : DeleteProduceTypeInput
{

}
