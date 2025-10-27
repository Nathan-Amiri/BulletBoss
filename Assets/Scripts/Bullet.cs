using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public CircleCollider2D col;

    public bool purple;
    private bool purpleBounceOn;

    public float blueAngle;

    private void Update()
    {
        if (purple && transform.position.magnitude < 3.6f)
            purpleBounceOn = true;

        if (purpleBounceOn && transform.position.magnitude > 3.6f)
        {
            rb.linearVelocity = Vector2.Reflect(rb.linearVelocity.normalized, transform.position.normalized) * rb.linearVelocity.magnitude;
            transform.position = transform.position.normalized * 3.6f;
        }
    }

    private void FixedUpdate()
    {
        if (blueAngle != 0)
        {
            Vector3 currentDirection = rb.linearVelocity.normalized;
            float newAngle = blueAngle * Time.deltaTime;
            Vector3 newDirection = Quaternion.AngleAxis(newAngle, Vector3.forward) * currentDirection;
            rb.linearVelocity = newDirection * rb.linearVelocity.magnitude;
        }
    }

    public void Red()
    {
        StartCoroutine(RedExplode());
    }
    private IEnumerator RedExplode()
    {
        yield return new WaitForSeconds(1);

        sr.enabled = true;
        col.enabled = true;

        yield return new WaitForSeconds(.5f);

        Destroy(gameObject);
    }
}