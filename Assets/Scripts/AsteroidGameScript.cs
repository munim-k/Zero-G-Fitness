using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGameScript : MonoBehaviour
{
    public GameObject[] asteroids; // Array of different asteroid prefabs
    [SerializeField] private float spawnRate = 1f; // Rate at which asteroids spawn
    [SerializeField] private Transform playerTransform; // Reference to the player's position
    [SerializeField] private float asteroidSpeed = 5f; // Speed at which asteroids fall

    [SerializeField] private GameObject explosion; // Reference to the explosion prefab

    private int randomIndex;
    public void StartGame()
    {
        StartCoroutine(SpawnAsteroids());
    }
   
    IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            randomIndex = Random.Range(0, asteroids.Length);
            GameObject newAsteroid = Instantiate(asteroids[randomIndex], new Vector3(Random.Range(-20, 20), 20, 0), Quaternion.identity);
            
            // Make the asteroid move towards the player
            newAsteroid.AddComponent<AsteroidMover>().Initialize(playerTransform, asteroidSpeed,explosion);

            yield return new WaitForSeconds(spawnRate);
        }
    }
}
