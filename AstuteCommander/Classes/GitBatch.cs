using System.IO;
using System.Collections.Generic;

namespace AstuteCommander.Classes
{
    public class GitBatch
    {
        protected string _fileName = string.Empty;
        protected List<string> _lines = new List<string>();
        protected string _redirectFile = string.Empty;
        protected string _redirectError = string.Empty;
        protected string _redirectNormal = string.Empty;
        protected bool _redirect = false;

        public GitBatch(string file)
        {
            _fileName = file;
        }
        public GitBatch()
        {
            _fileName = Path.Combine(Path.GetTempPath() + Path.GetTempFileName());
        }
        public bool Redirect
        {
            get { return _redirect; }
            set { _redirect = value; }
        }
        public void AddLine(string text)
        {
            _lines.Add(text);
        }
        public void AddCheckout(string local, string source)
        {
            AddLine($"git checkout {local} 2>/dev/null || git checkout -b {source} ");
        }
        public void AddFolderLine(string text)
        {
            _lines.Add(text.Replace(@"\", @"\\"));
        }
        public void AddReset()
        {
            AddLine($"git reset --hard");
        }
        public void AddCD(string text)
        {
//            var temp = text.Replace(@"C:\", "c/").Replace(@"\", @"/");
//            if (temp.Contains(" "))
//            {
//                var items = temp.Split("/");
//                foreach (var item in items)
//                {
//                    if (item.Contains(" "))
//                        _lines.Add($"cd /{item.Replace(" ", @"\ ")}");
//                    else
//                        _lines.Add($"cd /{item}");
//                }
//            }
//            else
                _lines.Add("cd \"" + text + "\"");
        }
        public string FileName => _fileName;
        public string RedirectFile
        {
            get { return _redirectFile; }
            set { _redirectFile = value; }
        }
        public string RedirectNormal
        {
            get { return _redirectNormal; }
            set { _redirectNormal = value; }
        }
        public string RedirectError
        {
            get { return _redirectError; }
            set { _redirectError = value; }
        }
        public void Write()
        {
            using (var sw = new StreamWriter(_fileName))
            {
                var redirector = ">";
                foreach (var line in _lines)
                {
                    var outLine = line;
                    if (_redirect)
                    {
                        if (!string.IsNullOrEmpty(RedirectFile))
                            outLine += $" {redirector}'{RedirectFile}'";
                        if (!string.IsNullOrEmpty(RedirectNormal))
                            outLine += $" 1{redirector}'{RedirectNormal}'";
                        if (!string.IsNullOrEmpty(RedirectError))
                            outLine += $" 2{redirector}'{RedirectError}'";
                        redirector = ">>";
                    }

                    sw.WriteLine(outLine);
                }
            }
        }
        public string WriteBuffer()
        {
            string buffer = string.Empty;
            var redirector = ">";
            foreach (var line in _lines)
            {
                var outLine = line;
                if (_redirect)
                {
                    if (!string.IsNullOrEmpty(RedirectFile))
                        outLine += $" {redirector}'{RedirectFile}'";
                    if (!string.IsNullOrEmpty(RedirectNormal))
                        outLine += $" 1{redirector}'{RedirectNormal}'";
                    if (!string.IsNullOrEmpty(RedirectError))
                        outLine += $" 2{redirector}'{RedirectError}'";
                    redirector = ">>";
                }

                buffer += outLine + @"\n\l\";
            }
            return buffer;
        }
        public void Delete()
        {
            File.Delete(_fileName);
            if (!string.IsNullOrEmpty(_redirectError))
                File.Delete(_redirectError);
            if (!string.IsNullOrEmpty(_redirectFile))
                File.Delete(_redirectFile);
            if (!string.IsNullOrEmpty(_redirectNormal))
                File.Delete(_redirectNormal);
        }

        public void GitRun(string startingFolder, string userName, string gitExePath)
        {
            if (File.Exists(_fileName))
            {
                var command = $"{_fileName}";

                if (!string.IsNullOrEmpty(command))
                {
                    if (Directory.Exists(startingFolder))
                        RunProcess(startingFolder, command, userName, gitExePath);
                    else
                        throw new System.Exception(
                            "Specified starting directory does not exist, please find and run BAT file manually");
                }
                else
                    throw new System.Exception("Unable to locate GIT");
            }
            else  // Could be a stream!!!
            {
                var command = WriteBuffer();

                if (!string.IsNullOrEmpty(command))
                {
                    if (Directory.Exists(startingFolder))
                        RunProcess(startingFolder, userName, gitExePath);
                    else
                        throw new System.Exception(
                            "Specified starting directory does not exist, please find and run BAT file manually");
                }
                else
                    throw new System.Exception("Unable to locate GIT");
            }
        }

        protected void RunProcess(string workingDirectory, string command, string userName, string gitExePath)
        {
            try
            {
                var proc1 = new System.Diagnostics.ProcessStartInfo()
                {
                    WorkingDirectory = workingDirectory,
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
                    UseShellExecute = false,
                    FileName = gitExePath + @"\sh.exe", // @"C:\Program Files (x86)\Git\bin\sh.exe";
                    Arguments = "\"" + command + "\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    Verb = $"runas "
                };

                var OutputProcessResults = string.Empty;

                using (var p = System.Diagnostics.Process.Start(proc1))
                {
                    using (StreamReader reader = p.StandardOutput)
                    {
                        string result = reader.ReadToEnd();
                        //Response.Write(result);
                        System.Diagnostics.Debug.WriteLine("GIT BATCH  " + result);
                    }

                    if (p.ExitCode > 1)
                    {
                        throw new System.Exception(p.ExitCode.ToString() + " => " + p.StandardError.ReadToEnd());
                    }

               }
            }
            catch (System.Exception ex)
            {
                var i = ex.Message;
                throw;
            }
        }

        protected void RunProcess(string workingDirectory, string userName, string gitExePath)
        {
            try
            {
                var proc1 = new System.Diagnostics.ProcessStartInfo()
                {
                    WorkingDirectory = workingDirectory,
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
                    UseShellExecute = false,
                    FileName = gitExePath + @"\sh.exe", // @"C:\Program Files (x86)\Git\bin\sh.exe";
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    RedirectStandardError = true,
                    Verb = $"runas "
                };

                var OutputProcessResults = string.Empty;

                var proc = new System.Diagnostics.Process();

                using (proc)
                {
                    proc.StartInfo = proc1;
                    proc.Start();

                    _lines.ForEach(command => proc.StandardInput.WriteLine(command));
                }
            }
            catch (System.Exception ex)
            {
                var i = ex.Message;
                throw;
            }
        }

    }
}
