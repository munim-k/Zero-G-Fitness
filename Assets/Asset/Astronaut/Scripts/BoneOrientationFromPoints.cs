using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneOrientationFromPoints : MonoBehaviour {

    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    Vector3 rotationVector = Vector3.zero;


    // Update is called once per frame
    void Update() {
        transform.position = startPoint.position;
        transform.LookAt(endPoint, transform.up);

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + rotationVector * 2);
    }
}
