using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogcatWrapper.Base
{
    public class Logcat_LogLevel : IComparable<Logcat_LogLevel>
    {
        public enum Priority
        {
            /// <summary> Verbose (lowest priority) </summary>
            V,
            /// <summary> Debug </summary>
            D,
            /// <summary> Info </summary>
            I,
            /// <summary> Warning </summary>
            W,
            /// <summary> Error </summary>
            E,
            /// <summary> Fatal </summary>
            F,
            /// <summary> Silent (highest priority, on which nothing is ever printed) </summary>
            S
        }
        private Priority _priority;

        public Priority GetPriority() => _priority;
        public int GetPriorityLevel() => (int)_priority;
        public string GetPriorityString() => _priority.ToString();

        private Logcat_LogLevel(Priority priority)
        {
            _priority = priority;
        }

        public int CompareTo(Logcat_LogLevel other)
        {
            return GetPriorityLevel().CompareTo(other.GetPriorityLevel());
        }

        public static readonly Logcat_LogLevel Verbose  = new Logcat_LogLevel(Priority.V);
        public static readonly Logcat_LogLevel Debug    = new Logcat_LogLevel(Priority.D);
        public static readonly Logcat_LogLevel Info     = new Logcat_LogLevel(Priority.I);
        public static readonly Logcat_LogLevel Warning  = new Logcat_LogLevel(Priority.W);
        public static readonly Logcat_LogLevel Error    = new Logcat_LogLevel(Priority.E);
        public static readonly Logcat_LogLevel Fatal    = new Logcat_LogLevel(Priority.F);
        public static readonly Logcat_LogLevel Silent   = new Logcat_LogLevel(Priority.S);

        private static Dictionary<string, Logcat_LogLevel> _logLevelMap;

        private static void InitMap()
        {
            _logLevelMap = new Dictionary<string, Logcat_LogLevel>();
            AddLevel(Verbose);
            AddLevel(Debug);
            AddLevel(Info);
            AddLevel(Warning);
            AddLevel(Error);
            AddLevel(Fatal);
            AddLevel(Silent);
        }

        public static Logcat_LogLevel GetLevel(Priority symbol)
        {
            return GetLevel(symbol.ToString());
        }

        public static Logcat_LogLevel GetLevel(string symbol)
        {
            if(_logLevelMap == null || _logLevelMap.Count == 0)
                InitMap();

            return _logLevelMap[symbol];
        }

        private static void AddLevel(Logcat_LogLevel level)
        {
            _logLevelMap.Add(level.GetPriorityString(), level);
        }
    }
}
