using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Clock : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Bullet bulletPref;

    public float startTime;

    public float bulletDuration;

    private float time;

    private void Start()
    {
        audioSource.time = startTime;
        audioSource.Play();
    }

    private void SpawnBullet(Vector2 position, Vector2 direction, int bulletType)
    {
        Bullet bullet = Instantiate(bulletPref, position, Quaternion.identity);
        bullet.rb.linearVelocity = direction * 10;

        StartCoroutine(DestroyBullet(bullet));
    }
    private IEnumerator DestroyBullet(Bullet bullet)
    {
        yield return new WaitForSeconds(bulletDuration);

        Destroy(bullet.gameObject);
    }


    public void TimedEvent()
    {
        if (time == 0)
            SpawnBullet(Vector2.zero, Vector2.right, 0);
        else if (time == 1)
            SpawnBullet(Vector2.zero, Vector2.right, 0);
        else if (time == 2)
            SpawnBullet(Vector2.zero, Vector2.right, 0);

        time += .25f;
    }
}