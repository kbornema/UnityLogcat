using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogcatWrapper.Base
{
    [System.Serializable]
    public class Logcat_LogFilter : Logcat_ACmdLineArg
    {
        public string ApplicationName = "*";
        public Logcat_LogLevel.Priority LogLevel = Logcat_LogLevel.Priority.V;
        public bool Enabled = true;

        public Logcat_LogFilter(string appName, Logcat_LogLevel.Priority logLevel)
        {
            ApplicationName = appName;
            LogLevel = logLevel;
        }

        public static string CreateCmdLineArg(IEnumerable<Logcat_LogFilter> filters)
        {
            string finalFilter = "";

            foreach (var filter in filters)
                finalFilter += filter.CreateCmdLineArg();

            return finalFilter;
        }

        public override string CreateCmdLineArg()
        {
            if (!Enabled)
                return "";

            return " " + ApplicationName + ":" + LogLevel;
        }
    }
}
