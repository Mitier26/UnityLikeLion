using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    CharController charController;

    void Start()
    {
        charController = GetComponentInParent<CharController>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D");
        charController.Grounded = true;
        charController.GetComponent<Animator>().Play("Idles");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit2D");
        charController.Grounded= false;
    }
}
