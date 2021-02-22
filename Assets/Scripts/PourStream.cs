using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Draws the line renderer between the origin and the collision entitiy to create a "pouring" effect.
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class PourStream : MonoBehaviour
{
    private LineRenderer _lineRenderer = null;
    [SerializeField] private Vector3 _targetPos = Vector3.zero;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        
    }

    /// <summary>
    /// Begins the pouring process
    /// </summary>
    public void Begin()
    {

    }

    /// <summary>
    /// A coroutine which handles the raycasting and pouring animations.
    /// </summary>
    /// <returns></returns>
    private IEnumerator BeginPour()
    {
        yield return null;
    }

    /// <summary>
    /// Finds the collision point of the fake fluid with the objects below.
    /// This is where the line will stop and particles will be spawned.
    /// </summary>
    /// <returns>
    /// Vector3: World space position of collision
    /// </returns>
    private Vector3 FindEndPoint()
    {
        RaycastHit hit;
        if(Physics.Raycast(_targetPos, -transform.up, out hit))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        return Vector3.zero;
    }

    /// <summary>
    /// Moves the splashing animation to the endpoint of the line
    /// </summary>
    /// <param name="index"></param>
    /// <param name="target"></param>
    private void MoveToPosition(int index, Vector3 target)
    {

    }

}
