using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LogcatWrapper.Unity
{
    public class ULogcat_GlobalConfig
    {   
        private EditorPrefString _adbPath = new EditorPrefString("ULogcat.AdbPath");
        public EditorPrefString AdbPath => _adbPath;

        private EditorPrefString _logConfigAssetGUID = new EditorPrefString("ULogcat.LogConfigAssetGUID");
        public EditorPrefString LogConfigAssetGUID => _logConfigAssetGUID;

        private ULogcat_LogConfig _logConfigAsset;
        public ULogcat_LogConfig LogConfigAsset => _logConfigAsset;

        public ULogcat_GlobalConfig()
        {
            _adbPath.Init();
            _logConfigAssetGUID.Init();

            _logConfigAsset = AssetDatabase.LoadAssetAtPath<ULogcat_LogConfig>(AssetDatabase.GUIDToAssetPath(_logConfigAssetGUID.Value));
        }

        public void SetLogConfig(ULogcat_LogConfig logConfig)
        {
            _logConfigAsset = logConfig;
            _logConfigAssetGUID.SetValue(AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(_logConfigAsset)));
        }

        public abstract class AEditorPrefVar<T>
        {
            protected string _key;
            public string Key => _key;

            protected T _value = default;
            public T Value => _value;

            public AEditorPrefVar(string key)
            {
                _key = key;
            }

            public abstract void Init();
            public abstract void SetValue(T value);
        }

        public class EditorPrefString : AEditorPrefVar<string>
        {
            public EditorPrefString(string key) 
                : base(key)
            {
            }

            public override void Init()
            {
                _value = EditorPrefs.GetString(_key, _value);
            }

            public override void SetValue(string value)
            {
                if(_value != value)
                {
                    _value = value;
                    EditorPrefs.SetString(_key, _value);
                }
            }
        }
    }
}