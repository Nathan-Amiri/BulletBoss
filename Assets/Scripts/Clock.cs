using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clock : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private GameObject resultsScreen;
    [SerializeField] private TMP_Text resultsText;
    [SerializeField] private GameObject score;
    [SerializeField] private List<Bullet> bulletPrefs = new();

    [Serializable] class Test { public T bulletType; public float xPos; public float yPos; public Vector2 direction; public float blueAngle; }
    [SerializeField] private List<Test> tests = new();
    private void BulletTest()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            foreach (Test test in tests)
                SpawnBullet(test.bulletType, test.xPos, test.yPos, test.direction, test.blueAngle);
    }

    public float startTime;

    public float bulletDuration;

    private float time;

    public enum T { red, yellow, green, purple, blue }

    public float yellowSpeed;
    public float greenSpeed;
    public float purpleSpeed;
    public float blueSpeed;

    private bool readyToContinue;



    private void EndGame()
    {
        player.StunPlayer();
        player.transform.position = Vector2.zero;
        resultsScreen.SetActive(true);
        resultsText.text = "GAME OVER\n\nScore: " + player.score + "/100";
        score.SetActive(false);

        Invoke(nameof(Continue), 2);
    }
    private void Continue()
    {
        resultsText.text = "GAME OVER\n\nScore: " + player.score + "/100\n\nPress Space to continue";
        readyToContinue = true;
    }
    private void Update() // For Game End
    {
        BulletTest();
        if (readyToContinue && Input.GetButtonDown("Reverse"))
            SceneManager.LoadScene(0);
    }



    private void Start()
    {
        musicSource.time = startTime;
        time = startTime;
        Invoke(nameof(StartMusic), 2);//4);
    }
    private void StartMusic()
    {
        musicSource.Play();
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
        if (time == 0)
        {
            SpawnBullet(T.blue, -5, 0, new(1, 0), 0);
        }
        else if (time == 1)
        {
            SpawnBullet(T.blue, 5, 3, new(-1, 0), 0);
        }
        else if (time == 2)
        {
            SpawnBullet(T.blue, -5, -3, new(1, 0), 0);
        }
        else if (time == 3)
        {
            SpawnBullet(T.blue, 5, 0, new(-1, 0), 0);
        }
        else if (time == 4)
        {
            SpawnBullet(T.blue, -5, 3, new(1, 0), 0);
        }
        else if (time == 5)
        {
            SpawnBullet(T.blue, -5, 0, new(1, 0), 0);
        }
        else if (time == 6)
        {
            SpawnBullet(T.blue, 5, -3, new(-1, 0), 0);
        }
        else if (time == 7)
        {
            SpawnBullet(T.blue, 5, 0, new(-1, 0), 0);
        }

        else if (time == 8)
        {
            //Blue Left Ring:
            SpawnBullet(T.blue, -5, 5, new(1, -1), -45);
            SpawnBullet(T.blue, -5, -5, new(1, 1), 45);
        }
        else if (time == 9)
        {
            //Blue Right Ring:
            SpawnBullet(T.blue, 5, 5, new(-1, -1), 45);
            SpawnBullet(T.blue, 5, -5, new(-1, 1), -45);
        }
        else if (time == 10)
        {
            //Blue Up Ring:
            SpawnBullet(T.blue, 5, 5, new(-1, -1), -45);
            SpawnBullet(T.blue, -5, 5, new(1, -1), 45);
        }
        else if (time == 11)
        {
            //Blue Down Ring:
            SpawnBullet(T.blue, 5, -5, new(-1, 1), 45);
            SpawnBullet(T.blue, -5, -5, new(1, 1), -45);
        }

        else if (time == 12)
        {
            //Blue Left Ring:
            SpawnBullet(T.blue, -5, 5, new(1, -1), 45);
            SpawnBullet(T.blue, -5, -5, new(1, 1), -45);
        }
        else if (time == 13)
        {
            //Blue Right Ring:
            SpawnBullet(T.blue, 5, 5, new(-1, -1), -45);
            SpawnBullet(T.blue, 5, -5, new(-1, 1), 45);
        }
        else if (time == 14)
        {
            //Blue Up Ring:
            SpawnBullet(T.blue, 5, 5, new(-1, -1), 45);
            SpawnBullet(T.blue, -5, 5, new(1, -1), -45);

        }
        else if (time == 15)
        {
            //Blue Down Ring:
            SpawnBullet(T.blue, 5, -5, new(-1, 1), -45);
            SpawnBullet(T.blue, -5, -5, new(1, 1), 45);
        }

        else if (time == 16f)
            SpawnBullet(T.blue, 5, -5, new(-1, 1), 45);
        else if (time == 16.5f)
            SpawnBullet(T.blue, -5, 5, new(1, -1), -45);
        else if (time == 17)
            SpawnBullet(T.blue, -5, -5, new(1, 1), 45);
        else if (time == 17.5)
            SpawnBullet(T.blue, -5, 5, new(1, -1), 45);
        else if (time == 18)
            SpawnBullet(T.blue, 5, 5, new(-1, -1), 45);
        else if (time == 18.5f)
            SpawnBullet(T.blue, 5, -5, new(-1, 1), -45);
        else if (time == 19)
            SpawnBullet(T.blue, -5, -5, new(1, 1), -45);
        else if (time == 19.5f)
            SpawnBullet(T.blue, 5, 5, new(-1, -1), -45);

        else if (time == 20)
            SpawnBullet(T.blue, 5, -5, new(-1, 1), 45);
        else if (time == 20.5f)
            SpawnBullet(T.blue, -5, 5, new(1, -1), -45);
        else if (time == 21)
            SpawnBullet(T.blue, 5, -5, new(-1, 1), -45);
        else if (time == 21.5)
            SpawnBullet(T.blue, -5, 5, new(1, -1), 45);
        else if (time == 22)
            SpawnBullet(T.blue, 5, 5, new(-1, -1), 45);
        else if (time == 22.5f)
            SpawnBullet(T.blue, -5, -5, new(1, 1), 45);
        else if (time == 23)
            SpawnBullet(T.blue, -5, -5, new(1, 1), -45);
        else if (time == 23.5f)
            SpawnBullet(T.blue, 5, 5, new(-1, -1), -45);

        else if (time == 24)
        {
            //Blue Left Slope
            SpawnBullet(T.blue, -6, 2, new(1, 0), -35);
            SpawnBullet(T.blue, -6, 3, new(1, 0), -35);
            SpawnBullet(T.blue, -6, 4, new(1, 0), -35);

            //Green Outside Left
            SpawnBullet(T.green, -8, 0, new(1, .7f));
            SpawnBullet(T.green, -8, 0, new(1, -.7f));
        }
        else if (time == 26)
        {
            //Blue Right Slope
            SpawnBullet(T.blue, 6, 2, new(-1, 0), 35);
            SpawnBullet(T.blue, 6, 3, new(-1, 0), 35);
            SpawnBullet(T.blue, 6, 4, new(-1, 0), 35);

            //Green Outside Right
            SpawnBullet(T.green, 8, 0, new(-1, .7f));
            SpawnBullet(T.green, 8, 0, new(-1, -.7f));
        }
        else if (time == 28)
        {
            //Blue Left Up Slope
            SpawnBullet(T.blue, -6, -2, new(1, 0), 35);
            SpawnBullet(T.blue, -6, -3, new(1, 0), 35);
            SpawnBullet(T.blue, -6, -4, new(1, 0), 35);

            //Green Outside Left
            SpawnBullet(T.green, -8, 0, new(1, .7f));
            SpawnBullet(T.green, -8, 0, new(1, -.7f));
        }
        else if (time == 30)
        {
            //Blue Right Up Slope
            SpawnBullet(T.blue, 6, -2, new(-1, 0), -35);
            SpawnBullet(T.blue, 6, -3, new(-1, 0), -35);
            SpawnBullet(T.blue, 6, -4, new(-1, 0), -35);

            //Green Outside Right
            SpawnBullet(T.green, 8, 0, new(-1, .7f));
            SpawnBullet(T.green, 8, 0, new(-1, -.7f));
        }

        else if (time == 32)
        {
            //Blue Left Slope
            SpawnBullet(T.blue, -6, 2, new(1, 0), -35);
            SpawnBullet(T.blue, -6, 3, new(1, 0), -35);
            SpawnBullet(T.blue, -6, 4, new(1, 0), -35);

            //Green Outside Left
            SpawnBullet(T.green, -8, 0, new(1, .7f));
            SpawnBullet(T.green, -8, 0, new(1, -.7f));
        }
        else if (time == 32.5f)
        {
            //Green Outside Left
            SpawnBullet(T.green, -8, 0, new(1, .7f));
            SpawnBullet(T.green, -8, 0, new(1, -.7f));
        }
        else if (time == 33)
        {
            //Green Outside Right
            SpawnBullet(T.green, 8, 0, new(-1, .7f));
            SpawnBullet(T.green, 8, 0, new(-1, -.7f));
        }
        else if (time == 33.5f)
        {
            //Green Outside Right
            SpawnBullet(T.green, 8, 0, new(-1, .7f));
            SpawnBullet(T.green, 8, 0, new(-1, -.7f));
        }
        else if (time == 34)
        {
            //Blue Right Slope
            SpawnBullet(T.blue, 6, 2, new(-1, 0), 35);
            SpawnBullet(T.blue, 6, 3, new(-1, 0), 35);
            SpawnBullet(T.blue, 6, 4, new(-1, 0), 35);

            //Green Outside Left
            SpawnBullet(T.green, -8, 0, new(1, .7f));
            SpawnBullet(T.green, -8, 0, new(1, -.7f));
        }
        else if (time == 34.5f)
        {
            //Green Outside Left
            SpawnBullet(T.green, -8, 0, new(1, .7f));
            SpawnBullet(T.green, -8, 0, new(1, -.7f));
        }
        else if (time == 35)
        {
            //Green Outside Right
            SpawnBullet(T.green, 8, 0, new(-1, .7f));
            SpawnBullet(T.green, 8, 0, new(-1, -.7f));
        }
        else if (time == 35.5f)
        {
            //Green Outside Right
            SpawnBullet(T.green, 8, 0, new(-1, .7f));
            SpawnBullet(T.green, 8, 0, new(-1, -.7f));
        }
        else if (time == 36)
        {
            //Blue Left Up Slope
            SpawnBullet(T.blue, -6, -2, new(1, 0), 35);
            SpawnBullet(T.blue, -6, -3, new(1, 0), 35);
            SpawnBullet(T.blue, -6, -4, new(1, 0), 35);

            //Green Outside Left
            SpawnBullet(T.green, -8, 0, new(1, .7f));
            SpawnBullet(T.green, -8, 0, new(1, -.7f));
        }
        else if (time == 36.5f)
        {
            //Green Outside Left
            SpawnBullet(T.green, -8, 0, new(1, .7f));
            SpawnBullet(T.green, -8, 0, new(1, -.7f));
        }
        else if (time == 37)
        {
            //Green Outside Right
            SpawnBullet(T.green, 8, 0, new(-1, .7f));
            SpawnBullet(T.green, 8, 0, new(-1, -.7f));
        }
        else if (time == 37.5f)
        {
            //Green Outside Right
            SpawnBullet(T.green, 8, 0, new(-1, .7f));
            SpawnBullet(T.green, 8, 0, new(-1, -.7f));
        }
        else if (time == 38)
        {
            //Blue Right Up Slope
            SpawnBullet(T.blue, 6, -2, new(-1, 0), -35);
            SpawnBullet(T.blue, 6, -3, new(-1, 0), -35);
            SpawnBullet(T.blue, 6, -4, new(-1, 0), -35);

            //Green Outside Left
            SpawnBullet(T.green, -8, 0, new(1, .7f));
            SpawnBullet(T.green, -8, 0, new(1, -.7f));
        }
        else if (time == 38.5f)
        {
            //Green Outside Left
            SpawnBullet(T.green, -8, 0, new(1, .7f));
            SpawnBullet(T.green, -8, 0, new(1, -.7f));
        }
        else if (time == 39)
        {
            //Green Outside Right
            SpawnBullet(T.green, 8, 0, new(-1, .7f));
            SpawnBullet(T.green, 8, 0, new(-1, -.7f));
        }
        else if (time == 39.5f)
        {
            //Green Outside Right
            SpawnBullet(T.green, 8, 0, new(-1, .7f));
            SpawnBullet(T.green, 8, 0, new(-1, -.7f));
        }

        else if (time == 40)
        {
            //Yellow Outside Spin
            SpawnBullet(T.yellow, -8, 0, new(1, 1));
            SpawnBullet(T.yellow, 8, 0, new(-1, -1));
            SpawnBullet(T.yellow, 0, 8, new(1, -1));
            SpawnBullet(T.yellow, 0, -8, new(-1, 1));

            //Green Center
            SpawnBullet(T.green, -8, 2, new(1, -.25f));
        }
        else if (time == 40.5f)
            SpawnBullet(T.green, -8, 2, new(1, -.25f));
        else if (time == 41)
            SpawnBullet(T.green, -8, 2, new(1, -.25f));
        else if (time == 41.5f)
            SpawnBullet(T.green, -8, 2, new(1, -.25f));

        else if (time == 42)
            SpawnBullet(T.green, 8, 2, new(-1, -.25f));
        else if (time == 42.5f)
            SpawnBullet(T.green, 8, 2, new(-1, -.25f));
        else if (time == 43)
            SpawnBullet(T.green, 8, 2, new(-1, -.25f));
        else if (time == 43.5f)
            SpawnBullet(T.green, 8, 2, new(-1, -.25f));

        else if (time == 44)
            SpawnBullet(T.green, -8, 2, new(1, -.25f));
        else if (time == 44.5f)
            SpawnBullet(T.green, -8, 2, new(1, -.25f));
        else if (time == 45)
            SpawnBullet(T.green, -8, 2, new(1, -.25f));
        else if (time == 45.5f)
            SpawnBullet(T.green, -8, 2, new(1, -.25f));

        else if (time == 46)
            SpawnBullet(T.green, 8, 2, new(-1, -.25f));
        else if (time == 46.5f)
            SpawnBullet(T.green, 8, 2, new(-1, -.25f));
        else if (time == 47)
            SpawnBullet(T.green, 8, 2, new(-1, -.25f));
        else if (time == 47.5f)
            SpawnBullet(T.green, 8, 2, new(-1, -.25f));

        else if (time == 48)
        {
            //Blue Meet
            SpawnBullet(T.blue, -6, 1, new(1, 0), -15);
            SpawnBullet(T.blue, -6, -1, new(1, 0), 15);
            SpawnBullet(T.blue, 6, -1, new(-1, 0), -15);
            SpawnBullet(T.blue, 6, 1, new(-1, 0), 15);

            //Yellow Top Left going Down
            SpawnBullet(T.yellow, -3, 8, new(0, -1));
        }
        else if (time == 49)
        {
            //Blue Meet
            SpawnBullet(T.blue, -6, 1, new(1, 0), -15);
            SpawnBullet(T.blue, -6, -1, new(1, 0), 15);
            SpawnBullet(T.blue, 6, -1, new(-1, 0), -15);
            SpawnBullet(T.blue, 6, 1, new(-1, 0), 15);
        }
        else if (time == 50)
        {
            //Blue Meet
            SpawnBullet(T.blue, -6, 1, new(1, 0), -15);
            SpawnBullet(T.blue, -6, -1, new(1, 0), 15);
            SpawnBullet(T.blue, 6, -1, new(-1, 0), -15);
            SpawnBullet(T.blue, 6, 1, new(-1, 0), 15);

            SpawnBullet(T.yellow, 3, -8, new(0, 1));
        }

        else if (time == 52)
        {
            //Blue Meet Vertical
            SpawnBullet(T.blue, -1, 6, new(0, -1), 15);
            SpawnBullet(T.blue, 1, 6, new(0, -1), -15);
            SpawnBullet(T.blue, 1, -6, new(0, 1), 15);
            SpawnBullet(T.blue, -1, -6, new(0, 1), -15);

            SpawnBullet(T.yellow, -8, 3, new(1, 0));
        }
        else if (time == 53)
        {
            //Blue Meet Vertical
            SpawnBullet(T.blue, -1, 6, new(0, -1), 15);
            SpawnBullet(T.blue, 1, 6, new(0, -1), -15);
            SpawnBullet(T.blue, 1, -6, new(0, 1), 15);
            SpawnBullet(T.blue, -1, -6, new(0, 1), -15);
        }
        else if (time == 54)
        {
            //Blue Meet Vertical
            SpawnBullet(T.blue, -1, 6, new(0, -1), 15);
            SpawnBullet(T.blue, 1, 6, new(0, -1), -15);
            SpawnBullet(T.blue, 1, -6, new(0, 1), 15);
            SpawnBullet(T.blue, -1, -6, new(0, 1), -15);

            SpawnBullet(T.yellow, 8, -3, new(-1, 0));
        }

        else if (time == 56)
        {
            //Red Left
            SpawnBullet(T.red, -2.85f, 0);
        }
        else if (time == 58)
            SpawnBullet(T.red, 0, 0);
        else if (time == 60)
            SpawnBullet(T.red, 2.85f, 0);
        else if (time == 62)
            SpawnBullet(T.red, 0, 0);

        else if (time == 64)
        {
            //Red Left Triplet
            SpawnBullet(T.red, -2.85f, 0);
            SpawnBullet(T.red, -2, 2);
            SpawnBullet(T.red, -2, -2);
        }
        else if (time == 66)
        {
            //Red Vertical Triplet
            SpawnBullet(T.red, 0, 0);
            SpawnBullet(T.red, 0, 2.85f);
            SpawnBullet(T.red, 0, -2.85f);
        }
        else if (time == 68)
        {
            //Red Right Triplet
            SpawnBullet(T.red, 2.85f, 0);
            SpawnBullet(T.red, 2, 2);
            SpawnBullet(T.red, 2, -2);
        }
        else if (time == 70)
        {
            //Red Vertical Triplet
            SpawnBullet(T.red, 0, 0);
            SpawnBullet(T.red, 0, 2.85f);
            SpawnBullet(T.red, 0, -2.85f);
        }

        else if (time == 72)
        {
            // Red Up Left Diagonal Section
            SpawnBullet(T.red, -2, 2);
            SpawnBullet(T.red, -1, 1);
            SpawnBullet(T.red, 0, 0);
        }
        else if (time == 74)
        {
            // Red Up Right Diagonal Section
            SpawnBullet(T.red, 2, 2);
            SpawnBullet(T.red, 1, 1);
            SpawnBullet(T.red, 0, 0);
        }
        else if (time == 76)
        {
            // Red Down Right Diagonal Section
            SpawnBullet(T.red, 2, -2);
            SpawnBullet(T.red, 1, -1);
            SpawnBullet(T.red, 0, 0);

        }
        else if (time == 78)
        {
            // Red Down Left Diagonal Section
            SpawnBullet(T.red, -2, -2);
            SpawnBullet(T.red, -1, -1);
            SpawnBullet(T.red, 0, 0);
        }

        else if (time == 80)
        {
            // Red Up Left Diagonal Section
            SpawnBullet(T.red, -2, 2);
            SpawnBullet(T.red, -1, 1);
            SpawnBullet(T.red, 0, 0);
        }
        else if (time == 81)
            SpawnBullet(T.yellow, 6, 0, new(-1, 0));
        else if (time == 82)
        {
            // Red Up Right Diagonal Section
            SpawnBullet(T.red, 2, 2);
            SpawnBullet(T.red, 1, 1);
            SpawnBullet(T.red, 0, 0);
        }
        else if (time == 83)
            SpawnBullet(T.yellow, 0, -6, new(0, 1));

        else if (time == 84)
        {
            // Red Down Right Diagonal Section
            SpawnBullet(T.red, 2, -2);
            SpawnBullet(T.red, 1, -1);
            SpawnBullet(T.red, 0, 0);
        }
        else if (time == 85)
            SpawnBullet(T.yellow, -6, 0, new(1, 0));
        else if (time == 86)
        {
            // Red Down Left Diagonal Section
            SpawnBullet(T.red, -2, -2);
            SpawnBullet(T.red, -1, -1);
            SpawnBullet(T.red, 0, 0);
        }
        else if (time == 87)
            SpawnBullet(T.yellow, 0, 6, new(0, -1));

        else if (time == 88)
        {
            //Yellow Cross
            SpawnBullet(T.yellow, -8, -3, new(1, .25f));
            SpawnBullet(T.yellow, 8, -3, new(-1, .25f));
        }
        else if (time == 90)
        {
            //Yellow Cross
            SpawnBullet(T.yellow, -8, -3, new(1, .25f));
            SpawnBullet(T.yellow, 8, -3, new(-1, .25f));
        }
        else if (time == 92)
        {
            //Yellow Cross
            SpawnBullet(T.yellow, -8, 3, new(1, -.25f));
            SpawnBullet(T.yellow, 8, 3, new(-1, -.25f));
        }
        else if (time == 94)
        {
            //Yellow Cross
            SpawnBullet(T.yellow, -8, 3, new(1, -.25f));
            SpawnBullet(T.yellow, 8, 3, new(-1, -.25f));
        }

        else if (time == 96)
        {
            //Yellow Outside Spin
            SpawnBullet(T.yellow, -8, 0, new(1, 1));
            SpawnBullet(T.yellow, 8, 0, new(-1, -1));
            SpawnBullet(T.yellow, 0, 8, new(1, -1));
            SpawnBullet(T.yellow, 0, -8, new(-1, 1));
        }
        else if (time == 98)
        {
            //Yellow Outside Spin
            SpawnBullet(T.yellow, -8, 0, new(1, 1));
            SpawnBullet(T.yellow, 8, 0, new(-1, -1));
            SpawnBullet(T.yellow, 0, 8, new(1, -1));
            SpawnBullet(T.yellow, 0, -8, new(-1, 1));
        }
        else if (time == 100)
        {
            //Yellow Outside Spin
            SpawnBullet(T.yellow, -8, 0, new(1, 1));
            SpawnBullet(T.yellow, 8, 0, new(-1, -1));
            SpawnBullet(T.yellow, 0, 8, new(1, -1));
            SpawnBullet(T.yellow, 0, -8, new(-1, 1));
        }

        else if (time == 104)
        {
            //Blue Left Ring:
            SpawnBullet(T.blue, -5, 5, new(1, -1), -45);
            SpawnBullet(T.blue, -5, -5, new(1, 1), 45);

            SpawnBullet(T.green, -8, 0, new(1, 0));
        }
        else if (time == 106)
        {
            //Blue Right Ring:
            SpawnBullet(T.blue, 5, 5, new(-1, -1), 45);
            SpawnBullet(T.blue, 5, -5, new(-1, 1), -45);

            SpawnBullet(T.green, 8, 0, new(-1, 0));
        }
        else if (time == 108)
        {
            //Blue Down Ring:
            SpawnBullet(T.blue, 5, -5, new(-1, 1), 45);
            SpawnBullet(T.blue, -5, -5, new(1, 1), -45);

            SpawnBullet(T.green, 0, -8, new(0, 1));
        }
        else if (time == 110)
        {
            //Blue Up Ring:
            SpawnBullet(T.blue, 5, 5, new(-1, -1), -45);
            SpawnBullet(T.blue, -5, 5, new(1, -1), 45);

            SpawnBullet(T.green, 0, 8, new(0, -1));
        }

        else if (time == 112)
        {
            //Blue Left Ring:
            SpawnBullet(T.blue, -5, 5, new(1, -1), -45);
            SpawnBullet(T.blue, -5, -5, new(1, 1), 45);

            // Left half of Blue meet:
            SpawnBullet(T.blue, -6, 1, new(1, 0), -15);
            SpawnBullet(T.blue, -6, -1, new(1, 0), 15);
        }
        else if (time == 114)
        {
            //Blue Right Ring:
            SpawnBullet(T.blue, 5, 5, new(-1, -1), 45);
            SpawnBullet(T.blue, 5, -5, new(-1, 1), -45);

            // Right half of Blue meet:
            SpawnBullet(T.blue, 6, -1, new(-1, 0), -15);
            SpawnBullet(T.blue, 6, 1, new(-1, 0), 15);
        }

        else if (time == 120)
            SpawnBullet(T.purple, -4, 0, new(1, 0));
        else if (time == 122)
            SpawnBullet(T.purple, 4, 0, new(-1, 0));
        else if (time == 124)
            SpawnBullet(T.purple, -4, -2, new(1, 1));
        else if (time == 126)
            SpawnBullet(T.purple, 4, 2, new(-1, -1));

        else if (time == 128)
        {
            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        }
        else if (time == 129)
        {
            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        }
        else if (time == 130)
        {
            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        }
        else if (time == 131)
        {
            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        }

        else if (time == 132)
        {
            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        }
        else if (time == 133)
        {
            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        }
        else if (time == 134)
        {
            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        }
        else if (time == 135)
        {
            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        }

        // START RANDOM 8:
        else if (time == 136)
        {
            SpawnBullet(T.yellow, -8, -3, new(1, .25f));

            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        }
        else if (time == 137)
        {
            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        }
        else if (time == 138)
        {
            SpawnBullet(T.red, 0, 0);

            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        }
        else if (time == 139)
        {
            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        }

        else if (time == 140)
        {
            SpawnBullet(T.blue, 6, -3, new(-1, 0), -35);

            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        }
        else if (time == 141)
        {
            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        } 
        else if (time == 142)
        {
            SpawnBullet(T.green, -8, 2, new(1, -.25f));

            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        }
        else if (time == 143)
        {
            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        }

        // END RANDOM 8, START HELL 8 (random + stray purples)

        else if (time == 144)
        {
            SpawnBullet(T.blue, -5, -5, new(1, 1), 45);

            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        }
        else if (time == 145)
        {
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));

            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        }
        else if (time == 146)
        {
            SpawnBullet(T.red, 1, -1);

            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        }
        else if (time == 147)
        {
            SpawnBullet(T.green, -8, 4, new(1, -.15f));

            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        }

        else if (time == 148)
        {
            SpawnBullet(T.yellow, -3, 8, new(0, -1));

            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        }
        else if (time == 149)
        {
            SpawnBullet(T.purple, 4, 0, new(-1, 0));

            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        }
        else if (time == 150)
        {
            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));

            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        }
        else if (time == 151)
        {
            SpawnBullet(T.green, 8, -4, new(-1, .15f));

            SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
            SpawnBullet(T.purple, 3.5f, 3, new(0, -1));
        }

        else if (time == 152)
        {
            //Blue Left Ring:
            SpawnBullet(T.blue, -5, 5, new(1, -1), -45);
            SpawnBullet(T.blue, -5, -5, new(1, 1), 45);

            SpawnBullet(T.green, -8, 0, new(1, 0));
        }
        else if (time == 153)
        {
            //Blue Right Ring:
            SpawnBullet(T.blue, 5, 5, new(-1, -1), 45);
            SpawnBullet(T.blue, 5, -5, new(-1, 1), -45);

            SpawnBullet(T.green, 8, 0, new(-1, 0));
        }
        else if (time == 154)
        {
            //Blue Up Ring:
            SpawnBullet(T.blue, 5, 5, new(-1, -1), -45);
            SpawnBullet(T.blue, -5, 5, new(1, -1), 45);

            SpawnBullet(T.green, 0, 8, new(0, -1));
        }
        else if (time == 155)
        {
            //Blue Down Ring:
            SpawnBullet(T.blue, 5, -5, new(-1, 1), 45);
            SpawnBullet(T.blue, -5, -5, new(1, 1), -45);

            SpawnBullet(T.green, 0, -8, new(0, 1));
        }

        else if (time == 156)
        {
            //Blue Left Ring:
            SpawnBullet(T.blue, -5, 5, new(1, -1), 45);
            SpawnBullet(T.blue, -5, -5, new(1, 1), -45);

            SpawnBullet(T.green, -8, 0, new(1, 0));
        }
        else if (time == 157)
        {
            //Blue Right Ring:
            SpawnBullet(T.blue, 5, 5, new(-1, -1), -45);
            SpawnBullet(T.blue, 5, -5, new(-1, 1), 45);

            SpawnBullet(T.green, 8, 0, new(-1, 0));
        }
        else if (time == 158)
        {
            //Blue Up Ring:
            SpawnBullet(T.blue, 5, 5, new(-1, -1), 45);
            SpawnBullet(T.blue, -5, 5, new(1, -1), -45);

            SpawnBullet(T.green, 0, 8, new(0, -1));
        }
        else if (time == 159)
        {
            //Blue Down Ring:
            SpawnBullet(T.blue, 5, -5, new(-1, 1), -45);
            SpawnBullet(T.blue, -5, -5, new(1, 1), 45);

            SpawnBullet(T.green, 0, -8, new(0, 1));
        }

        else if (time == 160)
        {
            //Purple Opposite Bounce
            SpawnBullet(T.purple, -4, -2, new(1, 0));
            SpawnBullet(T.purple, 4, -2, new(-1, 0));
        }
        else if (time == 162)
        {
            //Purple Opposite Bounce
            SpawnBullet(T.purple, -4, 2, new(1, 0));
            SpawnBullet(T.purple, 4, 2, new(-1, 0));
        }
        else if (time == 164)
        {
            //Purple Opposite Bounce
            SpawnBullet(T.purple, -4, -2, new(1, 0));
            SpawnBullet(T.purple, 4, -2, new(-1, 0));

            SpawnBullet(T.green, -8, 2, new(1, -.25f));
        }
        else if (time == 166)
        {
            //Purple Opposite Bounce
            SpawnBullet(T.purple, -4, 2, new(1, 0));
            SpawnBullet(T.purple, 4, 2, new(-1, 0));

            SpawnBullet(T.green, 8, 2, new(-1, .25f));
        }

        else if (time == 168)
        {
            //Green Outside Left
            SpawnBullet(T.green, -8, 0, new(1, .7f));
            SpawnBullet(T.green, -8, 0, new(1, -.7f));
        }
        else if (time == 168.5f)
        {
            //Green Outside Left
            SpawnBullet(T.green, -8, 0, new(1, .7f));
            SpawnBullet(T.green, -8, 0, new(1, -.7f));
        }
        else if (time == 169)
        {
            //Green Outside Right
            SpawnBullet(T.green, 8, 0, new(-1, .7f));
            SpawnBullet(T.green, 8, 0, new(-1, -.7f));
        }
        else if (time == 169.5f)
        {
            //Green Outside Right
            SpawnBullet(T.green, 8, 0, new(-1, .7f));
            SpawnBullet(T.green, 8, 0, new(-1, -.7f));
        }

        else if (time == 170)
        {
            //Green Outside Left
            SpawnBullet(T.green, -8, 0, new(1, .7f));
            SpawnBullet(T.green, -8, 0, new(1, -.7f));
        }
        else if (time == 170.5f)
        {
            //Green Outside Left
            SpawnBullet(T.green, -8, 0, new(1, .7f));
            SpawnBullet(T.green, -8, 0, new(1, -.7f));
        }
        else if (time == 171)
        {
            //Green Outside Right
            SpawnBullet(T.green, 8, 0, new(-1, .7f));
            SpawnBullet(T.green, 8, 0, new(-1, -.7f));
        }
        else if (time == 171.5f)
        {
            //Green Outside Right
            SpawnBullet(T.green, 8, 0, new(-1, .7f));
            SpawnBullet(T.green, 8, 0, new(-1, -.7f));
        }

        else if (time == 172)
        {
            //Green Outside Left
            SpawnBullet(T.green, -8, 0, new(1, .7f));
            SpawnBullet(T.green, -8, 0, new(1, -.7f));
        }
        else if (time == 172.5f)
        {
            //Green Outside Left
            SpawnBullet(T.green, -8, 0, new(1, .7f));
            SpawnBullet(T.green, -8, 0, new(1, -.7f));
        }
        else if (time == 173)
        {
            //Green Outside Right
            SpawnBullet(T.green, 8, 0, new(-1, .7f));
            SpawnBullet(T.green, 8, 0, new(-1, -.7f));
        }
        else if (time == 173.5f)
        {
            //Green Outside Right
            SpawnBullet(T.green, 8, 0, new(-1, .7f));
            SpawnBullet(T.green, 8, 0, new(-1, -.7f));
        }

        else if (time == 174)
        {
            //Green Outside Left
            SpawnBullet(T.green, -8, 0, new(1, .7f));
            SpawnBullet(T.green, -8, 0, new(1, -.7f));
        }
        else if (time == 174.5f)
        {
            //Green Outside Left
            SpawnBullet(T.green, -8, 0, new(1, .7f));
            SpawnBullet(T.green, -8, 0, new(1, -.7f));
        }
        else if (time == 175)
        {
            //Green Outside Right
            SpawnBullet(T.green, 8, 0, new(-1, .7f));
            SpawnBullet(T.green, 8, 0, new(-1, -.7f));
        }
        else if (time == 175.5f)
        {
            //Green Outside Right
            SpawnBullet(T.green, 8, 0, new(-1, .7f));
            SpawnBullet(T.green, 8, 0, new(-1, -.7f));
        }

        else if (time == 176)
        {
            //Yellow Outside Spin
            SpawnBullet(T.yellow, -8, 0, new(1, 1));
            SpawnBullet(T.yellow, 8, 0, new(-1, -1));
            SpawnBullet(T.yellow, 0, 8, new(1, -1));
            SpawnBullet(T.yellow, 0, -8, new(-1, 1));
        }

        else if (time == 188)
            EndGame();

        //Purple Meet Horizontal
        //SpawnBullet(T.purple, -6, 0, new(1, 0));
        //SpawnBullet(T.purple, 6, 0, new(-1, 0));

        //Purple Meet Diagonal
        //SpawnBullet(T.purple, -4, -4, new(1, 1));
        //SpawnBullet(T.purple, 4, 4, new(-1, -1));

        //Purple Opposite Bounce
        //SpawnBullet(T.purple, -4, -2, new(1, 0));
        //SpawnBullet(T.purple, 4, -2, new(-1, 0));

        //Purple Circle Duo
        //SpawnBullet(T.purple, -3.5f, -3, new(0, 1));
        //SpawnBullet(T.purple, 3.5f, 3, new(0, -1));

        //Purple Circle Duo Complex
        //SpawnBullet(T.purple, -2, -3, new(0, 1));
        //SpawnBullet(T.purple, 2, 3, new(0, -1));

        //Purple Circle Quad
        //SpawnBullet(T.purple, 3.25f, 4, new(0, -1));
        //SpawnBullet(T.purple, 4, -3.25f, new(-1, 0));
        //SpawnBullet(T.purple, -4, 3.25f, new(1, 0));
        //SpawnBullet(T.purple, -3.25f, -4, new(0, 1));

        //Blue Left Ring:
        //SpawnBullet(T.blue, -5, 5, new(1, -1), -45);
        //SpawnBullet(T.blue, -5, -5, new(1, 1), 45);

        //Blue Meet
        //SpawnBullet(T.blue, -6, 1, new(1, 0), -15);
        //SpawnBullet(T.blue, -6, -1, new(1, 0), 15);
        //SpawnBullet(T.blue, 6, -1, new(-1, 0), -15);
        //SpawnBullet(T.blue, 6, 1, new(-1, 0), 15);

        //Blue Left Slope
        //SpawnBullet(T.blue, -6, 2, new(1, 0), -35);
        //SpawnBullet(T.blue, -6, 3, new(1, 0), -35);
        //SpawnBullet(T.blue, -6, 4, new(1, 0), -35);

        //Red Diagonal
        //SpawnBullet(T.red, -2, -2);
        //SpawnBullet(T.red, -1, -1);
        //SpawnBullet(T.red, 0, 0);
        //SpawnBullet(T.red, 1, 1);
        //SpawnBullet(T.red, 2, 2);

        //Red Right Triplet
        //SpawnBullet(T.red, 2.85f, 0);
        //SpawnBullet(T.red, 2, 2);
        //SpawnBullet(T.red, 2, -2);

        //Red Center
        //SpawnBullet(T.red, 0, 0);

        //Green Center
        //SpawnBullet(T.green, -8, 2, new(1, -.25f));

        //Green Top
        //SpawnBullet(T.green, -8, 4, new(1, -.15f));

        //Green Outside Left
        //SpawnBullet(T.green, -8, 0, new(1, .7f));
        //SpawnBullet(T.green, -8, 0, new(1, -.7f));

        //Yellow Middle
        //SpawnBullet(T.yellow, -8, 0, new(1, 0));

        //Yellow Bottom
        //SpawnBullet(T.yellow, -8, -3, new(1, 0));

        //Yellow Cross
        //SpawnBullet(T.yellow, -8, -3, new(1, .25f));
        //SpawnBullet(T.yellow, 8, -3, new(-1, .25f));

        //Yellow Outside Spin
        //SpawnBullet(T.yellow, -8, 0, new(1, 1));
        //SpawnBullet(T.yellow, 8, 0, new(-1, -1));
        //SpawnBullet(T.yellow, 0, 8, new(1, -1));
        //SpawnBullet(T.yellow, 0, -8, new(-1, 1));
    }
}