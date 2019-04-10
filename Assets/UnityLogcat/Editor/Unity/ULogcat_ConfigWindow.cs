using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using LogcatWrapper.Base;

namespace LogcatWrapper.Unity
{   
    public class ULogcat_ConfigWindow : EditorWindow
    {
        private ULogcat_GlobalConfig _globalConfig;

        private void Init()
        {
            _globalConfig = new ULogcat_GlobalConfig();
        }

        private void OnGUI()
        {
            var adbPathPref = _globalConfig.AdbPath;
            adbPathPref.SetValue(EditorGUILayout.TextField("Path to Adb", adbPathPref.Value));

            var logConfig = (ULogcat_LogConfig)EditorGUILayout.ObjectField("Log Config", _globalConfig.LogConfigAsset, typeof(ULogcat_LogConfig), false);

            if(logConfig != _globalConfig.LogConfigAsset)
                _globalConfig.SetLogConfig(logConfig);

            var advDeviceId = _globalConfig.DeviceId;
            advDeviceId.SetValue(EditorGUILayout.TextField("Device Id", advDeviceId.Value));
            EditorGUILayout.LabelField("DeviceId is only needed if multiple devices are connected.");

            if (GUILayout.Button("Start Logcat"))
                ULogcat_Runner.StartLogcat();
        }

        [MenuItem("Tools/Configure Logcat")]
        public static void OpenLogcatConfiguration()
        {
            GetWindow<ULogcat_ConfigWindow>("Logcat Config Window").Init();
        }

        private class KeyValue
        {
            public string Key;
            public string Value;

            public KeyValue(string key)
            {
                this.Key = key;
            }

            public void InitValue()
            {
                Value = EditorPrefs.GetString(Key, Value);
            }

            public void SetValue(string value)
            {
                if(Value != value)
                {
                    Value = value;
                    EditorPrefs.SetString(Key, Value);
                }
            }
        }
    }
}