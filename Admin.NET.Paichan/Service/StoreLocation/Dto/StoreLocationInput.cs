using Admin.NET.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.NET.Paichan;

    /// <summary>
    /// 库位管理基础输入参数
    /// </summary>
    public class StoreLocationBaseInput
    {
        /// <summary>
        /// 库位编号
        /// </summary>
        public virtual string StoreLocationCode { get; set; }
        
        /// <summary>
        /// 库位名称
        /// </summary>
        public virtual string StoreLocationName { get; set; }
        
        /// <summary>
        /// 仓库外键
        /// </summary>
        public virtual long StoreId { get; set; }
        
        /// <summary>
        /// 仓库编号
        /// </summary>
        public virtual string StoreCode { get; set; }
        
        /// <summary>
        /// 仓库名称
        /// </summary>
        public virtual string StoreName { get; set; }
        
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
    /// 库位管理分页查询输入参数
    /// </summary>
    public class StoreLocationInput : BasePageInput
    {
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string? SearchKey { get; set; }

        /// <summary>
        /// 库位编号
        /// </summary>
        public string? StoreLocationCode { get; set; }
        
        /// <summary>
        /// 库位名称
        /// </summary>
        public string? StoreLocationName { get; set; }
        
        /// <summary>
        /// 仓库外键
        /// </summary>
        public long? StoreId { get; set; }
        
    }

    /// <summary>
    /// 库位管理增加输入参数
    /// </summary>
    public class AddStoreLocationInput : StoreLocationBaseInput
    {
        /// <summary>
        /// 库位编号
        /// </summary>
        [Required(ErrorMessage = "库位编号不能为空")]
        public override string StoreLocationCode { get; set; }
        
        /// <summary>
        /// 库位名称
        /// </summary>
        [Required(ErrorMessage = "库位名称不能为空")]
        public override string StoreLocationName { get; set; }
        
        /// <summary>
        /// 仓库外键
        /// </summary>
        [Required(ErrorMessage = "仓库外键不能为空")]
        public override long StoreId { get; set; }
        
    }

    /// <summary>
    /// 库位管理删除输入参数
    /// </summary>
    public class DeleteStoreLocationInput : BaseIdInput
    {
    }

    /// <summary>
    /// 库位管理更新输入参数
    /// </summary>
    public class UpdateStoreLocationInput : StoreLocationBaseInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "主键Id不能为空")]
        public long Id { get; set; }
        
    }

    /// <summary>
    /// 库位管理主键查询输入参数
    /// </summary>
    public class QueryByIdStoreLocationInput : DeleteStoreLocationInput
    {

    }
