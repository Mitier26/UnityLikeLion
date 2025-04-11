using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour, IObservable<GameObject>
{
    [Serializable]
    public class WeaponTriggerZone
    {
        public Vector3 position;
        public float radius;
    }
    
    [SerializeField] private WeaponTriggerZone[] _triggerZones;
    
    public int AttackPower => attackPower;
    [SerializeField] private int attackPower;
    [SerializeField] private LayerMask targetLayerMask;
    
    
    private List<IObserver<GameObject>> _observers = new List<IObserver<GameObject>>();
    
    // -----
    // 충돌 처리
    private Vector3[] _previousPositions;
    private HashSet<Collider> _hitColliders;
    
    private Ray _ray = new Ray();
    private RaycastHit[] _hits = new RaycastHit[10];
    private bool _isAttacking = false;

    private void Start()
    {
        _previousPositions = new Vector3[_triggerZones.Length];
        _hitColliders = new HashSet<Collider>();
    }

    public void AttackStart()
    {
        _isAttacking = true;
        _hitColliders.Clear();
        
        for(int i = 0; i < _triggerZones.Length; i++)
        {
            _previousPositions[i] = transform.position + transform.TransformVector(_triggerZones[i].position);
        }
    }

    public void AttackEnd()
    {
        _isAttacking = false;
    }

    private void FixedUpdate()
    {
        if (_isAttacking)
        {
            for(int i = 0; i < _triggerZones.Length; i++)
            {
                var worldPosition = transform.position + transform.TransformVector(_triggerZones[i].position);
                var direction = worldPosition - _previousPositions[i];
                _ray.origin = _previousPositions[i];
                _ray.direction = direction;
                
                // NonAlloc : 충돌을 계산하기 위해 스피어가 만들어지는대, 그 스피어를 사용하고 나서 파괴하지 않고 재사용하는 방식
                // QueryTriggerInteraction.Collide : 트리거를 포함한 모든 충돌체와 충돌을 계산
                var hitCount = Physics.SphereCastNonAlloc(_ray, _triggerZones[i].radius, _hits, direction.magnitude, targetLayerMask, QueryTriggerInteraction.Collide);
                
                for (int j = 0; j < hitCount; j++)
                {
                    var hit = _hits[j];

                    if (!_hitColliders.Contains(hit.collider))
                    {
                        Time.timeScale = 0f;
                        StartCoroutine(ResumeTimeScale());
                        
                        _hitColliders.Add(hit.collider);
                        Notify(hit.collider.gameObject);
                    }
                }
                _previousPositions[i] = worldPosition;
                
            }
        }   
    }

    private IEnumerator ResumeTimeScale()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1f;
    }

    public void Subscribe(IObserver<GameObject> observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    public void Unsubscribe(IObserver<GameObject> observer)
    {
        if (_observers.Contains(observer))
        {
            _observers.Remove(observer);
        }
    }

    public void Notify(GameObject value)
    {
        foreach (IObserver<GameObject> observer in _observers)
        {
            observer.OnNext(value);
        }
    }

    private void OnDestroy()
    {
        var copyObservers = new List<IObserver<GameObject>>(_observers);
        
        foreach (IObserver<GameObject> observer in copyObservers)
        {
            observer.OnCompleted();
        }
        _observers.Clear();
    }
    
    #if UNITY_EDITOR
    
    private void OnDrawGizmos()
    {
        if (_isAttacking)
        {
            for (int i = 0; i < _triggerZones.Length; i++)
            {
                var worldPosition = transform.position + transform.TransformVector(_triggerZones[i].position);
                var direction = worldPosition - _previousPositions[i];
                
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(worldPosition, _triggerZones[i].radius);
                
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(worldPosition + direction, _triggerZones[i].radius);
            }
        }
        else
        {
            Gizmos.color = Color.green;
            foreach (var triggerZone in _triggerZones)
            {
                Gizmos.DrawSphere(triggerZone.position, triggerZone.radius);
            }
        }
    }
    
    
    #endif
}
