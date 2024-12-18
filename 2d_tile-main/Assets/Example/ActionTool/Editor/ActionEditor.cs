using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class ActionNodeBase
{
}

public class AnimationNode : ActionNodeBase
{
    public string name;
    public float startTime;
    public float duration;
}

[CustomEditor(typeof(ActionToolEditor))]
public class ActionToolEditor : EditorWindow
{
    private List<ActionNodeBase> nodes = new List<ActionNodeBase>();
    private ActionNodeBase selectedNode;
    
    public GameObject targetObject;
    private float timeScale = 100f;
    private float maxTime = 10.0f;
    
    private float timelineWidth = 100f;
    private float timelineHeight = 200f;
    private float currentTime = 0f;
    private Vector2 scrollPosition;


    [MenuItem("Window/Action/ActionTool")]
    public static void ShowWindow()
    {
        GetWindow<ActionToolEditor>("ActionTool");
    }

    private void AddAnimationNode(string name, float duration)
    {
        nodes.Add(new AnimationNode() { name = name, startTime = currentTime, duration =  duration});
    }
    
    private void ShowAddNodeMenu()
    {
        AddAnimationNode("empty", 0);
    }

    
    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        
        targetObject = EditorGUILayout.
            ObjectField("Target Object", 
                targetObject, typeof(GameObject),
                true) as GameObject;
        
        timeScale = EditorGUILayout.FloatField("Time Scale", timeScale);
        maxTime = EditorGUILayout.FloatField("Max Time", maxTime);
                
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Timeline", EditorStyles.boldLabel);

        // 스크롤 뷰 시작
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        
        Rect timelineRect = GUILayoutUtility.GetRect(timeScale * maxTime, timelineHeight);
        
        DrawTimelineText(timelineRect);

        timelineRect.y += 20;
        
        GUI.Box(timelineRect, "");

        DrawTimelineGrid(timelineRect);
        DrawCurrentTimeline(timelineRect);

        DrawNodes();

        HandleTimelineInput(timelineRect);

        
        EditorGUILayout.EndScrollView();
        
        EditorGUILayout.EndVertical();

        Repaint();
        
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Animation"))
        {
            ShowAddNodeMenu();
        }
        EditorGUILayout.EndHorizontal();

        if (selectedNode is AnimationNode animationNode)
        {
            animationNode.name = EditorGUILayout.TextField("Name", animationNode.name);
            animationNode.startTime = EditorGUILayout.FloatField("StartTime", animationNode.startTime);
            animationNode.duration = EditorGUILayout.FloatField("Duration", animationNode.duration);
        }
    }

    private void DrawNodes()
    {
        int space = 10;

        foreach (var actionNodeBase in nodes)
        {
            AnimationNode animNode = actionNodeBase as AnimationNode;
            if(animNode != null)
            {
                float x = (animNode.startTime * timeScale);
                if (GUI.Button(new Rect(x, space, animNode.duration * timeScale + 30, 30), animNode.name))
                {
                    selectedNode = animNode;
                }

                if (currentTime >= animNode.startTime && currentTime <= animNode.startTime + animNode.duration)
                {
                    if (targetObject)
                    {
                        if (targetObject.TryGetComponent<Animator>(out var animator))
                        {
                            Debug.Log($"{animNode.name} {currentTime - animNode.startTime} {animNode.duration}");
                            
                            animator.Play(animNode.name
                                , 0, 
                                (currentTime - animNode.startTime) / animNode.duration);
                            animator.Update(0.0f); 
                        }
                    }   
                }
            }

            space += 30;
        }
    }

    void DrawTimelineText(Rect timelineRect)
    {
        float secondWidth = timeScale;
        float totalSeconds = maxTime;

        for (int i = 0; i < totalSeconds; i++)
        {
            float x = timelineRect.x + (secondWidth * i);
            GUI.Label(
                new Rect(x - 15, timelineRect.y, 30, 15),
                i.ToString("F1"),
                new GUIStyle(EditorStyles.miniLabel) { alignment = TextAnchor.MiddleCenter }
            );
        }
    }

    void DrawTimelineGrid(Rect timelineRect)
    {
        float secondWidth = timeScale;
        float totalSeconds = maxTime;

        for (int i = 0; i < totalSeconds; i++)
        {
            float x = timelineRect.x + (secondWidth * i);
            Handles.DrawLine(new Vector3(x, timelineRect.y, 0),
                new Vector3(x, timelineRect.y + timelineRect.height, 0));
        }
    }

    void DrawCurrentTimeline(Rect timelineRect)
    {
        float x = timelineRect.x + (currentTime * timeScale);
        Handles.color = Color.red;
        Handles.DrawLine(new Vector3(x, timelineRect.y), new Vector3(x, timelineRect.y + timelineRect.height));
        Handles.color = Color.white;
    }

    void HandleTimelineInput(Rect timelineRect)
    {
        Event e = Event.current;
        if (timelineRect.Contains(e.mousePosition))
        {
            if (e.type == EventType.MouseDown || e.type == EventType.MouseDrag && e.button == 0)
            {
                float clickPosition = e.mousePosition.x - timelineRect.x;
                currentTime = Mathf.Clamp(clickPosition / timeScale, 0f, timelineRect.width);
                
                e.Use();
                Repaint();
            }
        }
    }
}
