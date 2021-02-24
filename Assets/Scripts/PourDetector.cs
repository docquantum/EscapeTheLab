using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    [SerializeField] private int _pourThreshold = 45;
    [SerializeField] private Transform _origin = null;
    [SerializeField] private GameObject _streamPrefab = null;

    private bool _isPouring = false;
    private PourStream currentStream = null;

    private void Update()
    {
        bool pourCheck = CalculatePourAngle() < _pourThreshold;

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
        print("Start");
        currentStream = CreateStream();
        currentStream.Begin();
    }

    private void EndPour()
    {
        print("end");
    }

    private float CalculatePourAngle()
    {
        return transform.up.y * Mathf.Rad2Deg;
    }

    private PourStream CreateStream()
    {
        GameObject streamObject = Instantiate(_streamPrefab, _origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<PourStream>();
    }
}
