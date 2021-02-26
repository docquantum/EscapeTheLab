using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquids : MonoBehaviour
{

    public GameObject computerScreen;
    public GameObject notifier;

    private PourDetector _pourDetector;

    private HashSet<Liquid> _liquidsSet = new HashSet<Liquid>();

    public HashSet<Liquid> LiquidsSet => _liquidsSet;

    private int _liquidCount = 0;
    private Color _mixedColor;

    private void Awake()
    {
        _pourDetector = GetComponent<PourDetector>();
    }

    private void UpdateColor()
    {
        _liquidCount = _liquidsSet.Count;
        if (_liquidCount == 1)
        {
            Liquid[] arr = new Liquid[_liquidCount];
            _liquidsSet.CopyTo(arr);
            _mixedColor = arr[0].Color;
            _pourDetector.Color = _mixedColor;
            return;
        }
        foreach (var liquid in _liquidsSet)
        {
            _mixedColor += liquid.Color;
        }
        _pourDetector.Color = _mixedColor;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    // if collision with liquid and another liquid within dict liquids:
    //    Liquid collidingLiquid = null; // get from collision
    //    onCollision(liquid, collidingLiquid);
    //}

    private void OnCollisionStream(Collider other)
    {
        if (other.CompareTag(gameObject.tag))
        {
            if (other.GetComponent<Liquids>())
            {
                _liquidsSet.UnionWith(other.GetComponent<Liquids>().LiquidsSet);
                if (_liquidCount != _liquidsSet.Count)
                    UpdateColor();
            }
            else if (other.GetComponent<Liquid>())
            {
                _liquidsSet.Add(other.GetComponent<Liquid>());
                if (_liquidCount != _liquidsSet.Count)
                    UpdateColor();
            }
        }
    }
}
