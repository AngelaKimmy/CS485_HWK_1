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

    //For jump
    public Transform[] feet = new Transform [6];
    public LayerMask ground;
    public Transform foot;
    private bool isGrounded;
    private AudioSource jumpSound;
    private AudioSource collectSound;

    private Vector3 fallingVelocity;

    private Rigidbody rb;
    private int count;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";

        //For jump
        gravity = 9.8f;
        jumpHeight = 3.0f;
        fallingVelocity = Vector3.zero;
    }

    //For jump
    void Update()
    {
        //isGrounded = Physics.CheckSphere(foot.position, 0.1f, ground, QueryTriggerInteraction.Ignore);
        /*
        for(int i =0; i < 6; i++)
        {
            if(Physics.CheckSphere(feet[i].position, 0.1f, ground, QueryTriggerInteraction.Ignore))
            {
                isGrounded = true;
                break;
            }
            else
            {
                isGrounded = false;
            }
        }*/

        // isGrounded = Physics.CheckSphere(transform.position, 0.1f, ground, QueryTriggerInteraction.Ignore);
        isGrounded = Physics.CheckSphere(rb.transform.position, 0.8f, ground, QueryTriggerInteraction.Ignore);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            Debug.Log("I jumped! isGrounded = " + isGrounded);
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
            other.gameObject.SetActive(false);
            collectSound.Play();
            count++;
            SetCountText();

        }
/*        else if (other.gameObject.CompareTag("Enemy") && isGrounded)
        {
            other.gameObject.SetActive(false);
        }*/
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 12)
        {
            winText.text = "You Win!";
        }
    }

}