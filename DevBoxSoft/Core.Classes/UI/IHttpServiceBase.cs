using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBox.Core.Classes.UI
{
    public interface IHttpServiceBase
    {
        Task<IEnumerable<Type>> GetAsync<Type>(string endpoint, object search);
        Task<TResult> PostAsync<Type, TResult>(string endpoint, Type body, object search = null) where TResult : class;
    }
}
