using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace ValheimNetworkFix
{
    public class EntryPoint
    {
        [DllExport("Initialize")]
        public static void Initialize()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            AddLog("found assemblies:");
            AddLog(String.Join(Environment.NewLine, assemblies.Select(x => x.FullName)));

            var assembly = assemblies
                .Single(x => x.FullName.Contains("assembly_valheim") == true);
            //var assembly = Assembly.Load("assembly_valheim, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");

            //var type = Type.GetType("ZDOMan");
            //var type = ZDOMan.instance.GetType();
            var type = assembly.GetType("ZDOMan");
            var instanceGetter = type.GetProperty("instance", BindingFlags.Public | BindingFlags.Static);
            var instance = instanceGetter.GetValue(null, null);
            var field = type.GetField("m_dataPerSec", BindingFlags.NonPublic | BindingFlags.Instance);

            var value = (int)field.GetValue(instance);
            field.SetValue(instance, value * 10); // increase network limit by factor 10 (default 50kb/s to 500kb/s)

            //MessageBox.Show("done");

            //Debugger.Break();
        }

        private static void AddLog(string text)
        {
            File.AppendAllText(@"C:\Users\Lyra\Desktop\valheim_network.log", String.Concat(text, Environment.NewLine));
        }
    }
}
