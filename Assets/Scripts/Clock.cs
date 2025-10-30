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

    //[Serializable] class Test { public T bulletType; public float xPos; public float yPos; public Vector2 direction; public float blueAngle; }
    //[SerializeField] private List<Test> tests = new();
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //        foreach (Test test in tests)
    //            SpawnBullet(test.bulletType, test.xPos, test.yPos, test.direction, test.blueAngle);
    //}

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
        if (readyToContinue && Input.GetButtonDown("Reverse"))
            SceneManager.LoadScene(0);
    }



    private void Start()
    {
        musicSource.time = startTime;
        time = startTime;
        Invoke(nameof(StartMusic), 4);
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
            SpawnBullet(T.yellow, -8, -3, new(1, .25f));
        }




        if (time == 1000)
        {
            //Blue Left Ring:
            //SpawnBullet(T.blue, -5, 5, new(1, -1), -45);
            //SpawnBullet(T.blue, -5, -5, new(1, 1), 45);

            //Blue Meet
            //SpawnBullet(T.blue, -6, 1, new(1, 0), -15);
            //SpawnBullet(T.blue, -6, -1, new(1, 0), 15);
            //SpawnBullet(T.blue, 6, -1, new(-1, 0), -15);
            //SpawnBullet(T.blue, 6, 1, new(-1, 0), 15);

            //Blue Slope
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
            SpawnBullet(T.yellow, -8, -3, new(1, .25f));
            SpawnBullet(T.yellow, 8, -3, new(-1, .25f));

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
        }




        else if (time == 1000) // Whatever second the end is at
            EndGame();
    }
}