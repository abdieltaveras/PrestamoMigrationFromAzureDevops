using Newtonsoft.Json.Linq;
using PrestamoBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Tests
{
    internal class TestUtil
    {
        public static bool ActionMustFail(Action action)
        {
            return (ActionMustSucceed(action) == false);
        }

        public static bool ActionMustSucceed(Action action)
        {
            bool successFullOperation = true;
            try { action(); 
            }
            catch (Exception ex)
            {
                successFullOperation = false;
            }
            return successFullOperation == true;
        }

    }
}
