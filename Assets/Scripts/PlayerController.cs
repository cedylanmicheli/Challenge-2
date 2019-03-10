using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject theCamera;
    private Rigidbody2D rb2d;
    public float speed;
    public float jumpforce;

    Animator anim;

    private int count;
    public Text countText;
    public Text winText;
    public Text livesText;
    private int lives;
    private int flip;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        SetCountText();
        winText.text = "";
        lives = 3;
        SetLivesText();
        flip = 0;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 1);
            transform.localEulerAngles = new Vector3(0, 360, 0);
            flip = 1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 1);
            transform.localEulerAngles = new Vector3(0, 180, 0);
            flip = 1;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 0);
            flip = 0;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetInteger("State", 3);
            flip = 3;
        }




        if (Input.GetKey("escape"))
            Application.Quit();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();

            if (count == 4)
            {
                transform.position = new Vector3((7.83f + 14.88f), 0f, 0f);
                theCamera.transform.position = new Vector3((7.83f + 14.88f), -1f, -10f);
                lives = 3;
                SetLivesText();
            }
        }
        else if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.SetActive(false);
                lives--;
                SetLivesText();
            }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground" || collision.collider.tag == "Platform")
        {
            if(flip ==3) anim.SetInteger("State", 0);
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
                anim.SetInteger("State", 3);
            }
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 8)
        {
            winText.text = "You Win!";
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives == 0)
        {
            this.gameObject.SetActive(false);
            winText.text = "Game Over";
        }
    }
}


