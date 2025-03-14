using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public struct SignupData
{
    public string username;
    public string password;
}

public class SignupPanelController : MonoBehaviour
{
    [SerializeField] private TMP_InputField _usernameInputField;
    [SerializeField] private TMP_InputField _passwordInputField;
    [SerializeField] private TMP_InputField _ConfirmPasswordInputField;
    
    public void OnClickConfirmButton()
    {
        var username = _usernameInputField.text;
        var password = _passwordInputField.text;
        var passwordConfirm = _ConfirmPasswordInputField.text;
        
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(passwordConfirm))
        {
            // GameManager.Instance.OpenConfirmPanel("입력값이 비어있습니다.", () =>
            // {
            //     _usernameInputField.text = "";
            //     _nicknameInputField.text = "";
            //     _passwordInputField.text = "";
            //     _ConfirmPasswordInputField.text = "";
            // });
            return;
        }

        if (password.Equals(passwordConfirm))
        {
            SignupData signupData = new SignupData();
            signupData.username = username;
            signupData.password = password;

            StartCoroutine(NetworkManager.Instance.Signup(signupData, () =>
            {
                Destroy(gameObject);
            }, () =>
            {
                _usernameInputField.text = "";
                _passwordInputField.text = "";
                _ConfirmPasswordInputField.text = "";
            }));
        }
        else
        {
            // GameManager.Instance.OpenConfirmPanel("비밀번호가 일치하지 않습니다.", () =>
            // {
            //     _passwordInputField.text = "";
            //     _ConfirmPasswordInputField.text = "";
            // });
        }
    }

    
    
    public void OnClickCancelButton()
    {
        
    }
}
