using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEvent : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("충돌 Enter");
    }

    public void OnCollisionStay(Collision collision)
    {
        Debug.Log("충돌 Stay");
    }

    public void OnCollisionExit(Collision collision)
    {
        Debug.Log("충돌 Exit");
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("감지 Enter");
    }

    public void OnTriggerStay(Collider other)
    {
        Debug.Log("감지 Stay");
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("감지 Exit");
    }

}