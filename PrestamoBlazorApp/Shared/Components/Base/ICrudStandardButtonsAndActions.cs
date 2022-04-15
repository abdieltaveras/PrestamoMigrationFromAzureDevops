using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Base
{
    public interface ICrudStandardButtonsAndActions<TType>
    {
        Task BtnAddClick(TType obj);
        Task BtnEdtClick(TType obj);
        Task BtnDelClick(TType obj);
        bool BtnAddEnabled(object obj);
        bool BtnEdtEnabled(object obj);
        bool BtnDelEnabled(object obj);

        bool BtnAddShow();
        bool BtnEdtShow();
        bool BtnDelShow();


    }

}
