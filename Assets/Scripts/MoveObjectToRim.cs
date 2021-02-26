using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectToRim : MonoBehaviour
{

    [SerializeField] private MeshRenderer _targetMesh = null;
    private Transform _targetTransform = null;

    private void Awake()
    {
        _targetTransform = _targetMesh.transform;
    }

    void Update()
    {
        float x = -_targetTransform.right.y;
        float z = -_targetTransform.forward.y;

        float c = Mathf.Sqrt(x * x + z * z);

        Vector3 newPos = new Vector3(x / c, 2f, z / c);

        transform.localPosition = newPos;
    }
}
