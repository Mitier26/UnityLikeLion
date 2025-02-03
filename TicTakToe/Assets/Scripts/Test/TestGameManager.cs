using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    public void Open()
    {
        PopupPanelController.Instance.Show("X를 눌러 Joy를 표하시오", "확인",true, () =>
        {
            Debug.Log("열림 확인");
        });
    }
}
