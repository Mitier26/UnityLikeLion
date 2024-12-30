using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float cameraSpeed = 5f;
    public float minX;
    public float maxX;

    private Vector3 initialCameraPosition;
    private Transform target;
    private bool isFollowing;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        initialCameraPosition = transform.position; // 초기 카메라 위치 저장
    }

    private void Update()
    {
        if (isFollowing && target != null)
        {
            FollowTarget();
        }
        else
        {
            ReturnToInitialPosition();
        }
    }

    private void FollowTarget()
    {
        // 타겟의 x 좌표를 기준으로 카메라 이동
        Vector3 targetPosition = new Vector3(target.position.x, initialCameraPosition.y, initialCameraPosition.z);

        // 타겟이 화면 중앙을 넘어섰는지 확인
        if (Camera.main.WorldToViewportPoint(target.position).x > 0.5f)
        {
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
        }
    }

    private void ReturnToInitialPosition()
    {
        // 초기 위치로 이동
        transform.position = Vector3.Lerp(transform.position, initialCameraPosition, cameraSpeed * Time.deltaTime);

        // 초기 위치와의 거리가 충분히 가까워지면 이동 중지
        if (Vector3.Distance(transform.position, initialCameraPosition) < 0.01f)
        {
            transform.position = initialCameraPosition;
        }
    }

    public void StartFollowing(Transform newTarget)
    {
        target = newTarget;
        isFollowing = true;
    }

    public void StopFollowing()
    {
        isFollowing = false;
        target = null;
    }
}