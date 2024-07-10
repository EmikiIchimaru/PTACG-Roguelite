using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2D : Singleton<Camera2D>
{
    private enum CameraMode
    {
        Update,
        FixedUpdate,
        LateUpdate
    }    

    
    public Transform Target;
    public Vector2 Offset { get; set; }
	public Vector2 PlayerOffset => offset;

    [Header("Offset")] 
    [SerializeField] private Vector2 offset;
    [SerializeField] private float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    [Header("Mode")] 
	[SerializeField] private CameraMode cameraMode = CameraMode.Update;

    public bool isSequencing;

    protected override void Awake()
    {
        base.Awake();
        Offset = offset;
        isSequencing = false;
    }

    private void Update()
    {
        if (cameraMode == CameraMode.Update)
        {
            FollowTarget();
        }
    }

    private void FixedUpdate()
    {
        if (cameraMode == CameraMode.FixedUpdate)
        {
            FollowTarget();
        }
    }	

    private void LateUpdate()
    {
        if (cameraMode == CameraMode.LateUpdate)
        {
            FollowTarget();
        }
    }

    private void FollowTarget()
    {
        if (Target == null || isSequencing) { return; }
        Vector3 desiredPosition = new Vector3(Target.position.x + Offset.x, Target.position.y + Offset.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        transform.position = smoothedPosition;
    }

    public void StartBossPanSequence(Transform target)
    {
        StartCoroutine(PanCamera(target));
    }

    IEnumerator PanCamera(Transform target)
    {
        isSequencing = true;
        float panDuration = 1f;
        // Move to the target location
        yield return StartCoroutine(MoveToPosition(target.position, panDuration));
        
        // Pause at the target location
        yield return new WaitForSeconds(1f);
        
        // Move back to the original location
        yield return StartCoroutine(MoveToPosition(Target.position, panDuration));
        isSequencing = false;
    }

    IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = new Vector3(targetPosition.x, targetPosition.y, -10f);
    }
}