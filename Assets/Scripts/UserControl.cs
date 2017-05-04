using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
public class UserControl : MonoBehaviour
{
    public Text countText;
    public Text winText;

    private Player character;
    private bool jump;
    private bool crouch;
    private int count;

    // Use this for initialization
    void Awake()
    {
        character = GetComponent<Player>();
        SetCountText();
        winText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (jump == false)
        {
            //Read jump input in Update so that button
            // Presses arent missed
            jump = Input.GetButtonDown("Jump");
        }
    }

    void FixedUpdate()
    {
        // Read input
        crouch = Input.GetKey(KeyCode.LeftControl);
        float h = Input.GetAxis("Horizontal");
        // Pass all parameters to the character move
        character.Move(h, crouch, jump);
        jump = false;
    }

    //2D collider for the Pick up
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();

        }

    }

    //Text to tell you how many power ups collect and itf you hit target amount
    void SetCountText()
    {        
        countText.text = "Count: " + count.ToString();
        if (count >= 13)
        {
            winText.text = "You Win";
        }
    }

}
