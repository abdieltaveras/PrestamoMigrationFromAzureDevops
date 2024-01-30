using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Layout.Components
{
    public partial class YearCalendar : ComponentBase
    {
        int _year = 0;
        [Parameter]
        public int Year { get => _year; set { _year = value; OnYearChange(_year); } }
        [Parameter]
        public Dictionary<DateTime, string> MarkedDates { get; set; } = new Dictionary<DateTime, string>();
        [Parameter]
        public bool DisablePast { get; set; } = false;
        [Parameter]
        public bool DisableFuture { get; set; } = false;
        [Parameter]
        public Action<DateTime, string> OnDayClick { get; set; } = (dt, desc) => { };
        [Parameter]
        public Action<int> OnYearChange { get; set; } = (year) => { };
    }
}
