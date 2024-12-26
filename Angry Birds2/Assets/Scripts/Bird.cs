using System;
using UnityEngine;
public class Bird : MonoBehaviour
{
    // 새에게 있는 기능
    // 1. 스킬 사용 하기
    // 2. 속도가 낮거나, 화면 밖으로 가면 사라지는 기능
    
    // 스킬은 개별 기능, 사라지는 것은 공통 기능
    
    // 몇개의 상태가 필요하다.
    // 등장 상태 : 등장 할 때는 클릭을 할 수 없게 만든다. DOTween 에서 작동
    // 드레그 가능한 상태 : 새를 장착 하고 발사 하기 전 까지 MouseDown일 때 스킬 발동 금지
    // 장착 상태 : 새총에 장착된 상태, 드래그 앤 드롭으로 조전을 해야 한다. 스킬 발동 금지
    // 날아가는 상태 : 다른 조작은 불가능 하고 다시 MouseDown시 스킬을 발동한다.
    // 스킬을 사용한 상태 : 스킬을 사용 했을 경우 다시 작동되는 것을 방지한다.
    // 멈춤 상태 : 속도가 일정 이하가 되면 멈충 상태가 되고 Pool에 반납한다.
    // 사라짐 상태 : SpawnManager에 다음 새를 생성하라는 명령을 한다.
    
    
    public BirdData data;
    
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private Collider2D[] colliders;
    
    public BirdState birdState = BirdState.Appearing;
    private bool isKillUsed;
    public Vector3 startPoint;


    public void InitBirdData(BirdData data)
    {
        this.data = data;
        gameObject.name = data.birdName;
        
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        colliders = GetComponents<Collider2D>();
        
        rigidbody2D.isKinematic = true;
        rigidbody2D.mass = data.weight;
        spriteRenderer.sprite = data.birdSprite;

        if (data.isTriangle)
        {
            EnableCollider<PolygonCollider2D>();
        }
        else
        {
            EnableCollider<CircleCollider2D>();
        }
        
    }

    private void EnableCollider<T>() where T : Collider2D
    {
        foreach (Collider2D collider1 in colliders)
        {
            collider1.enabled = collider1 is T;
            
            if (collider1 is CircleCollider2D circleCollider && typeof(T) == typeof(CircleCollider2D))
            {
                circleCollider.offset = data.offset;
                circleCollider.radius = data.radius;
            }
        }
    }

    private void OnMouseDown()
    {
        // 스킬 사용이 가능한 상태
        if (!isKillUsed && birdState == BirdState.Flying)
        {
            UseSkill(data.skillType);
            isKillUsed = true;
        }
    }

    private void OnMouseDrag()
    {
        if (birdState == BirdState.Dragable)
        {
            // 카메라 범위 내에서 이동 가능
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition; 
        }
    }

    private void OnMouseUp()
    {
        if (birdState == BirdState.Dragable)
        {
            // BirdGrab 의 범위이면 창작
            // 뭔가 꼬인것 같은 느낌이다.
            // 새에서 범위를 판단하는 것이 이상하다.
            // BirdGrab을 찾고 준비되어 있는 새가 있는지 확인해야 했다.
            // 범위에 들어 가는 것을 판단하는 것이 BirdGrab에 있기 때문
            BirdGrab birdGrab = FindObjectOfType<BirdGrab>();
            if (birdGrab != null && birdGrab.readyBird !=null)
            {
                // 장착은 새총에
                birdGrab.AttachBird();
            }
            else
            {
                // 돌아가는 것은 새에
                ReturnPosition();
            }
        }
    }

    private void CollidersSwitch(bool isOn)
    {
        // 모든 Collider를 찾아서 끈다.
        foreach (Collider2D collider1 in colliders)
        {
            collider1.enabled = isOn;
        }
    }

    public void ResetBird(Vector3 spawnPoint)
    {
        transform.position = spawnPoint;
        transform.rotation = Quaternion.identity;
        rigidbody2D.isKinematic = true;
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.angularVelocity = 0f;
        birdState = BirdState.Appearing;
    }

    public void AttachBird(Vector3 attachPoint)
    {
        transform.position = attachPoint;
        birdState = BirdState.Attached;
        CollidersSwitch(false);
    }

    public void LaunchBird(Vector3 launchVelocity)
    {
        birdState = BirdState.Flying;
        rigidbody2D.isKinematic = false;
        rigidbody2D.velocity = launchVelocity;
        CollidersSwitch(true);
    }

    public void ReturnPosition()
    {
        transform.position = startPoint;
        birdState = BirdState.Dragable;
    }

    // 새의 등장이 끝나면 드레그 가능한 상태로 바꾼다.
    public void Dragable()
    {
        startPoint = transform.position;
        birdState = BirdState.Dragable;
    }
    
    
    private void UseSkill(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.Explosive:
                {
                    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, data.explosionRadius);

                    foreach (Collider2D collider in hitColliders)
                    {
                        if (collider.TryGetComponent<Block>(out Block block))
                        {
                            block.TakeDamage(data.explosionDamage);
                            Vector2 direction = collider.transform.position - transform.position;
                            direction.Normalize();
                            block.rigidbody2d.AddForce(direction * 20f, ForceMode2D.Impulse);
                            Instantiate(data.explosionParticle, transform.position, Quaternion.identity);
                        }
                    }
                    break;
                }
            case SkillType.Boost:
                {
                    break;
                }
            case SkillType.Split:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

}
