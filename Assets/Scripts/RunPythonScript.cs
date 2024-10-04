using System.Diagnostics;
using UnityEngine;

public class RunPythonScript : MonoBehaviour
{
    private Process process; // Store the process reference

    void Start()
    {
        var runningProcesses = Process.GetProcessesByName("python"); // Change to match your Python executable name
        foreach (var runningProcess in runningProcesses)
        {
            runningProcess.Kill(); // Kill any existing Python processes
        }


        // Command to execute
        string command = "python computervision.py"; // Replace with your command

        ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/C " + command)
        {
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            WorkingDirectory = @"C:\Zero-G-Fitness" // Set the working directory
        };

        process = Process.Start(processInfo); // Start the process and store the reference

        // Optionally, you can read the output from the command
        string output = process.StandardOutput.ReadToEnd();
        UnityEngine.Debug.Log(output); // Log the output if needed
        process.WaitForExit(); // Optional: Wait for the command to finish
    }

    void OnDestroy()
{
    // Ensure the process is killed when this script is destroyed
    if (process != null)
    {
        if (!process.HasExited)
        {
            process.Kill();
        }
        process.Dispose(); // Dispose of the process resources
    }
}

}
