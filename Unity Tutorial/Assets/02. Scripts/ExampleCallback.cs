using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExampleCallback : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float timer = 5f;
    public static Action bombAction;

    private Animator anim;
    
    private void Awake()
    {
        bombAction += BombEffect;
        bombAction += BombDamageCalculation;    
        bombAction += BombPhysics;
    }

    void Start()
    {
        StartCoroutine(BombRoutine(bombAction));
        
        anim = GetComponent<Animator>();
    }



    IEnumerator BombRoutine(Action bombAction)
    {
        while (timer > 0)
        {
            Debug.Log($"현재 남은 시간 : {timer}초");
            yield return new WaitForSeconds(1f);
            timer--;
        }

        Debug.Log("폭탄이 터졌습니다.");

        bombAction?.Invoke();
    }

    private void BombEffect()
    {
        Debug.Log("포탄이 터질때 나오는 이팩트");
    }

    private void BombDamageCalculation()
    {
        Debug.Log("폭탄 데미지 계산");
    }
    
    private void BombPhysics()
    {
        Debug.Log("폭탄 물리 효과");
    }
}
