using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyController))]

public class EnemyControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 기본 인스펙터를 그리기
        base.OnInspectorGUI();
        
        // 타켓 컴포넌트 참조 가져오기
        EnemyController enemyController = (EnemyController)target;
        
        // 여백 추가
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("상태 디버그 정보", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
    
        // 상태별 색상 지정
        switch (enemyController.CurrentState)
        {
            case EnemyState.Idle:
                GUI.backgroundColor = new Color(1, 1, 1, 0.8f);
                break;
            case EnemyState.Patrol:
                GUI.backgroundColor = new Color(0, 0, 1, 0.8f);
                break;
            case EnemyState.Trace:
                GUI.backgroundColor = new Color(0, 1, 0, 0.8f);
                break;
            case EnemyState.Attack:
                GUI.backgroundColor = new Color(1, 0, 1, 0.8f);
                break;
            case EnemyState.Hit:
                GUI.backgroundColor = new Color(1, 1, 0, 0.8f);
                break;
            case EnemyState.Dead:
                GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.8f);
                break;
        }
        
        
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.LabelField("현재 상태: ", enemyController.CurrentState.ToString(), EditorStyles.boldLabel);
        EditorGUILayout.EndVertical();
        
        EditorGUILayout.EndVertical();
        
        
        // 강제로 상태 변화 버튼
        EditorGUILayout.BeginHorizontal();
        
        if(GUILayout.Button("Idle"))
        {
            enemyController.SetState(EnemyState.Idle);
        }
        if(GUILayout.Button("Patrol"))
        {
            enemyController.SetState(EnemyState.Patrol);
        }
        if(GUILayout.Button("Trace"))
        {
            enemyController.SetState(EnemyState.Trace);
        }
        if(GUILayout.Button("Attack"))
        {
            enemyController.SetState(EnemyState.Attack);
        }
        if(GUILayout.Button("Hit"))
        {
            enemyController.SetState(EnemyState.Hit);
        }
        if(GUILayout.Button("Dead"))
        {
            enemyController.SetState(EnemyState.Dead);
        }
        
        EditorGUILayout.EndHorizontal();
    }
    
    private void OnEnable()
    {
        EditorApplication.update += OnEditorUpdate;
    }
    
    private void OnDisable()
    {
        EditorApplication.update -= OnEditorUpdate;
    }
    
    private void OnEditorUpdate()
    {
        if(target != null) Repaint();
    }
}
