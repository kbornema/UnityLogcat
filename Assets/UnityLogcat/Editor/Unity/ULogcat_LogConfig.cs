using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LogcatWrapper.Base;
using System;

namespace LogcatWrapper.Unity
{   
    [CreateAssetMenu]
    public class ULogcat_LogConfig : ScriptableObject
    {
        [Header("Runtime parameters")]
        [SerializeField]
        private Logcat_LogLevel.Priority _minPriority = Logcat_LogLevel.Priority.V;
        public Logcat_LogLevel.Priority MinPriority => _minPriority;

        [SerializeField]
        private bool _printEmptyMessages = true;
        public bool PrintEmptyMessages => _printEmptyMessages;

        [SerializeField]
        private bool _printDate = true;
        public bool PrintDate => _printDate;

        [SerializeField]
        private bool _printTime = true;
        public bool PrintTime => _printTime;

        [SerializeField]
        private bool _printAppName = true;
        public bool PrintAppName => _printAppName;

        [SerializeField]
        private bool _printPriority = true;
        public bool PrintPriority => _printPriority;

        [Header("Command line arguments")]
        [SerializeField]
        private bool _clearBeforeStart = true;
        public bool ClearBeforeStart => _clearBeforeStart;

        [SerializeField]
        private List<Logcat_LogFilter> _logFilters = default;
        public List<Logcat_LogFilter> LogFilters => _logFilters;

        public string GetCommandLineArgsString()
        {
            string args = "";

            args += Logcat_LogFilter.CreateCmdLineArg(_logFilters);      

            return args;
        }
    }
}
