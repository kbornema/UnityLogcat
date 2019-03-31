using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LogcatWrapper.Base
{
    public class Logcat_LogLine
    {
        private string _date;
        public string Date => _date;

        private string _time;
        public string Time => _time;

        private string _appName;
        public string AppName => _appName;

        private string _message;
        public string Message => _message;

        private Logcat_LogLevel _logLevel;
        public Logcat_LogLevel LogLevel => _logLevel;

        public bool IsEmptyMessage => String.IsNullOrEmpty(_message);

        public Logcat_LogLine(string line)
        {   
            _date = line.Substring(0, 5);
            _time = line.Substring(6, 12);
            var logLevelSymbol = line.Substring(31, 1);

            _logLevel = Logcat_LogLevel.GetLevel(logLevelSymbol);

            const int FLEXIBLE_PART_START_INDEX = 33;
            string flexiblePart = line.Substring(FLEXIBLE_PART_START_INDEX, line.Length - FLEXIBLE_PART_START_INDEX);
            int appNameEndIndex = flexiblePart.IndexOf(":");
            _appName = flexiblePart.Substring(0, appNameEndIndex).Trim();
            //+-1 to remove ":"
            _message = flexiblePart.Substring(appNameEndIndex + 1, flexiblePart.Length - appNameEndIndex -1).Trim();
        }

        public override string ToString()
        {
            return GetPrintableString(true, true, true, true);
        }

        public string GetPrintableString(bool date, bool time, bool priority, bool appName)
        {
            string result = "";

            if (date)
                result += _date + " | ";

            if (time)
                result += _time + " | ";

            if (priority)
                result += _logLevel.GetPriorityString() + " | ";

            if (appName)
                result += _appName + " | ";

            result += _message;

            return result;
        }
    }
}
