using System;
using System.Collections.Generic;
using System.Text;

namespace Yuque.Infrastructure.Options
{
    public interface IOptions
    {
        /// <summary>
        /// 配置节点名称
        /// </summary>
        string SectionName { get; }
    }
}
