using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectToRim : MonoBehaviour
{
    private Transform _targetTransform = null;
    private float _top = 0f;

    private void Start()
    {
        _targetTransform = transform.parent;
        transform.localPosition = _targetTransform.localPosition;
        var objectBounds = GetComponentInParent<MeshFilter>().mesh.bounds;
        _top = objectBounds.center.y + objectBounds.extents.y;
    }

    void FixedUpdate()
    {
        float x = -_targetTransform.right.y;
        float z = -_targetTransform.forward.y;

        float c = Mathf.Sqrt(x * x + z * z);
        if (c == 0f)
            return;

        Vector3 newPos = new Vector3(x / c, _top, z / c);

        transform.localPosition = newPos;
    }
}
