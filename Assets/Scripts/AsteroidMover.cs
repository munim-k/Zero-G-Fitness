using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMover : MonoBehaviour
{
    private Transform playerTransform; // Player's position
    private float speed; // Speed of the asteroid
    private Vector3 direction;
    private GameObject explosion;
    private GameObject tempexplosion;


    private GameObject leftHand;   // Reference to player's left hand
    private GameObject rightHand;  // Reference to player's right hand

    void Start()
    {
        leftHand= GameObject.FindWithTag("left");
        rightHand= GameObject.FindWithTag("right");
        if(!leftHand || !rightHand)
        {
            Debug.LogError("Hands not found");
        }
    }

    public void Initialize(Transform player, float asteroidSpeed, GameObject _explosion)
    {
        playerTransform = player;
        speed = asteroidSpeed;
        direction = (playerTransform.position - transform.position).normalized;
        explosion = _explosion;
    }

    void Update()
    {
        // Move the asteroid toward the player
        if (playerTransform != null)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    // Detect collision with hands
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that collided is the left or right hand
        if (other.gameObject == leftHand || other.gameObject == rightHand)
        {
            Debug.Log("Asteroid hit by hand!");
            Destroy(gameObject);
            tempexplosion = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(tempexplosion, 1f);
        }
    }
}
