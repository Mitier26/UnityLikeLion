using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public struct SigninData
{
    public string username;
    public string password;
}

public struct SigninResult
{
    public int result;
}


public class SigninPanelController : MonoBehaviour
{
    [SerializeField] private TMP_InputField _usernameInputField;
    [SerializeField] private TMP_InputField _passwordInputField;

    public void OnClickSigninButton()
    {
        string username = _usernameInputField.text;
        string password = _passwordInputField.text;
        
        if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            // TODO : 입력값이 비어있음을 알리는 팝업창 표시
            return;
        }
        
        var signinData = new SigninData();
        signinData.username = username;
        signinData.password = password;
        
        StartCoroutine(NetworkManager.Instance.Signin(signinData, () =>
        {
            Destroy(gameObject);
        }, result =>
        {
            if (result == 0)
            {
                _usernameInputField.text = "";
            }
            else if (result == 1)
            {
                _passwordInputField.text = "";
            }
        }));
    }
    
    public void OnClickSignupButton()
    {
        GameManager.Instance.OpenSignupPanel();
    }
}
