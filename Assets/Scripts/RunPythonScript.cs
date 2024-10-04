using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class RunPythonScript : MonoBehaviour
{
    private Process pythonProcess;
    
    
    private void Start()
    {
        StartPythonScript();
    }
    public void StartPythonScript()
    {
        // Path to the Python interpreter
        string pythonInterpreterPath = @"C:\Users\Tech Mehal\AppData\Local\Microsoft\WindowsApps\python.exe";

        // Path to the Python script
        string pythonScriptPath = @"/../computervision.py";

        // Configure the process start info
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = pythonInterpreterPath;
        startInfo.Arguments = pythonScriptPath;
        startInfo.UseShellExecute = false;
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardError = true;
        startInfo.CreateNoWindow = true;

        // Start the Python process
        pythonProcess = new Process();
        pythonProcess.StartInfo = startInfo;
        Debug.Log(pythonProcess.Id);

        pythonProcess.Start();
        pythonProcess.BeginOutputReadLine();
        pythonProcess.BeginErrorReadLine();
    }
    
    public void StopPythonScript()
    {
        if (pythonProcess != null && !pythonProcess.HasExited)
        {
            pythonProcess.Kill();
            pythonProcess.WaitForExit();
        }
    }

    private void OnApplicationQuit()
    {
        StopPythonScript();
    }
}
