using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private ScreenShake screenShake;

    [SerializeField] private AudioClip bounceClip;
    [SerializeField] private AudioClip reverseClip;
    [SerializeField] private AudioClip damageClip;

    private Vector2 moveInput;

    private readonly float acceleration = 4.5f;
    private readonly float speedCap = 8;

    private bool bounceCooldown;

    private void Update()
    {
        if (transform.position.magnitude > 3.52f)
        {
            rb.linearVelocity = Vector2.Reflect(rb.linearVelocity.normalized, transform.position.normalized) * rb.linearVelocity.magnitude;
            transform.position = transform.position.normalized * 3.52f;
            Bounce();
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
            Damage();
    }

    private void Reverse()
    {
        screenShake.StartShake(.1f, .1f);
        audioSource.PlayOneShot(reverseClip);
        rb.linearVelocity *= -0.75f;
    }

    private void Bounce()
    {
        if (!bounceCooldown)
        {
            bounceCooldown = true;

            screenShake.StartShake(.15f, .2f);
            audioSource.PlayOneShot(bounceClip);

            Invoke(nameof(BounceCooldown), .1f);
        }
    }
    private void BounceCooldown()
    {
        bounceCooldown = false;
    }
    private void Damage()
    {
        screenShake.StartShake(.5f, .5f);
        audioSource.PlayOneShot(damageClip);
    }
}