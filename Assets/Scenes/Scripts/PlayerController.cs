using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.UI.Scrollbar;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    
    [SerializeField]
    private float jumpForce = 10f;

    [SerializeField]
    private float sideForce = 20f;
    private Vector3 direction;
    public bool keyrightPressed;
    private bool keyleftPressed;
    private bool jumpingKeyheld;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        HandleInputs();
        HandleJumping();
    }

    private void HandleJumping()
    {
        if (jumpingKeyheld) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpingKeyheld = true;
        } else {
            jumpingKeyheld = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            direction = Vector3.right;
        }

        rb.AddForce(direction * sideForce, ForceMode.Acceleration);
    }
}
