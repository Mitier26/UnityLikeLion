using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScopeChecker : EditorWindow
{
    private string _text;
    
    [MenuItem("Window/Scope Checker")]
    // 어트리뷰트
    // 기능적인 옵션을 추가할 때 사용
    public static void ShowWindow()
    {
        GetWindow<ScopeChecker>("Scope Checker");
    }

    private void OnGUI()
    {
        _text = EditorGUILayout.TextArea( _text, GUILayout.Height(300));
        // ( 화면에 표시되는 이름, 데이터, 레이아웃 옵션 )

        if (GUILayout.Button("Check Scope"))
        {
            if (AreaBracketBalanced(_text))
            {
                EditorUtility.DisplayDialog("Scope Checker", "Scope Check Success", "OK");
            }
            else
            {
                EditorUtility.DisplayDialog("Scope Checker", "Scope Checker Fail", "OK");
            }
        }
    }
    
    public bool AreaBracketBalanced(string expression)
    {
        Stack<char> stack = new Stack<char>();

        foreach (char c in expression)
        {
            if (c == '(' || c == '[' || c == '{')
            {
                stack.Push(c);
            }
            else if (c == ')' || c == ']' || c == '}')
            {
                if (stack.Count == 0)
                    return false;

                char top = stack.Pop();
                if (c == ')' && top != '(' || c == ']' && top != '[' || c == '}' && top != '{')
                {
                    return false;
                }
            }
        }

        return stack.Count == 0;
    }
}
