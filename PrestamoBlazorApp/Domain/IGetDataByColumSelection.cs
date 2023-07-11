
using PrestamoBlazorApp.Models;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Domain
{
    public interface IGetDataByColumSelection<TGetParam> where TGetParam : class
    {
        bool IsGetParamHasValue { get; }
        IEnumerable<EnumModel> Items { get; }
        string OptionSelected { get; }

        Task ExecGetDataAction(int indexSelected, string searchText, Func<TGetParam, Task> service);

        TGetParam SetParam(eOpcionesSearchPrestamo searchOption, string searchText);
        string ToString();
    }
}