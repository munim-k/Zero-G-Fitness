using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public Transform[] bodyJoints; // Assign body joints in the Unity Inspector

    private TcpClient client;
    private NetworkStream stream;

    void Start()
{
    // Check if the client and stream are already initialized
    if (client != null && client.Connected)
    {
        stream.Close();
        client.Close();
    }

    // Proceed with connecting to the server
    try
    {
        client = new TcpClient("localhost", 8080);
        stream = client.GetStream();
        Debug.Log("Connected to Python server.");
    }
    catch (SocketException ex)
    {
        Debug.LogError("SocketException: " + ex.Message);
    }
    catch (Exception ex)
    {
        Debug.LogError("Exception: " + ex.Message);
    }
}


    
    
    void Update()
    {
        if (stream.DataAvailable)
        {
            byte[] data = new byte[1024];
            int bytesRead = stream.Read(data, 0, data.Length);
            string poseData = Encoding.UTF8.GetString(data, 0, bytesRead);

            // Parse the pose data and update Unity's body joints
            string[] poseValues = poseData.Split(',');
            for (int i = 0; i < bodyJoints.Length && i < poseValues.Length / 3; i++)
            {
                float x = float.Parse(poseValues[i * 3]) / 100f;
                float y = float.Parse(poseValues[i * 3 + 1]) / 100f;
                float z = float.Parse(poseValues[i * 3 + 2]) / 100f;

                bodyJoints[i].localPosition = new Vector3(x, y, z);
            }
        }
    }

   void OnApplicationQuit()
{
    // Ensure the stream and client are closed when Unity quits
    if (stream != null)
    {
        stream.Close();
    }

    if (client != null)
    {
        client.Close();
    }
}
}