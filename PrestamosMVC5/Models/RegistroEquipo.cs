using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Models
{
    /// <summary>
    ///  para registrar el equipo mediante un cookie
    /// </summary>
    public static class RegistroEquipo
    {
        private static string Key = "989##kjkjkds#!@#!@kjj";
        private static HttpCookie EquipoCookie = new HttpCookie(Key);
        private static List<string> valores=new List<string>();

        private static void SetValores()
        {
            valores.Add("001");
            valores.Add("002");
            valores.Add("003");
            valores.Add("004");
            valores.Add("005");
        }

        public static string getValue
        {
            get {
                    SetValores();
                    var index = new Random().Next(4);
                    return valores[index];
                }
        }
        public static void Registrar(HttpResponseBase response, string value, int duracionEnDias = 365 )
        {
            
            
            EquipoCookie.Value = value;
            EquipoCookie.Expires = DateTime.Now.AddDays(duracionEnDias);
            EquipoCookie.HttpOnly = true;
            response.Cookies.Add(EquipoCookie);
        }


        public static void DesvincularEquipo(HttpContextBase context)
        {
            var cookie = context.Response.Cookies[Key];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
            }
        }

        public static bool EstaRegistrado(HttpRequestBase request)
        {
            
            var result = false;
            var cookie = request.Cookies[Key];
            if (cookie != null)
            {
                SetValores();
                var cookieValue = cookie.Values[0];
                SetValores();
                result = valores.Contains(cookieValue);
            }
            return result;
        }
    }
}