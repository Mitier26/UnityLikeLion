using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LottoCreator : MonoBehaviour
{
    public int[] numbers = new int[45]; // 45자리의 숫자 배열

    public LottoBall[] lottoBalls;
    
    private int shakeCount = 1000;

    void Start()
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = i + 1;
        }

        // OnCreateLotto();
    }

    public void OnCreateLotto()
    {
        for (int i = 0; i < shakeCount; i++)
        {
            int ranInt1 = Random.Range(0, numbers.Length);
            int ranInt2 = Random.Range(0, numbers.Length);
            // int temp = 0;

            var temp = numbers[ranInt1];
            numbers[ranInt1] = numbers[ranInt2];
            numbers[ranInt2] = temp;
        }

        int[] sortArray = new int[7];
        for (int i = 0; i < 7; i++)
        {
            sortArray[i] = numbers[i];
        }
        
        Array.Sort(sortArray);

        for (int i = 0; i < lottoBalls.Length; i++)
        {
            lottoBalls[i].textNumber.text = sortArray[i].ToString();
        }

        StartCoroutine(ShowBall());
    }

    IEnumerator ShowBall()
    {
        foreach (var ball in lottoBalls)
        {
            ball.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
}