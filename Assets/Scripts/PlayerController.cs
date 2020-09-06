using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject timer;
    public int totalPickups = 8;
    public float jumpForce = 1;

    private bool onFloor;
    private Rigidbody rb;
    private int count;
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

        if (count == totalPickups)
        {
            timer.SendMessage("StopCountDown");
            winTextObject.SetActive(true);
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
            count++;
            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Floor")) {
            onFloor = true;
        }
    }

    public int GetCount() {
        return count;
    }
}
