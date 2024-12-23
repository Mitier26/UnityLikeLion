using UnityEngine;

public abstract class Bird : MonoBehaviour
{
    // 새에게 있는 기능
    // 1. 스킬 사용 하기
    // 2. 속도가 낮거나, 화면 밖으로 가면 사라지는 기능
    
    // 스킬은 개별 기능, 사라지는 것은 공통 기능
    
    public BirdData data;
    
    private Rigidbody2D rigidbody2D;
    private bool isKillUsed;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        if (!isKillUsed)
        {
            isKillUsed = true;
            UseSkill();
        }
    }

    protected abstract void UseSkill();

}
