using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LogcatWrapper.Base
{
    public class Logcat_Hook
    {
        public delegate void ParsedLogLineReceivedEventHandler(Logcat_LogLine line);
        public delegate void RawLogLineReceivedEventHandler(string line);

        public event ParsedLogLineReceivedEventHandler OnParsedLogLineReceivedEvent;
        public event RawLogLineReceivedEventHandler OnRawLogLineReceivedEvent;

        private Process _process;

        public Logcat_Hook(string pathToAdb, string deviceIdArgument, string arguments, bool hasWindow, string command = "logcat")
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = pathToAdb,
                Arguments = deviceIdArgument + command + arguments,

                CreateNoWindow = !hasWindow,
                UseShellExecute = false,

                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            _process = new Process
            {
                EnableRaisingEvents = true,
                StartInfo = startInfo
            };

            _process.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(OnOutputDataReceived);
            _process.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(OnOutputDataReceived);
        }

        public bool Start(bool threadBlocking)
        {
            if(_process.Start())
            {
                _process.BeginErrorReadLine();
                _process.BeginOutputReadLine();

                if (threadBlocking)
                    _process.WaitForExit();

                return true;
            }

            return false;
        }

        private void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            var line = e.Data.Trim();

            if (!string.IsNullOrEmpty(line))
            {   
                if (OnRawLogLineReceivedEvent != null)
                    OnRawLogLineReceivedEvent.Invoke(line);

                if (OnParsedLogLineReceivedEvent != null)
                {
                    if (!line.StartsWith("-"))
                    {
                        var parsedLine = new Logcat_LogLine(line);
                        OnParsedLogLineReceivedEvent.Invoke(parsedLine);
                    }
                }
            }
            
        }
    }
}
