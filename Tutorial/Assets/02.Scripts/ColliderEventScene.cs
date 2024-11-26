using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEventScene : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name + "충돌 들어감");
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision.gameObject.name + "충돌 중");
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log(collision.gameObject.name + "충돌 나감");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + "감지 들어감");
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.name + "감지 중");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.name + "감지 나감");
    }

}
