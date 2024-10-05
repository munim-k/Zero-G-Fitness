using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour
{
    public Transform[] bodyJoints; // Assign body joints in the Unity Inspector
    private TcpClient client;
    private NetworkStream stream;
    private byte[] buffer = new byte[1024];

    void Start()
    {
        StartCoroutine(ConnectToPythonServerWithDelay());
    }

    private IEnumerator ConnectToPythonServerWithDelay()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds before connecting
        ConnectToPythonServer();
    }

    private void ConnectToPythonServer()
    {
        try
        {
            client = new TcpClient("localhost", 8080); // Match the port with the Python server
            stream = client.GetStream();
            UnityEngine.Debug.Log("Connected to Python server.");
        }
        catch (SocketException se)
        {
            UnityEngine.Debug.LogError("SocketException: " + se.Message);
            RetryConnection(); // Attempt to retry the connection
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("Connection error: " + e.Message);
        }
    }

    private void RetryConnection()
    {
        for (int i = 0; i < 5; i++)
        {
            UnityEngine.Debug.Log("Retrying connection...");
            System.Threading.Thread.Sleep(1000); // Wait before retrying
            try
            {
                client = new TcpClient("localhost", 8080);
                stream = client.GetStream();
                UnityEngine.Debug.Log("Reconnected to Python server.");
                return; // Exit retry loop on success
            }
            catch (SocketException se)
            {
                UnityEngine.Debug.LogError("Retry SocketException: " + se.Message);
            }
        }
        UnityEngine.Debug.LogError("Failed to reconnect after multiple attempts.");
    }

    void Update()
    {
        if (client != null && stream.DataAvailable)
        {
            Debug.Log("Entering update loop:");
            try
            {
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string poseData = Encoding.UTF8.GetString(buffer, 0, bytesRead).TrimEnd('\0');

                // Parse the pose data and update Unity's body joints
                string[] poseValues = poseData.Split(',');
                int jointCount = poseValues.Length / 3; // Calculate how many joints we can update
                for (int i = 0; i < bodyJoints.Length && i < jointCount; i++)
                {
                    if (float.TryParse(poseValues[i * 3], out float x) &&
                        float.TryParse(poseValues[i * 3 + 1], out float y) &&
                        float.TryParse(poseValues[i * 3 + 2], out float z))
                    {
                        bodyJoints[i].localPosition = new Vector3(x / 100f, y / 100f, z / 100f);
                    }
                }
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError("Error reading from stream: " + ex.Message);
            }
        }
    }

    void OnApplicationQuit()
    {
        if (stream != null) stream.Close();
        if (client != null) client.Close();
        UnityEngine.Debug.Log("Connection closed.");
    }
}
