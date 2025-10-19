using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Clock clock;

    private Vector2 moveInput;

    private readonly float acceleration = 3.5f;
    private readonly float speedCap = 9;

    private void Update()
    {
        if (Vector2.Distance(transform.position, Vector2.zero) > 3.6f)
        {
            rb.linearVelocity = Vector2.Reflect(rb.linearVelocity.normalized, transform.position.normalized) * rb.linearVelocity.magnitude;
            transform.position = transform.position.normalized * 3.6f;
        }

        moveInput = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetButtonDown("Reverse"))
            Reverse();
    }

    private void FixedUpdate()
    {
        rb.AddForce(acceleration * 100 * Time.fixedDeltaTime * moveInput);

        if (rb.linearVelocity.magnitude > speedCap)
            rb.linearVelocity = rb.linearVelocity.normalized * speedCap;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet"))
        {
            Debug.Log("Took damage!");
        }
    }

    private void Reverse()
    {
        rb.linearVelocity *= -0.75f;
    }
}