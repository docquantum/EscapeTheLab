using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeFluids : MonoBehaviour
{
    private enum Direction
    {
        green,
        red,
        blue
    };

    [SerializeField] private Color _color;
    [SerializeField] private Direction _direction;

    // DEBUG VARS
    private GameObject d_point;
    private Material d_mat;

    // Start is called before the first frame update
    void Start()
    {
        d_point = GameObject.CreatePrimitive(PrimitiveType.Cube);
        d_point.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        d_point.transform.parent = transform;
        d_point.transform.localPosition = Vector3.zero + transform.up;
        d_mat = d_point.GetComponent<MeshRenderer>().material;
        d_mat.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        float up = GetUpValue(_direction);
        if (up > 0.7f)
            d_mat.color = Color.red;
        else if (up > 0.1f)
            d_mat.color = Color.yellow;
        else
            d_mat.color = Color.green;
    }

    private float GetUpValue(Direction direction)
    {
        float val = 0f;
        if (direction == Direction.red)
            val = transform.right.x;
        else if (direction == Direction.blue)
            val = transform.forward.z;
        else if (direction == Direction.green)
            val = transform.up.y;

        return val;
    }
}
