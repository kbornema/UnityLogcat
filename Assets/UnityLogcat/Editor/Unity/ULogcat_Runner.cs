using LogcatWrapper.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LogcatWrapper.Unity
{   
    public class ULogcat_Runner
    {
        private Logcat_Hook _logcatHook;

        private ULogcat_LogConfig _logConfig;
        private ULogcat_GlobalConfig _globalConfig;

        public ULogcat_Runner(ULogcat_GlobalConfig globalConfig)
        {
            _globalConfig = globalConfig;
            _logConfig = _globalConfig.LogConfigAsset;
        }

        private void Start()
        {   
            string adbPath = _globalConfig.AdbPath.Value;

            if (string.IsNullOrEmpty(adbPath))
            {
                Debug.LogError("Path to Adb not set!");
                return;
            }

            if (_logConfig == null)
            {
                Debug.LogError("LogConfig for Adb not set!");
                return;
            }

            var deviceArgument = _globalConfig.DeviceId.Value.Trim();

            if (deviceArgument.Length > 0)
                deviceArgument = " -s " + deviceArgument + " ";

            if (_logConfig.ClearBeforeStart)
            {
                var clearHook = new Logcat_Hook(adbPath, deviceArgument, " -b all -c", false);
                clearHook.Start(true);
            }

            _logcatHook = new Logcat_Hook(adbPath, deviceArgument, _logConfig.GetCommandLineArgsString(), true);
        
            if(_logcatHook.Start(false))
            {
                Debug.Log("ULogcat_Runner started");
                _logcatHook.OnParsedLogLineReceivedEvent += OnParsedLogLineReceivedListener;
            }

            else
            {
                Debug.LogError("Could not start ULogcat_Runner. Check path and config!");
            }
        }

        private void OnParsedLogLineReceivedListener(Logcat_LogLine line)
        {
            if(!line.IsEmptyMessage || _logConfig.PrintEmptyMessages)
            {   
                var prio = line.LogLevel.GetPriority();

                if (prio >= _logConfig.MinPriority)
                {
                    var printSring = line.GetPrintableString(_logConfig.PrintDate, _logConfig.PrintTime, _logConfig.PrintPriority, _logConfig.PrintAppName);

                    if (prio >= Logcat_LogLevel.Priority.E)
                    {
                        Debug.LogError(printSring);
                    }

                    else if (prio >= Logcat_LogLevel.Priority.W)
                    {
                        Debug.LogWarning(printSring);
                    }

                    else
                    {
                        Debug.Log(printSring);
                    }
                }
            }
        }

        [MenuItem("Tools/Start Logcat")]
        public static void StartLogcat()
        {
            new ULogcat_Runner(new ULogcat_GlobalConfig()).Start();
        }
    }
}