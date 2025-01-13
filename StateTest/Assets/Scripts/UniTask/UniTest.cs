using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;


public class UniTest : MonoBehaviour
{
    public TMP_Text text1;
    public TMP_Text text2;
    public float limitTime = 10f;
    public string[] dialogues;

    private void Start()
    {
        startDialogue();
        QuestTimer();
    }

    private async void startDialogue()
    {
        foreach (string dialogue in dialogues)
        {
            text1.text = dialogue;

            await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        }
        
        text1.text = "대화 종료";
    }

    private async void QuestTimer()
    {
        float settingTime = limitTime;

        while (settingTime > 0)
        {
            text2.text = settingTime.ToString("F1") + "seconds";
            await UniTask.Delay(100);
            settingTime -= 0.1f;
        }
        
        text2.text = "타임 종료";
    }
}
