using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Liquids : MonoBehaviour
{

    public Text computerScreen;
    public GameObject notifier;
    private bool showNotifier = false;
    private float currentTime = 0.0f, executedTime = 0.0f, timeToWait = 5.0f;

    private PourDetector _pourDetector;

    private HashSet<Liquid> _liquidsSet = new HashSet<Liquid>();

    [SerializeField] private GameObject _LiquidInContainer;

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
            _LiquidInContainer.GetComponent<MeshRenderer>().material.color = _mixedColor;
            return;
        }
        foreach (var liquid in _liquidsSet)
        {
            _mixedColor += liquid.Color;
        }
        _pourDetector.Color = _mixedColor;
        _LiquidInContainer.GetComponent<MeshRenderer>().material.color = _mixedColor;
    }

    private void OnCollisionStream(Collider other)
    {
        executedTime = Time.time;
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
                computerScreen.text = "Liquid: " + other.GetComponent<Liquid>().Name + "\n\n" + "Description: " + other.GetComponent<Liquid>().Description;
                showNotifier = true;
            }
        }
    }

    private void ClearLiquids()
    {
        _liquidsSet.Clear();
        _liquidCount = 0;
        _mixedColor = new Color();
        _pourDetector.Color = _mixedColor;
        _LiquidInContainer.GetComponent<MeshRenderer>().material.color = _mixedColor;
    }

    void Update()
    {
        currentTime = Time.time;
        if (showNotifier)
            notifier.SetActive(true);
        else
            notifier.SetActive(false);

        if (executedTime != 0.0f)
        {
            if (currentTime - executedTime > timeToWait)
            {
                executedTime = 0.0f;
                showNotifier = false;
            }
        }
    }
}
