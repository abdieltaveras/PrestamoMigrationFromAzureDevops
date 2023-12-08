using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBox.Core.Classes.UI
{
    public interface IHttpServiceBase
    {
        Task<IEnumerable<Type>> GetAsync<Type>(string endpoint, object search, bool requiresAuth = true);
        Task<TResult> PostAsync<Type, TResult>(string endpoint, Type body, object search = null, bool requiresAuth = true) where TResult : class;
    }
}
