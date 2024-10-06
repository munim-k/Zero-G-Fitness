using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneOrientationFromPoint : MonoBehaviour {
    [SerializeField] private Transform point;
    [SerializeField] private Vector3 offset;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.position = point.position+offset;
    }
}
