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
    private float d_rad;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(_direction);
        d_point = GameObject.CreatePrimitive(PrimitiveType.Cube);
        d_point.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        d_point.transform.parent = transform;
        d_point.transform.localPosition = Vector3.zero + GetUpVector(_direction)*0.5f;
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

    private Vector3 GetUpVector(Direction direction)
    {
        Vector3 val = Vector3.zero;
        if (direction == Direction.red)
            val = transform.right;
        else if (direction == Direction.blue)
            val = transform.forward;
        else if (direction == Direction.green)
            val = transform.up;
        return val;
    }
    private float GetUpValue(Direction direction)
    {
        float val = 0f;
        if (direction == Direction.red)
            val = transform.right.y;
        else if (direction == Direction.blue)
            val = transform.forward.y;
        else if (direction == Direction.green)
            val = transform.up.y;
        return val;
    }
}
