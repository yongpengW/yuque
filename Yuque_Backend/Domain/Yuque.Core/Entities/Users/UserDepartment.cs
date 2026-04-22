using Yuque.Core.Entities.SystemManagement;
using Yuque.EFCore.Entities;

namespace Yuque.Core.Entities.Users
{
    /// <summary>
    /// 用户与组织单元关联（替代 User.DepartmentIds 字符串）
    /// </summary>
    public class UserDepartment : AuditedEntity
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 组织单元（当前指向 Region.Id）
        /// </summary>
        public long DepartmentId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual User? User { get; set; }

        /// <summary>
        /// 组织单元（Region）
        /// </summary>
        public virtual Region? Department { get; set; }
    }
}

