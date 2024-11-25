using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStudy : MonoBehaviour
{
    public enum MOVETYPE { WorldMove, Translate, MoveTowards, SmoothDamp, Lerp, LinearLerp}
    public MOVETYPE moveType;

    public Transform startTransform;
    public Transform endTrsnsform;

    public float speed = 3f;
    private bool isMove = true;
    private Vector3 currentVel;

    public float timer = 0f;
    public float percent = 0f;
    public float targetTime = 3f;

    private void Start()
    {
        transform.position = startTransform.position;
    }

    private void Update()
    {
        if (!isMove) return;

        switch (moveType)
        {
            case MOVETYPE.WorldMove:
                transform.position = transform.position + new Vector3(0f, 0f, speed * Time.deltaTime);
                break;
            case MOVETYPE.Translate:
                transform.Translate(0f, 0f, speed * Time.deltaTime);
                break;
            case MOVETYPE.MoveTowards:
                transform.position = Vector3.MoveTowards(transform.position, endTrsnsform.position, speed * Time.deltaTime);
                break;
            case MOVETYPE.SmoothDamp:
                transform.position = Vector3.SmoothDamp(transform.position, endTrsnsform.position, ref currentVel, speed);
                break;
            case MOVETYPE.Lerp:
                transform.position = Vector3.Lerp(transform.position, endTrsnsform.position, Time.deltaTime);
                break;
            case MOVETYPE.LinearLerp:
                timer += Time.deltaTime;
                percent = Mathf.Clamp01(timer / targetTime);
                transform.position = Vector3.Lerp(startTransform.position, endTrsnsform.position, percent);
                break;
        }

        if(Vector3.Distance(transform.position, endTrsnsform.position) <= 0.5f)
        {
            isMove = false;
        }
    }
}
