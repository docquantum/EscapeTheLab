using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Draws the line renderer between the origin and the collision entitiy to create a "pouring" effect.
/// </summary>
public class PourStream : MonoBehaviour
{
    private LineRenderer _lineRenderer = null;
    private ParticleSystem _particleSystem = null;
    private Vector3 _targetPos = Vector3.zero;
    private Color _color;
    private float _streamScale = 1f;
    private float _animationSpeed = 3f;

    private int _ignoredCollidersLayerMask = 0;
    private Coroutine _startPourRoutine = null;
    private Coroutine _endPourRoutine = null;
    private bool _beginAnimationEnded = false;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
    }

    public void IgnoreCollider(Collider collider)
    {
        _ignoredCollidersLayerMask = 1 << collider.gameObject.layer;
        _ignoredCollidersLayerMask = ~_ignoredCollidersLayerMask;
    }

    public void SetColor(Color color)
    {
        _color = color;
    }

    public void SetStreamScale(float scale)
    {
        _streamScale = scale;
    }

    /// <summary>
    /// Begins the pouring process
    /// </summary>
    public void Begin()
    {
        _lineRenderer.material.color = _color;
        _lineRenderer.widthMultiplier *= _streamScale;
        var _particleSystemMain = _particleSystem.main;
        _particleSystemMain.startColor = _color;
        _particleSystemMain.startSizeMultiplier *= _streamScale;
        StartCoroutine(UpdateParticle());
        _startPourRoutine = StartCoroutine(BeginPour());
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
            if (HasReachedPosition(1, _targetPos))
                _beginAnimationEnded = true;
            if (_beginAnimationEnded)
                MoveToPosition(1, _targetPos);
            else
                AnimateToPosition(1, _targetPos);
            _particleSystem.transform.position = _targetPos;
            yield return null;
        }
    }

    /// <summary>
    /// A couroutine which handles the animation for ending the pour and
    /// stopping the previous coroutine
    /// </summary>
    /// <returns></returns>
    private IEnumerator EndPour()
    {
        while(!HasReachedPosition(0, _targetPos))
        {
            AnimateToPosition(0, _targetPos);
            AnimateToPosition(1, _targetPos);
            yield return null;
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// Stops the pouring process
    /// </summary>
    public void End()
    {
        StopCoroutine(_startPourRoutine);
        _endPourRoutine = StartCoroutine(EndPour());
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
        Physics.Raycast(ray, out hit, 4f, _ignoredCollidersLayerMask);
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
        _lineRenderer.SetPosition(index, target);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="target"></param>
    private void AnimateToPosition(int index, Vector3 target)
    {
        Vector3 currentPoint = _lineRenderer.GetPosition(index);
        Vector3 newPosition = Vector3.MoveTowards(currentPoint, _targetPos, Time.deltaTime * _animationSpeed);
        _lineRenderer.SetPosition(index, newPosition);
    }

    private bool HasReachedPosition(int index, Vector3 target)
    {
        Vector3 currentPosition = _lineRenderer.GetPosition(index);
        return currentPosition == target;
    }

    private IEnumerator UpdateParticle()
    {
        while (gameObject.activeSelf)
        {
            _particleSystem.gameObject.transform.position = _targetPos;
            _particleSystem.gameObject.SetActive(HasReachedPosition(1, _targetPos));
            yield return null;
        }
    }
}
