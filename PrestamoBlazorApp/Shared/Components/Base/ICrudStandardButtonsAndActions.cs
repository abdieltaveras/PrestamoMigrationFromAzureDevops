using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Base
{
    public interface ICrudStandardButtonsAndActions<TType>
    {
        void BtnAddClick(TType obj);
        void BtnEdtClick(TType obj);
        void BtnDelClick(TType obj);
        bool BtnAddEnabled(TType obj);
        bool BtnEdtEnabled(TType obj);
        bool BtnDelEnabled(TType obj);

        bool BtnAddShow();
        bool BtnEdtShow();
        bool BtnDelShow();


    }

}
