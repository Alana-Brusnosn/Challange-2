﻿using System.Collections;
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

    private int live = 3;

    public Text loseText;

    private GameObject other;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();

        score.text = scoreValue.ToString();

        winText.text = "";

        SetScoreText();

        SetLifeText();

    }


    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
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
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            live = live - 1;
            SetLifeText();
            Destroy(collision.collider.gameObject);
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
        if (scoreValue >= 4)
        {
            winText.text = "You Win! Game Created by Alana Brunson";
        }
    }
     void SetLifeText()
    {
        LivesText.text = "Lives: " + live.ToString();
        if (live < 1)
        {
            loseText.text = " You Lost. ";
            Destroy(gameObject);
        }
    }
}

