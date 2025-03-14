using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private Canvas _canvas;
    
    [SerializeField] private GameObject signinPanel;
    [SerializeField] private GameObject signupPanel;
    [SerializeField] private GameObject confirmPanel;

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        // 씬이 로드되면 캔버스를 찾아서 할당
        _canvas = FindObjectOfType<Canvas>();   // Singleton awake에서 실행
    }
    
    private void Start()
    {
        // 로그인
        // OpenSigninPanel();
    }
    
    public void ChangeToGameScene()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void OpenGiboPanel()
    {
        if(_canvas != null)
        {
            // 기보창을 생성
        }
    }
    
    public void OpenRankPanel()
    {
        if(_canvas != null)
        {
            // 랭킹창을 생성
        }
    }
    
    public void OpenShopPanel()
    {
        if(_canvas != null)
        {
            // 상점창을 생성
        }
    }
    
    public void OpenSettingsPanel()
    {
        if (_canvas != null)
        {
            // 설정창을 생성
        }
    }
    
    public void TestPanel()
    {
        OpenConfirmPanel("hello", () =>
        {
            Debug.Log("TestPanel");
        });
    }
    
    public void OpenConfirmPanel(string message, ConfirmPanelController.OnConfirmButtonClick onConfirmButtonClick)
    {
        if (_canvas != null)
        {
            var confirmPanelObject = Instantiate(confirmPanel, _canvas.transform);
            confirmPanelObject.GetComponent<ConfirmPanelController>()
                .Show(message, onConfirmButtonClick);
        }
    }
    
    public void OpenSigninPanel()
    {
        if (_canvas != null)
        {
            var signinPanelObject = Instantiate(signinPanel, _canvas.transform);
        }
    }
    
    public void OpenSignupPanel()
    {
        if (_canvas != null)
        {
            var signupPanelObject = Instantiate(signupPanel, _canvas.transform);
        }
    }
}
