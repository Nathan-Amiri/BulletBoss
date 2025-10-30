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
        else if (time == 51)
        {
            //Blue Meet
            SpawnBullet(T.blue, -6, 1, new(1, 0), -15);
            SpawnBullet(T.blue, -6, -1, new(1, 0), 15);
            SpawnBullet(T.blue, 6, -1, new(-1, 0), -15);
            SpawnBullet(T.blue, 6, 1, new(-1, 0), 15);
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
        else if (time == 55)
        {
            //Blue Meet Vertical
            SpawnBullet(T.blue, -1, 6, new(0, -1), 15);
            SpawnBullet(T.blue, 1, 6, new(0, -1), -15);
            SpawnBullet(T.blue, 1, -6, new(0, 1), 15);
            SpawnBullet(T.blue, -1, -6, new(0, 1), -15);
        }



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




        else if (time == 188) // Whatever second the end is at
            EndGame();
    }
}