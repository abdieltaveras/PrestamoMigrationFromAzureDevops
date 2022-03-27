using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Base
{
    public interface ICrudStandardButtonsAndActions
    {
        void BtnAddClick(object obj);

        void BtnEdtClick(object obj);

        void BtnDelClick(object obj);
        bool BtnAddEnabled(object obj);
        bool BtnEdtEnabled(object obj);
        bool BtnDelEnabled(object obj);

        bool BtnAddShow();
        bool BtnEdtShow();
        bool BtnDelShow();


    }

}
