namespace Admin.NET.Paichan;

    /// <summary>
    /// 检验标准项输出参数
    /// </summary>
    public class ExamineProjectDto
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// 检验项目编号
        /// </summary>
        public string ExamineProjectCode { get; set; }
        
        /// <summary>
        /// 检验项目名称
        /// </summary>
        public string ExamineProjectName { get; set; }
        
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
