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

    public enum T { red, black, green, purple, blue }

    public float blackSpeed;
    public float greenSpeed;
    public float purpleSpeed;
    public float blueSpeed;

    public float redSize;
    public float blackSize;
    public float greenSize;
    public float purpleSize;
    public float blueSize;

    private void Start()
    {
        audioSource.time = startTime;
        audioSource.Play();
    }

    private void SpawnBullet(float xPos, float yPos, Vector2 direction, T bulletType)
    {
        Bullet bullet = Instantiate(bulletPref, new(xPos, yPos), Quaternion.identity);

        float bulletSpeed = 0;

        if (bulletType == T.green)
        {
            bulletSpeed = greenSpeed;
            bullet.transform.localScale = Vector2.one * greenSize;
            bullet.sr.color = Color.green;
        }
        if (bulletType == T.black)
        {
            bulletSpeed = blackSpeed;
            bullet.transform.localScale = Vector2.one * blackSize;
            bullet.sr.color = Color.black;
            bullet.blackOutline.SetActive(true);
        }

        bullet.rb.linearVelocity = direction * bulletSpeed;

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
            SpawnBullet(-7, 0, Vector2.right, T.black);
        else if (time == 1)
            SpawnBullet(-7, 0, Vector2.right, T.black);
        else if (time == 2)
            SpawnBullet(-7, 0, Vector2.right, T.black);

        time += .25f;
    }
}