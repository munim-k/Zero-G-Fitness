using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGameScript : MonoBehaviour
{
    public GameObject[] asteroids;
    [SerializeField] private float spawnRate = 1f;
    
   // [SerializeField] private float spawnRateIncrease = 0.1f;

    private int randomIndex;

    void Start()
    {
        StartCoroutine(SpawnAsteroids());
    }
    
    IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            randomIndex = Random.Range(0, asteroids.Length);
            Instantiate(asteroids[randomIndex], new Vector3(Random.Range(-20, 20), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
           // spawnRate -= spawnRateIncrease;
        }
    }

    


}
