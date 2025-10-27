using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<Bullet> bulletPrefs = new();

    public float startTime;

    public float bulletDuration;

    private float time;

    public enum T { red, yellow, green, purple, blue }

    public float yellowSpeed;
    public float greenSpeed;
    public float purpleSpeed;
    public float blueSpeed;

    private void Start()
    {
        audioSource.time = startTime;
        audioSource.Play();
    }

    private void SpawnBullet(T bulletType, float xPos, float yPos, Vector2 direction = default, float blueAngle = 0)
    {
        Bullet bullet = Instantiate(bulletPrefs[(int)bulletType], new(xPos, yPos), Quaternion.identity);

        float bulletSpeed = 0;

        if (bulletType == T.red)
        {
            bullet.Red();
        }
        else if (bulletType == T.yellow)
        {
            bulletSpeed = yellowSpeed;
        }
        if (bulletType == T.green)
        {
            bulletSpeed = greenSpeed;
        }
        else if (bulletType == T.purple)
        {
            bulletSpeed = purpleSpeed;
            bullet.purple = true;
        }
        else if (bulletType == T.blue)
        {
            bulletSpeed = blueSpeed;
            bullet.blueAngle = blueAngle;
        }


        bullet.rb.linearVelocity = direction * bulletSpeed;

        StartCoroutine(DestroyBullet(bullet));
    }
    private IEnumerator DestroyBullet(Bullet bullet)
    {
        yield return new WaitForSeconds(bulletDuration);

        if (bullet != null)
            Destroy(bullet.gameObject);
    }


    public void TimedEvent()
    {
        EventList();
        time += .25f;
    }

    private void EventList()
    {
        // Blue angle 15, 25, or 35
        // Arena's radius is 3.75
        if (time == 0)
        {
            SpawnBullet(T.red, -2, -2);
            SpawnBullet(T.red, 2, 2);
        }
        else if (time == 1)
        {
            SpawnBullet(T.red, -2, 2);
            SpawnBullet(T.red, 2, -2);
        }
        else if (time == 2)
        {
            SpawnBullet(T.red, 2, 2);
            SpawnBullet(T.red, -2, -2);
        }
        else if (time == 3)
        {
            SpawnBullet(T.red, 2, -2);
            SpawnBullet(T.red, -2, 2);
        }
    }
}