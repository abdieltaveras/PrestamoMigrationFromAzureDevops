namespace PrestamoBlazorApp.Pages.Prestamos.Components.PrestamoByColumnSelected;

using PrestamoBlazorApp.Domain;
using PrestamoBlazorApp.Models;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



public class PrestamoGetDataByColumnSelected : IGetDataByColumSelection<PrestamoClienteUIGetParamWtSearchText>
{

    public IEnumerable<EnumModel> Items { get; private set; } = new List<EnumModel>();

    public string OptionSelected => this.SelectedItem.ToString();

    public bool IsGetParamHasValue { get; private set; } = false;

    private Dictionary<eOpcionesSearchPrestamo, Func<string, PrestamoClienteUIGetParamWtSearchText>> DictOpciones { get; set; }
    private eOpcionesSearchPrestamo SelectedItem { get; set; } = eOpcionesSearchPrestamo.Nombres;

    private void SetSelectedItem(eOpcionesSearchPrestamo indexSearchOption)
    {
        bool isDefined = Enum.IsDefined(typeof(eOpcionesSearchPrestamo), indexSearchOption);
        //if (!isDefined) this.SelectedItem = eOpcionesSearchPrestamo.Indefinido;
        this.SelectedItem = (eOpcionesSearchPrestamo)indexSearchOption;
    }

    //public static string Cedula => eOpcionesSearchPrestamo.Cedula.ToString();

    public PrestamoGetDataByColumnSelected()
    {
        this.Items = GetSearchPersonOption();
        DictOpciones = CreateDictOpciones();
    }

    private IEnumerable<EnumModel> GetSearchPersonOption()
    {
        var result = Enum.GetValues(typeof(eOpcionesSearchPrestamo)).Cast<eOpcionesSearchPrestamo>().Select(c => new EnumModel { Value = (int)c, Text = c.ToString() }).ToList();
        //result.Remove(result.Last()); // quitar el eltimo elemento de la lista
        return result;
    }

    public async Task ExecGetDataAction(int indexSelected, string searchText, Func<PrestamoClienteUIGetParamWtSearchText, Task> func)
    {
        SetParamWithDictionary((eOpcionesSearchPrestamo)indexSelected, searchText);
        var resultParam = SetParamWithDictionary((eOpcionesSearchPrestamo)indexSelected, searchText);
        if (this.IsGetParamHasValue)
        {
            await func.Invoke(resultParam);
        }
    }
    private Dictionary<eOpcionesSearchPrestamo, Func<string, PrestamoClienteUIGetParamWtSearchText>> CreateDictOpciones()
    {
        var opciones = new Dictionary<eOpcionesSearchPrestamo, Func<string, PrestamoClienteUIGetParamWtSearchText>>();

        opciones.Add(eOpcionesSearchPrestamo.Nombres, (searchText) => new PrestamoClienteUIGetParamWtSearchText { Nombres = searchText });
        opciones.Add(eOpcionesSearchPrestamo.Apellidos, (searchText) => new PrestamoClienteUIGetParamWtSearchText { Apellidos = searchText });
        opciones.Add(eOpcionesSearchPrestamo.Matricula, (searchText) => new PrestamoClienteUIGetParamWtSearchText { Matricula = searchText });
        opciones.Add(eOpcionesSearchPrestamo.Garantia, (searchText) => new PrestamoClienteUIGetParamWtSearchText { IdGarantia = Convert.ToInt32(searchText) });
        opciones.Add(eOpcionesSearchPrestamo.Chasis, (searchText) => new PrestamoClienteUIGetParamWtSearchText { Chasis = searchText });
        opciones.Add(eOpcionesSearchPrestamo.Placa, (searchText) => new PrestamoClienteUIGetParamWtSearchText { Placa = searchText });



        IsGetParamHasValue = true;
        return opciones;
        //var result = opciones[SelectedItem];
    }
    public PrestamoClienteUIGetParamWtSearchText SetParam(eOpcionesSearchPrestamo searchOption, string searchText)
    {
        IsGetParamHasValue = false;
        return SetParamWithDictionary(searchOption, searchText);
        //return SetParamWithSwitch(searchOption, searchText);
    }

    private PrestamoClienteUIGetParamWtSearchText SetParamWithDictionary(eOpcionesSearchPrestamo searchOption, string searchText)
    {
        SetSelectedItem(searchOption);
        return DictOpciones.ContainsKey(SelectedItem) ? DictOpciones[SelectedItem].Invoke(searchText) : new PrestamoClienteUIGetParamWtSearchText();
    }
    private PrestamoClienteUIGetParamWtSearchText SetParamWithSwitch(eOpcionesSearchPrestamo searchOption, string searchText)
    {

        SetSelectedItem(searchOption);
        IsGetParamHasValue = true;
        var param = new PrestamoClienteUIGetParamWtSearchText();
        {
            switch (SelectedItem)
            {
                case eOpcionesSearchPrestamo.Nombres:
                    param.Nombres = searchText;
                    break;
                case eOpcionesSearchPrestamo.Matricula:
                    param.Matricula = searchText;
                    break;
                default:
                    IsGetParamHasValue = false;
                    break;
            }
        }
        return param;
    }

    public override string ToString() => this.OptionSelected;


}



