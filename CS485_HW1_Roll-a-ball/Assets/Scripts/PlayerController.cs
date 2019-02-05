using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //For jump
    public float gravity;
    public float jumpHeight;

    public float speed;
    public Text countText;
    public Text winText;
    public Text enemyText;

    //For jump
    public LayerMask ground;
    private bool isGrounded;
    private AudioSource jumpSound;
    private AudioSource collectSound;

    private Vector3 fallingVelocity;
    private bool win;

    private Rigidbody rb;
    private int count;
    private int enemies;

    public int buttonWidth;
    public int buttonHeight;
    private int origin_x;
    private int origin_y;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        enemies = 0;
        SetCountText();
        winText.text = "";
        SetEnemyText();

        //For jump
        gravity = 9.8f;
        jumpHeight = 3.0f;
        fallingVelocity = Vector3.zero;
        jumpSound = GetComponent<AudioSource>();

        buttonWidth = 200;
        buttonHeight = 50;
        origin_x = Screen.width / 2 - buttonWidth / 2;
        origin_y = Screen.height / 2 - buttonHeight * 2;
    }

    //For jump
    void Update()
    {

        isGrounded = Physics.CheckSphere(rb.transform.position, 2f, ground, QueryTriggerInteraction.Ignore);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            Debug.Log("I jumped! isGrounded = " + isGrounded);
            jumpSound.Play();
        }
       
        Debug.Log("End of jump, isGrounded = " + isGrounded);

    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

    }

    void OnTriggerEnter(Collider other) 
    { 
        if(other.gameObject.CompareTag("Pick Up"))
        {
            collectSound = other.gameObject.GetComponent<AudioSource>();
            collectSound.Play();
            other.gameObject.SetActive(false);
            count++;
            SetCountText();

        }
        else if (other.gameObject.CompareTag("Enemy") && !isGrounded)
        {
            other.gameObject.SetActive(false);
            enemies++;
            SetEnemyText();
        }
    }

    void SetCountText()
    {
        countText.text = "Rings: " + count.ToString();
        if(count >= 12 && enemies >= 5)
        {
            winText.text = "You Win!";
            win = true;
        }
    }

    void SetEnemyText()
    {
        enemyText.text = "Enemies: " + enemies.ToString();
        if (count >= 12 && enemies >= 5)
        {
            winText.text = "You Win!";
            win = true;
        }
    }

    void OnGUI()
    {

        if (win)
        {
            if (GUI.Button(new Rect(origin_x, origin_y + 80, buttonWidth, buttonHeight), "PLAY AGAIN"))
            {
                Application.LoadLevel(1);
            }

            if (GUI.Button(new Rect(origin_x, origin_y + 150, buttonWidth, buttonHeight), "MAIN MENU"))
            {
                Application.LoadLevel(0);
            }
        }

    }
}