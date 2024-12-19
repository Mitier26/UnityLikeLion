using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public Vector3 direction;
    public ArrowController arrow;
    
    private Vector3 startPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    public void ResetBall()
    {
        transform.position = startPos;
        direction = Vector3.zero;
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        arrow.gameObject.SetActive(true);
        UIManager.instance.ShowStartText();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !GameManager.instance.isGameStarted)
        {
            direction = arrow.GetDirection().normalized;
            direction.Normalize();
            GameManager.instance.isGameStarted = true;
            arrow.gameObject.SetActive(false);
            UIManager.instance.HideStartText();
        }

        if (GameManager.instance.isGameStarted)
        {
            transform.position += direction * (speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        ICollisionHandler handler = collision.gameObject.GetComponent<ICollisionHandler>();
        if (handler != null)
        {
            handler.HandleCollision(this, collision);
        }
        else
        {
            direction = Vector2.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            direction.Normalize();
        }
    }
}