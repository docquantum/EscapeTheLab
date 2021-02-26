using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquids : MonoBehaviour
{

    public GameObject computerScreen;
    public GameObject notifier;

    private HashSet<Liquid> _liquidsSet = new HashSet<Liquid>();

    public HashSet<Liquid> LiquidsSet => _liquidsSet;

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
            _liquidsSet.UnionWith(other.GetComponent<Liquids>().LiquidsSet);
        }
    }
}
