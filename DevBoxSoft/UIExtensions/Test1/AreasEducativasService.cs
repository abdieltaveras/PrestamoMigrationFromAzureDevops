using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    public class AreasEducativasService : ServiceBase
    {
        internal async Task<IEnumerable<AreaEducativa>> GetAreaEducativas() => await GetAsync<AreaEducativa>("api/getAreas", new { });
        protected AreasEducativasService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {
        }
    }
}
