using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSquatScript : MonoBehaviour
{
    public GameObject sun; // Sun prefab to spawn
    [SerializeField] private GameObject head; // Left foot reference
    [SerializeField] private GameObject camera;

    private GameObject newSun;
    public float verticalOffset = 50f;   
    // Right foot reference
    
    // To control the random spawn rate
    private float spawnRate;
    
    public void startGame()
    {
        StartCoroutine(SpawnSun());
    }

    IEnumerator SpawnSun()
    {
        while (true)
        {
            // Calculate the average x position and use Lfoot's y position
            Vector3 spawnPosition = new Vector3(head.transform.position.x, head.transform.position.y, camera.transform.position.z);
            // Instantiate the sun at the calculated position
            newSun = Instantiate(sun, spawnPosition, Quaternion.identity);
            newSun.AddComponent<SunMover>().Initialize(head.transform,verticalOffset);
            
            // Randomize the spawn rate between 1 and 3 seconds
            spawnRate = Random.Range(10.0f, 13.0f);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
