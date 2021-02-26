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
    [SerializeField] private GameObject _streamPrefab = null;
    [SerializeField] private float _streamScale = 1f;
    [SerializeField] private Color _color;
    [SerializeField] private Direction _direction;
    [SerializeField] private string _liquidsTag;
    [SerializeField] private bool _allowPourOut = false;

    [SerializeField] private GameObject _tempLiquidInContainer = null;

    public Color Color
    {
        get
        {
            return _color;
        }
        set
        {
            _color = value;
        }
    }

    private Transform _origin = null;
    private bool _isPouring = false;
    private PourStream _currentStream = null;
    private Collider _objectCollider = null;
    public Collider StreamCollider
    {
        get
        {
            if  (_currentStream.RayHitCollider && _currentStream.RayHitCollider.CompareTag(_liquidsTag))
                return _currentStream.RayHitCollider;
            return null;
        }
    }

    private void Awake()
    {
        _objectCollider = GetComponent<Collider>();
        var liquid = GetComponent<Liquid>();
        if (liquid)
            Color = liquid.Color;
        if (_tempLiquidInContainer)
            _tempLiquidInContainer.GetComponent<MeshRenderer>().material.color = liquid.Color;
    }

    private void Start()
    {
        GameObject obj = new GameObject("Origin");
        obj.AddComponent<MoveObjectToRim>();
        _origin = obj.transform;
        _origin.parent = transform;
    }

    private void Update()
    {
        float angle = CalculatePourAngle(_direction);
        bool pourCheck = angle < _pourThreshold;

        if (angle < -_pourThreshold && _allowPourOut)
        {
            SendMessage("ClearLiquids");
            _currentStream.SetColor(new Color());
        }

        if (_isPouring != pourCheck)
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
        _currentStream.SetColor(_color);
        _currentStream.SetStreamScale(_streamScale);
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
        PourStream pourStream = streamObject.GetComponent<PourStream>();
        return pourStream;
    }
}
