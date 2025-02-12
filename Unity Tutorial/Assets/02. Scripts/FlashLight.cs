using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public GameObject spotLight;

    public bool isFlashLight = false; // ���� ����Ʈ�� ����

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // true -> false / false -> true
            isFlashLight = !isFlashLight; // ���� ������ �ݴ�

            spotLight.SetActive(isFlashLight);
        }
    }
}