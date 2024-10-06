using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMover : MonoBehaviour
{
    private Transform face;
    public float speed = 0.2f;   
    public float boundaryDistance = 25.0f;
    private Vector3 direction;
    private Material redmaterial;
    private Material originalmaterial;

     private GameObject leftHand;   // Reference to player's left hand
    private GameObject rightHand;
    private int randomIndex;

    private int[] randomIndexArray;

    void Start()
    {
        leftHand= GameObject.FindWithTag("left");
        rightHand= GameObject.FindWithTag("right");
        if(!leftHand || !rightHand)
        {
            Debug.LogError("Hands not found");
        }
        originalmaterial = gameObject.GetComponent<Renderer>().material;
    }
    public void Initialize(Transform _face, Material _material)
    {
        randomIndex = Random.Range(0, 3);
       face = _face;
       direction.z = (face.position.z - transform.position.z);
       redmaterial = _material;
    }

    void Update()
    {
        transform.position += direction * Time.deltaTime * speed;
        if (Vector3.Distance(transform.position, face.position) > boundaryDistance)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "left" || other.gameObject.tag == "right")
        {
            gameObject.GetComponent<Renderer>().material = redmaterial;
        }   
    }

}
