using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoroutineEx1 : MonoBehaviour
{
    // void Start()
    // {
    //     StartCoroutine(NewCoroutine());
    //     
    //     // StartCoroutine("NewCoroutine"); // 이렇게도 사용 가능, 
    //     // StartCoroutine("NewCoroutine2", 10, "Hello"); // 이렇게 하면 에러가 난다.
    // }
    public Image fadeImage;
    private float percent;
    private float timer;
    
    public float fadeTime;
    public bool isFade = true;
    
    IEnumerator Start()
    {
        while (percent < 1f)
        {
            timer += Time.deltaTime;
            percent = timer / fadeTime;

            if (isFade)
            {
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, percent);
            }
            else
            {
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1- percent);
            }
            
            yield return null;
        }
    }
    
    IEnumerator NewCoroutine()
    {
        yield return null; // 한 프레임 대기

        yield return new WaitForSeconds(0.1f); // 0.1초 대기
        
        yield return new WaitForSeconds(1f); // 1초 대기
    }
    
    
    IEnumerator NewCoroutine2(int number, string name)
    {
        yield return null;
    }
}
