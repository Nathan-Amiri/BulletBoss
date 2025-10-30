using System;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private ScreenShake screenShake;
    [SerializeField] private BloomPulse bloomPulse;

    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private AudioClip bounceClip;
    [SerializeField] private AudioClip reverseClip;
    [SerializeField] private AudioClip damageClip;

    private Vector2 moveInput;

    private readonly float acceleration = 4.5f;
    private readonly float speedCap = 8;

    private bool bounceCooldown;
    private bool damageCooldown;

    [NonSerialized] public bool isStunned;
    [NonSerialized] public int score = 100;

    private void Update()
    {
        if (isStunned)
            return;

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
        if (isStunned)
            return;

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
        sfxSource.PlayOneShot(reverseClip);
        rb.linearVelocity *= -0.75f;
    }

    private void Bounce()
    {
        if (!bounceCooldown)
        {
            bounceCooldown = true;

            screenShake.StartShake(.15f, .2f);
            sfxSource.PlayOneShot(bounceClip);

            Invoke(nameof(BounceCooldown), .1f);
        }
    }
    private void BounceCooldown()
    {
        bounceCooldown = false;
    }
    private void Damage()
    {
        if (!damageCooldown)
        {
            damageCooldown = true;

            screenShake.StartShake(.5f, .5f);
            sfxSource.PlayOneShot(damageClip);

            Invoke(nameof(DamageCooldown), .6f);

            score -= 2;
            if (score < 0)
                score = 0;
            scoreText.color = Color.red;
            scoreText.text = "Score: " + score;
            Invoke(nameof(ScoreColorReset), .5f);
        }
    }
    private void DamageCooldown()
    {
        damageCooldown = false;
    }
    private void ScoreColorReset()
    {
        scoreText.color = Color.white;
    }

    public void StunPlayer()
    {
        isStunned = true;
        rb.linearVelocity = Vector2.zero;
    }
}