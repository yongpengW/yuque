using System;
using System.Collections.Generic;
using System.Text;

namespace Yuque.Core.Dtos.Dashboard
{
    public class DashboardChartDataQueryDto
    {
        public DateTimeOffset? Month { get; set; }
        public DateTimeOffset? Year { get; set; }
    }
}
