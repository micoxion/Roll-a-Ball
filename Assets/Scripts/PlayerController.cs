using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject timer;
    public GameObject restartButton;
    public GameObject pickUpParent;
    public GameObject dustPuff;
    //change this to 1 or more in the inspector to override the score needed to win
    public int scoreOverride;
    public float jumpForce = 1;
    public float speed = 0;

    private Rigidbody rb;
    private bool onFloor;
    private int count;
    private int scoreToWin;
    private float movementX;
    private float movementY;

    public void testMethod()
    {
        Debug.Log("testMethod");
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        count = 0;
        SetCountText();

        winTextObject.SetActive(false);

        PickUpData[] pickUps = pickUpParent.GetComponentsInChildren<PickUpData>();

        foreach (PickUpData data in pickUps) {
            scoreToWin += data.pointValue;
        }

        if (scoreOverride > 0) {
            scoreToWin = scoreOverride;
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump() {
        if (onFloor) {
            rb.AddForce(new Vector3(0, jumpForce, 0));
            onFloor = false;
        }
    }

    void OnRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SetCountText()
    {
        countText.text = "Count: " + count;

        if (count == scoreToWin)
        {
            timer.SendMessage("StopCountDown");
            winTextObject.SetActive(true);
            restartButton.SetActive(true);  
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);

        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += other.gameObject.GetComponent<PickUpData>().pointValue;
            SetCountText();
        } else if (other.gameObject.CompareTag("Kill Plain")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Floor")) {
            GameObject puff = Instantiate(dustPuff);
            puff.transform.position = new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z);
            puff.GetComponent<ParticleSystem>().Play();
            onFloor = true;
        }
    }

    public int GetCount() {
        return count;
    }
}
