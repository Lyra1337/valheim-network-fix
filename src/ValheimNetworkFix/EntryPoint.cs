using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ValheimNetworkFix
{
    public class EntryPoint
    {
        [DllExport("Initialize")]
        public static void Initialize()
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
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
    }
}
