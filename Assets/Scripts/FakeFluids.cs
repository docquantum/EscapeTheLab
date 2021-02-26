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

    private void Awake()
    {
        gameObject.AddComponent<PourDetector>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<PourDetector>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
