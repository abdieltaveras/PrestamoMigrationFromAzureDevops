using DevBox.Core.Classes.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace UIClient.Utils
{
    static class AssemblyManager
    {
        private static Assembly[] additionalAssemblies = null;

        public static Assembly[] AdditionalAssemblies
        {
            get
            {
                if (additionalAssemblies == null)
                {
                    additionalAssemblies = loadAssemblies();
                }
                return additionalAssemblies;
            }
        }
        static Assembly[] loadAssemblies()
        {
            var folder = Path.Combine($"{AppContext.BaseDirectory}../../", "extensions");
            var assemblies = loadAssemblies(folder);
            return (assemblies.Count > 0) ? assemblies.ToArray() : null;
        }
        static List<Assembly> loadAssemblies(string folder)
        {
            var result = new List<Assembly>();
            IEnumerable<string> dlls = new List<string>();
            if (Directory.Exists(folder))
            {
                dlls = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories)
                                   .Select(f => Path.GetFullPath(f));
            }
            var dllNames = new List<string>();
            foreach (var dll in dlls)
            {
                if (!dll.EndsWith(".uiextension.dll")) { continue; }
                var dllName = Path.GetFileName(dll);
                if (dllNames.Contains(dllName)) { continue; }
                dllNames.Add(dllName);
                try
                {
                    var a = Assembly.LoadFile(dll);
                    result.Add(a);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return result;
        }
        static public void LoadSupportingAssembly(string dll) => loadSupportingAssembly(dll);
        static private void loadSupportingAssembly(string dll)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            byte[] rawAssembly = loadFile(dll);
            byte[] rawSymbolStore = loadFile(Path.ChangeExtension(dll, ".pdb"));
            var asm = currentDomain.Load(rawAssembly, rawSymbolStore);
        }
        static public IUIAction LoadTask(string dll) => loadTask(dll);
        static private IUIAction loadTask(string dll)
        {
            Assembly a = Assembly.LoadFile(dll);
            Type[] types = a.GetTypes();
            foreach (Type type in types)
            {
                // Does this class support the transport interface?
                Type typeTest = type.GetInterface("IUIAction");
                if (typeTest == null)
                {
                    // Not supported.
                    continue;
                }
                // This class supports the interface. Instantiate it.
                var obj = a.CreateInstance(type.FullName) as IUIAction;
                return obj;
            }
            return null;
        }
        // Loads the content of a file to a byte array. 
        static byte[] loadFile(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            byte[] buffer = new byte[(int)fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            fs.Close();
            return buffer;
        }
    }
}
