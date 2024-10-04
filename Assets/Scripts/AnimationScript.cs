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
        client = new TcpClient("localhost", 8080);
        stream = client.GetStream();
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
        // Close the socket when Unity quits
        stream.Close();
        client.Close();
    }
}