using Blazored.LocalStorage;
using DevBox.Core.Classes.UI;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace UIClient.Services
{
    public class ActionsManagerService : HttpServiceBase
    {
        public string GetFormAction(DevBox.Core.Access.Action action)
        {
            var form = getFormByValue(action.Value);
            return form;
        }

        private string getFormByValue(string value) => forms.ContainsKey(value) ? $"/{forms[value]}" : value;

        Dictionary<string, string> forms = new Dictionary<string, string>
        {

        };
        //Dictionary<string, string> forms = new Dictionary<string, string>
        //{
        //    {"8d8fd452-0ad9-44b2-b8be-f49a7c36ae00","AreasEducativas"},
        //    {"2688fb13-293d-461f-9024-0589eb956b55","Aulas"},
        //    {"453e3779-855f-47cd-bfba-918c159d31ab","CatálogoCuentas"},
        //    {"44f855a3-78f7-4e2e-bca3-eb8e37f2fb60","Cursos"},
        //    {"88aaddf0-9b98-45ca-90cd-acc46152d407","DíasFeriados"},
        //    {"90b8ac00-efdb-421d-9c6f-cfcb994c48ed","ListadoArticulos"},
        //    {"9498ef8f-4436-47f4-842a-c1cb1152dce6","AsignacionesCarnets"},
        //    {"34c9bf6c-f7ff-4d08-8f5c-65735fc46646","Matriculación"},
        //    {"14f7880f-a1d5-418f-81a1-be9cd2b4d467","RegistroNotas"},
        //    {"3269d450-5690-4276-843f-b2ee3a77e013","ListadoEstudiantes"},
        //    {"77c8726e-83ba-463d-9a48-d17825190f00","Cargos"},
        //    {"c2afba87-2882-4a30-a57e-8b82b449f3b7","ImportsGrades"},
        //    {"1d3006c3-cfe7-408e-a81f-ba860adec118","InformeCargos"},
        //    {"10b451be-4bc8-4022-9d40-6df0ccb5d1a5","Grupos"},
        //    {"cf3693a0-0e52-4d0d-8799-847335897c3f","Usuarios"},
        //    {"ce5eda86-977c-4299-bf4b-df02c6c9b719","SysPolicies"},
        //    {"ee7daf58-9dfa-4304-a7f9-4a00403c0cde","ActionManager"}
        //};

        public ActionsManagerService(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService, NotificationService notificationService) : base("", clientFactory, configuration, localStorageService, notificationService)
        {
        }
    }
}
