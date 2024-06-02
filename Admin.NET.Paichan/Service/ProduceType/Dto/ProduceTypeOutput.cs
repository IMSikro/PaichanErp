namespace Admin.NET.Paichan;

/// <summary>
/// 产品类型输出参数
/// </summary>
public class ProduceTypeOutput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// 设备类型
    /// </summary>
    public string? TypeName { get; set; }

    /// <summary>
    /// 产品系列
    /// </summary>
    public virtual string? ProduceSeries { get; set; }

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
 

