using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchScript : MonoBehaviour
{
    public GameObject cube;
    public GameObject face;
    public Material redMaterial;
    public float delay = 2f;

    public GameObject _camera;
    private int randomIndex;

    public float[] randomIndexArray;

    public float offset = 0.5f;
    public void StartGame()
    {
        StartCoroutine(SpawnCube());
    }

    
    IEnumerator SpawnCube()
    {
        while (true)
        {
            randomIndex = Random.Range(0, randomIndexArray.Length);
            Vector3 spawnPosition = new Vector3(face.transform.position.x-offset, face.transform.position.y+ randomIndexArray[randomIndex], _camera.transform.position.z );
            GameObject newCube = Instantiate(cube, spawnPosition, Quaternion.identity);
            newCube.AddComponent<CubeMover>().Initialize(face.transform,redMaterial);
            spawnPosition = new Vector3(face.transform.position.x+offset, face.transform.position.y+ randomIndexArray[randomIndex], _camera.transform.position.z );
            newCube = Instantiate(cube, spawnPosition, Quaternion.identity);
            newCube.AddComponent<CubeMover>().Initialize(face.transform,redMaterial);
            yield return new WaitForSeconds(delay);
        }
    }
}
