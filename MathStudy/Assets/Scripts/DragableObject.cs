using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour, 
    IPointerDownHandler, 
    IPointerUpHandler, 
    IDragHandler
{ 
    private Vector3 startPosition;      // 라인의 시작 위치
    private Vector3 pullPosition;       // 마우스로 당기는 위치
    public Vector3 direction;           // 발사 방향
    public Vector3 initialPosition;     // 자신의 초기 위치 저장용
    public Vector3 initialLinePosition;

    private float repositionTime = 0.5f;
    public float maxForce = 100f;
    public GameObject ballPrefab;
    
    private Camera MainCamera;
 
    public LineRenderer lineRenderer;
    [SerializeField] private float maxPullDistance;
    
    public Action OnFly;
    public Action OnDestroyed;

    private void Awake()
    {
        MainCamera = Camera.main;
        
        initialPosition = transform.position;   // 초기 위치를 저장
        initialLinePosition = lineRenderer.GetPosition(1);
    }

    private void OnDestroy()
    {
        if (!OnDestroyed.IsUnityNull())
            OnDestroyed?.Invoke();
    }


    // 마우스를 클릭했을 때 작동
    public void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log("OnPointerDown");
        // 마우스를 클릭했을 때 위치를 저장
        pullPosition = startPosition = MainCamera.ScreenToWorldPoint(
            new Vector3(eventData.position.x, 
                eventData.position.y , 
                MainCamera.WorldToScreenPoint(transform.position).z));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StartCoroutine(Reposition());
    }

    // 마우스를 드레그 할 때
    public void OnDrag(PointerEventData eventData)
    {
        if (Camera.main != null)
        {   
            // 마우스의 위치로 오브젝트를 이동하기
            Vector3 mouseWorldPos= pullPosition = MainCamera.ScreenToWorldPoint(
                new Vector3(eventData.position.x, 
                eventData.position.y , 
                MainCamera.WorldToScreenPoint(transform.position).z));
            
            //mouseWorldPos.z = transform.position.z;
            // Debug.Log(mouseWorldPos);
            
            direction = startPosition - mouseWorldPos;
            
            // 마우스를 이동하는 방향, 
            Vector3 pullDirection = startPosition - mouseWorldPos;
            // Debug.Log("Pull"+pullDirection);
            
            // 라인 렌더러의 시작점
            Vector3 LinePosition1 = Vector3.zero;
            
            // magnitude는 벡터의 길이, 길이가 최대길이 보다 길어 지면
            if (pullDirection.magnitude > maxPullDistance)
            {
                // 당기는 방향의 최대 값 설정
                pullDirection = pullDirection.normalized * maxPullDistance;
                LinePosition1 = pullPosition = startPosition - pullDirection;
            }
            else
            {
                LinePosition1 = mouseWorldPos;
            }
            
            transform.position = mouseWorldPos;
            
            lineRenderer.SetPosition(1, LinePosition1);
        }
    }

    IEnumerator Reposition()
    {
        float elapsedTime = 0;      // 경과시간
        
        // 초기위치로 돌아가는 시간 동안 작동
        while (elapsedTime < repositionTime)
        {
            elapsedTime += Time.deltaTime;      // 경과 시간 중첩
            Vector3 newPosition = Vector3.Lerp(pullPosition, initialLinePosition, elapsedTime / repositionTime);
            lineRenderer.SetPosition(1, newPosition);
            yield return null;
        }
        
        lineRenderer.SetPosition(1, initialLinePosition);
        transform.position = initialPosition;

        LaunchProjectile();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void LaunchProjectile()
    {
        float pullStrength = Mathf.Clamp(direction.magnitude, 0f, maxPullDistance) / maxPullDistance;
        float launchForce = pullStrength * maxForce;
        
        GameObject projectile = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().AddForce(direction.normalized * launchForce, ForceMode.Impulse);
    }
}
