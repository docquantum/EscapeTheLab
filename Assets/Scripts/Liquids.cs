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

    private HashSet<Liquid> _liquidsSet = new HashSet<Liquid>();

    public HashSet<Liquid> LiquidsSet => _liquidsSet;

    private void OnCollisionStream(Collider other)
    {
        executedTime = Time.time;
        if (other.CompareTag(gameObject.tag))
        {
            if (other.GetComponent<Liquids>())
            {
                _liquidsSet.UnionWith(other.GetComponent<Liquids>().LiquidsSet);
            }
            else if (other.GetComponent<Liquid>())
            {
                _liquidsSet.Add(other.GetComponent<Liquid>());
                computerScreen.text = "Liquid: " + other.GetComponent<Liquid>().Name + "\n\n" + "Description: " + other.GetComponent<Liquid>().Description;
                showNotifier = true;
            }
        }
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
