using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float boostSpeed = 8f;
    public float glideSpeed = 2f;
    public float waveBoost = 2f;

    private Rigidbody2D rb;
    private bool isGliding = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Glide();
    }

    void Move()
    {
        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void Glide()
    {
        if (isGliding)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -glideSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Fire":
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * 1.5f);
                break;
            case "Lightning":
                rb.linearVelocity = new Vector2(boostSpeed, rb.linearVelocity.y);
                break;
            case "Air":
                isGliding = true;
                break;
            case "Water":
                // Call wave boost logic
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Air")
        {
            isGliding = false;
        }
    }
}
