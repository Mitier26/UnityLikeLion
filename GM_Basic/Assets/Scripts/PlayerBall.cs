using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    public float jumpPower = 10f;
    private bool isJumping = false;
    private Rigidbody rb;
    public int itemCount;
    private AudioSource audio;
    public GameManager gameManager;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        
        rb.AddForce(new Vector3(x, 0, y), ForceMode.Impulse);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            itemCount++;
            audio.Play();
            gameManager.GetItem(itemCount);
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Finish"))
        {
            if (gameManager.totalItemCount == itemCount)
            {
                if (gameManager.stage == 1)
                {
                    SceneManager.LoadScene(0);    
                }
                SceneManager.LoadScene(gameManager.stage + 1);
            }
            else
            {
                SceneManager.LoadScene(gameManager.stage);
            }
        }
    }
}
