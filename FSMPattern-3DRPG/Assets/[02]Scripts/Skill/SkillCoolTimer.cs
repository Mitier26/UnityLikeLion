using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCoolTimer : MonoBehaviour
{
    public SkillData skillData;

    private float remainDuration;

    public bool IsReady => 0 >= remainDuration;

    public float RemainDuration => remainDuration;
    
    public void StartCoolTimer()
    {
        CoolTimeProcess();
    }

    async void CoolTimeProcess()
    {
        remainDuration = skillData.skillCoolTime;
        while (true)
        {
            remainDuration -= Time.deltaTime;
            
            await UniTask.Yield();
        }
    }
}
