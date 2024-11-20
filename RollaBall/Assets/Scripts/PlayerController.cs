using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public float speed = 0.0f;

    private float movementX;
    private float movementY;

    private int count;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        winTextObject.SetActive(false);
        SetCountText();
    }

    void SetCountText()
    {
        countText.text = "Count : " + count.ToString();
        if(count >= 14)
        {
            winTextObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
            //Destroy(other.gameObject);
        }
    }
}
