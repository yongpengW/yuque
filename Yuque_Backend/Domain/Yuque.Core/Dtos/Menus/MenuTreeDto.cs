using System;
using System.Collections.Generic;
using System.Text;

namespace Yuque.Core.Dtos.Menus
{
    public class MenuTreeDto : MenuDto
    {
        /// <summary>
        /// 下级菜单
        /// </summary>
        public List<MenuTreeDto>? Children { get; set; } = new List<MenuTreeDto>();
    }
}
