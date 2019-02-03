using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //For menu***********
    public int buttonWidth;
    public int buttonHeight;
    private int origin_x;
    private int origin_y;
    //*******************

    public GameObject player;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        //For menu***********
        buttonWidth = 100;
        buttonHeight = 25;
        //origin_x = Screen.width / 2 - buttonWidth / 2;
        origin_x = Screen.width - Screen.width/8;
        origin_y = 15;
        //*******************
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }

    //For menu***********
    void OnGUI()
    {
        if (GUI.Button(new Rect(origin_x, origin_y, buttonWidth, buttonHeight), "Back to Menu"))
        {
            Application.LoadLevel(0);
        }

    }
    //*******************
}
