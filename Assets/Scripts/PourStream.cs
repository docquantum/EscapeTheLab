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
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
    }

    /// <summary>
    /// Begins the pouring process
    /// </summary>
    public void Begin()
    {
        StartCoroutine(BeginPour());
    }

    /// <summary>
    /// A coroutine which handles the raycasting and pouring animations.
    /// </summary>
    /// <returns></returns>
    private IEnumerator BeginPour()
    {
        while(gameObject.activeSelf)
        {
            _targetPos = FindEndPoint();
            MoveToPosition(0, transform.position);
            MoveToPosition(1, _targetPos);
            yield return null;
        }
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
        Ray ray = new Ray(transform.position, Vector3.down);
        Physics.Raycast(ray, out hit, 2f);
        Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(2f);
        return endPoint;
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
