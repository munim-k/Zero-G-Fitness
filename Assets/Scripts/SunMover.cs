using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMover : MonoBehaviour
{
    public Transform playerHead;    // Reference to the player's head (target)
    public float speed = 5.0f;      // Movement speed towards the player head
    public float boundaryDistance = 70.0f; // Boundary distance to destroy object when out of bounds
    private Vector3 direction;      // Direction towards the player's head

    public void Initialize(Transform _playerHead,float t)
    {
        Debug.Log(_playerHead.transform.position);
        playerHead = _playerHead;
        direction = (playerHead.position+new Vector3(0,t,0)  - transform.position).normalized;
    }

    void Update()
    {
        
        transform.position += direction * speed * Time.deltaTime;

        // Check if the object is too far out of bounds and destroy it
        if (Vector3.Distance(transform.position, playerHead.position) > boundaryDistance)
        {
            Destroy(gameObject);  // Destroy the object if it's too far from the player's head
        }
    }

    
}
