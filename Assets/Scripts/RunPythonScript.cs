using System.Diagnostics; // For Process class
using UnityEngine;

public class RunPythonScript : MonoBehaviour
{
    private Process pythonProcess;

    void Start()
    {
        StartPythonScript();
    }

    public void StartPythonScript()
    {
        // Ensure the system's default Python interpreter is used
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "python",  // Use 'python' directly, assuming it is in the system's PATH
            Arguments = $"\"{Application.dataPath}/../computervision.py\"",  // Enclose in quotes for paths with spaces
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        pythonProcess = new Process();
        pythonProcess.StartInfo = startInfo;
        pythonProcess.OutputDataReceived += (sender, args) =>
        {
            if (!string.IsNullOrEmpty(args.Data))
            {
                UnityEngine.Debug.Log(args.Data); // Log standard output
            }
        };
        pythonProcess.ErrorDataReceived += (sender, args) =>
        {
            if (!string.IsNullOrEmpty(args.Data))
            {
                UnityEngine.Debug.LogError(args.Data); // Log standard error
            }
        };

        try
        {
            pythonProcess.Start();
            pythonProcess.BeginOutputReadLine();
            pythonProcess.BeginErrorReadLine();
            UnityEngine.Debug.Log("Python script started successfully.");
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.LogError($"Failed to start Python script: {ex.Message}"); // Log error on start failure
            return; // Exit the method to avoid null reference errors
        }

        // Optionally, you may want to set an event handler to ensure cleanup on exit
        pythonProcess.Exited += (sender, args) =>
        {
            UnityEngine.Debug.Log("Python script has exited.");
            StopPythonScript(); // Ensure we clean up if the script exits
        };
    }

    public void StopPythonScript()
    {
        if (pythonProcess != null && !pythonProcess.HasExited)
        {
            try
            {
                pythonProcess.Kill();
                pythonProcess.WaitForExit();
                UnityEngine.Debug.Log("Python script stopped successfully.");
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogError($"Failed to stop Python script: {ex.Message}"); // Log error on stop failure
            }
            finally
            {
                pythonProcess.Dispose(); // Dispose process resources
                pythonProcess = null; // Set to null to avoid potential null reference issues
            }
        }
    }

    void OnApplicationQuit()
    {
        StopPythonScript(); // Ensure the Python script is stopped when the application quits
    }
}
