using Microsoft.JSInterop;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared.Components.Base;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Catalogos
{

    public class CommonActionsForCatalogo : CommonActions, ICrudStandardButtonsAndActions<Catalogo>
    {
        Action<Catalogo> ShowEditor { get; }
        Action<Catalogo> ShowEditorForBorrar { get; }
        IJSRuntime JsRuntime { get; }
        public CommonActionsForCatalogo()
        {

        }
        public CommonActionsForCatalogo(Action<Catalogo> showEditor, Action<Catalogo> showEditorForBorrar,IJSRuntime jsRuntime)
        {
            ShowEditor = showEditor;
            JsRuntime = jsRuntime;
            ShowEditorForBorrar = showEditorForBorrar;
        }
        public void BtnAddClick(Catalogo obj)
        {
            ShowEditor(new Catalogo());
        }
        public void BtnEdtClick(Catalogo obj)
        {

            if (obj != null)
            {
                ShowEditor(obj);
            }
        }
        public void BtnDelClick(Catalogo obj)
        {
            ShowEditorForBorrar(obj);
        }
    }
    
}
