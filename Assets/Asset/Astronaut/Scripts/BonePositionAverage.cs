using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonePositionAverage : MonoBehaviour {
    [System.Serializable]
    class WeightedPosition {

        public Transform transform;
        public float weight = 1f;
    }

    [SerializeField] private WeightedPosition[] positionTransforms;
    [SerializeField] private WeightedPosition[] rotationTransforms;
    Vector3 position = Vector3.zero;
    Vector3 rotation = Vector3.zero;

    // Update is called once per frame
    void Update() {
        //Average the position of the position transforms
        if (positionTransforms.Length > 0) {
            float totalWeight = 0f;
            for (int i = 0; i < positionTransforms.Length; i++) {
                position += positionTransforms[i].transform.position * positionTransforms[i].weight;
                totalWeight += positionTransforms[i].weight;
            }
            if (totalWeight > 0f)
                position /= totalWeight;
            transform.position = position;
        }

        if (rotationTransforms.Length > 0) {
            float totalWeight = 0f;
            for (int i = 0; i < rotationTransforms.Length; i++) {
                rotation += rotationTransforms[i].transform.position * rotationTransforms[i].weight;
                totalWeight += positionTransforms[i].weight;

            }
            if (totalWeight > 0f)
                rotation /= rotationTransforms.Length;
            transform.LookAt(rotation, transform.forward);
        }

    }


}
