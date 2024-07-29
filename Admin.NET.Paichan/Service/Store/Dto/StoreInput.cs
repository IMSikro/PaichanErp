using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

    /// <summary>
    /// 仓库基础输入参数
    /// </summary>
    public class StoreBaseInput
    {
        /// <summary>
        /// 仓库编号
        /// </summary>
        public virtual string StoreCode { get; set; }
        
        /// <summary>
        /// 仓库名称
        /// </summary>
        public virtual string StoreName { get; set; }
        
        /// <summary>
        /// 库位
        /// </summary>
        public virtual string? StoreLocation { get; set; }
        
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
        
        /// <summary>
        /// 库位列表
        /// </summary>
        public virtual List<StoreLocationBaseInput>? StoreLocations { get; set; }

    }

    /// <summary>
    /// 仓库分页查询输入参数
    /// </summary>
    public class StoreInput : BasePageInput
    {
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string? SearchKey { get; set; }

        /// <summary>
        /// 仓库编号
        /// </summary>
        public string? StoreCode { get; set; }
        
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string? StoreName { get; set; }
        
        /// <summary>
        /// 库位
        /// </summary>
        public string? StoreLocation { get; set; }
        
    }

    /// <summary>
    /// 仓库增加输入参数
    /// </summary>
    public class AddStoreInput : StoreBaseInput
    {
        /// <summary>
        /// 仓库编号
        /// </summary>
        [Required(ErrorMessage = "仓库编号不能为空")]
        public override string StoreCode { get; set; }
        
        /// <summary>
        /// 仓库名称
        /// </summary>
        [Required(ErrorMessage = "仓库名称不能为空")]
        public override string StoreName { get; set; }

}

    /// <summary>
    /// 仓库删除输入参数
    /// </summary>
    public class DeleteStoreInput : BaseIdInput
    {
    }

    /// <summary>
    /// 仓库更新输入参数
    /// </summary>
    public class UpdateStoreInput : StoreBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }

        public new List<UpdateStoreLocationInput>? StoreLocations { get; set; }
    }

    /// <summary>
    /// 仓库主键查询输入参数
    /// </summary>
    public class QueryByIdStoreInput : DeleteStoreInput
    {

    }
