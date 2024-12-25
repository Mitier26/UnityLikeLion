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
    public BirdState birdState = BirdState.Appearing;
    private bool isKillUsed;
    public Vector3 startPoint;

    public void InitBirdData(BirdData data)
    {
        this.data = data;
        gameObject.name = data.birdName;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().mass = data.weight;
        GetComponent<SpriteRenderer>().sprite = data.birdSprite;
    }

    private void OnMouseDown()
    {
        // 스킬 사용이 가능한 상태
        if (!isKillUsed && birdState == BirdState.Flying)
        {
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
            BirdGrab birdGrab = FindObjectOfType<BirdGrab>();
            if (birdGrab != null && birdGrab.readyBird !=null)
            {
                birdGrab.AttachBird();
            }
            else
            {
                ReturnPosition(this);
            }
        }
    }

    public void ReturnPosition(Bird bird)
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

}
