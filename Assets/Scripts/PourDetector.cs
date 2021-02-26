using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    private enum Direction
    {
        green,
        red,
        blue
    };
    [SerializeField] private int _pourThreshold = 45;
    [SerializeField] private Transform _origin = null;
    [SerializeField] private GameObject _streamPrefab = null;
    [SerializeField] private Color _color;
    [SerializeField] private Direction _direction;

    private bool _isPouring = false;
    private PourStream _currentStream = null;
    private Collider _objectCollider = null;

    private void Awake()
    {
        _objectCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        bool pourCheck = CalculatePourAngle(_direction) < _pourThreshold;

        if(_isPouring != pourCheck)
        {
            _isPouring = pourCheck;

            if (_isPouring)
                StartPour();
            else
                EndPour();
        }
    }

    private void StartPour()
    {
        _currentStream = CreateStream();
        _currentStream.IgnoreCollider(_objectCollider);
        _currentStream.Begin();
    }

    private void EndPour()
    {
        _currentStream.End();
        _currentStream = null;
    }

    private float CalculatePourAngle(Direction direction)
    {
        float val = 0f;
        if (direction == Direction.red)
            val = transform.right.y;
        else if (direction == Direction.blue)
            val = transform.forward.y;
        else if (direction == Direction.green)
            val = transform.up.y;
        return val * Mathf.Rad2Deg;
    }

    private PourStream CreateStream()
    {
        GameObject streamObject = Instantiate(_streamPrefab, _origin.position, Quaternion.identity, _origin);
        return streamObject.GetComponent<PourStream>();
    }
}
