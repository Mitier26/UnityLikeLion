using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piace : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
