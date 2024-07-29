using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

    /// <summary>
    /// 物料类别基础输入参数
    /// </summary>
    public class MaterialTypeBaseInput
    {
        /// <summary>
        /// 物料类型
        /// </summary>
        public virtual string MaterialTypeName { get; set; }
        
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
    /// 物料类别分页查询输入参数
    /// </summary>
    public class MaterialTypeInput : BasePageInput
    {
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string? SearchKey { get; set; }

        /// <summary>
        /// 物料类型
        /// </summary>
        public string? MaterialTypeName { get; set; }
        
    }

    /// <summary>
    /// 物料类别增加输入参数
    /// </summary>
    public class AddMaterialTypeInput : MaterialTypeBaseInput
    {
        /// <summary>
        /// 物料类型
        /// </summary>
        [Required(ErrorMessage = "物料类型不能为空")]
        public override string MaterialTypeName { get; set; }
        
    }

    /// <summary>
    /// 物料类别删除输入参数
    /// </summary>
    public class DeleteMaterialTypeInput : BaseIdInput
    {
    }

    /// <summary>
    /// 物料类别更新输入参数
    /// </summary>
    public class UpdateMaterialTypeInput : MaterialTypeBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// 物料类别主键查询输入参数
    /// </summary>
    public class QueryByIdMaterialTypeInput : DeleteMaterialTypeInput
    {

    }
