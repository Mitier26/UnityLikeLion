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
        charController.Grounded++;
        charController.GetComponent<Animator>().Play("Idles");
        Debug.Log("OnTriggerEnter2D" + charController.Grounded);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        charController.Grounded--;
        Debug.Log("OnTriggerExit2D" + charController.Grounded);
    }
}
