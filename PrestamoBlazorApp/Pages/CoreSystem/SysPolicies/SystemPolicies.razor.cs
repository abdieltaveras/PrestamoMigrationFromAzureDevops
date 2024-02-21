using DevBox.Core.Classes.Configuration;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared.Components.Base;

namespace PrestamoBlazorApp.Pages.CoreSystem.SysPolicies
{
    public partial class SystemPolicies : BasePage
    {
        [Inject] private SystemPoliciesService systemPoliciesSvr { get; set; }
        SysPolicyCategory systemPolicies;
        MudForm form;
        bool success;
        string[] errors = { };
        List<SelectKeyValue> keysValues = new List<SelectKeyValue>();
       
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await GetPolicies();
        }

        private async Task GetPolicies()
        {
            var ps = await systemPoliciesSvr.GetPoliciesAsync();
            systemPolicies = ps.FirstOrDefault();
            loadKeys();
        }

        protected async Task SaveChanges()
        {
            foreach (var pol in keysValues)
            {
                systemPolicies.SetPolicyValue(pol.PolID, pol.Value);
            }
            await systemPoliciesSvr.SavePolicies(systemPolicies);
            await GetPolicies();
        }
        protected void CancelChanges()
        {
            loadKeys();
        }
        private void loadKeys()
        {
            keysValues.Clear();
            systemPolicies.Policies.ForEach(p => keysValues.Add(new SelectKeyValue() { PolID = p.id, Value = p.value }));
            foreach (var cat in systemPolicies.Categories)
            {
                cat.Policies.ForEach(p => keysValues.Add(new SelectKeyValue() { PolID = p.id, Value = p.value }));
            }
        }
        bool isChanged(SelectKeyValue keyValue) => systemPolicies.GetPolicy(keyValue.PolID).value != keyValue.Value;
    }
    public class SelectKeyValue : IComparable<SelectKeyValue>, IEquatable<SelectKeyValue>, IComparable<SysPolicy>, IEquatable<SysPolicy>
    {
        public string PolID { get; set; }
        public string Value { get; set; }        
        public int CompareTo(SelectKeyValue other) => PolID.CompareTo(other.PolID);
        public bool Equals(SelectKeyValue other) => PolID.Equals(other.PolID, StringComparison.InvariantCultureIgnoreCase);
        public override string ToString() => Value;
        int IComparable<SysPolicy>.CompareTo(SysPolicy other) => PolID.CompareTo(other.id);
        bool IEquatable<SysPolicy>.Equals(SysPolicy other) => PolID.Equals(other.id, StringComparison.InvariantCultureIgnoreCase);
    }
}
