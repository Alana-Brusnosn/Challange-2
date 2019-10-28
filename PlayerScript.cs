using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    private int scoreValue = 0;

    public Text winText;

    public Text LivesText;

    private int live;

    public Text loseText;

    private GameObject other;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        live = 3;
        score.text = scoreValue.ToString();

        winText.text = "";
        loseText.text = "";

        SetScoreText();

        SetLifeText();

    }


    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            musicSource.loop = true;
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetScoreText();
            Destroy(collision.collider.gameObject);

            if (scoreValue == 4)
            {
                transform.position = new Vector2(46.0f, 49.28f);
                live = 3;
                SetLifeText();
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            
            live = live - 1;
            collision.gameObject.SetActive(false);
            Destroy(collision.collider.gameObject);
            SetLifeText();
            
        }
      
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }

    }

   private void SetScoreText()
    {
        score.text = "Score:" + scoreValue.ToString();
      
       if (scoreValue >= 8)
        {
            winText.text = "You Win! Game Created by Alana Brunson";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = false;
        }
    }
     void SetLifeText()
    {
        LivesText.text = "Lives: " + live.ToString();
        if (live == 0)
        {
            loseText.text = " You Lost. ";
            Destroy(gameObject);
        }
    }
}

