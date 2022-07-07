using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;

    public float speed;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public float cameraRotation;

    private Vector3 _velocity;

    private Transform _groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Camera _camera;

    private bool _isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        ApplyGravity();
        MoveInput();
    }

    void MoveInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        /*if (x != 0)
        {
            float useX = x;
            Vector3 v = _camera.transform.rotation.eulerAngles;
            _camera.transform.rotation = Quaternion.Euler(v.x, v.y, -cameraRotation * x);
        }

            if((v.z < 0 && x > 0) || (v.z > 0 && x < 0))
            {
                useX = Mathf.Lerp(x, 0, Time.deltaTime * 5);
            }
            _camera.transform.rotation = Quaternion.Euler(v.x, v.y, -cameraRotation * useX);
        }
        */
        
        Vector3 move = transform.right * x + transform.forward * z;

        _controller.Move(move * (speed * Time.deltaTime));

    }

    void ApplyGravity()
    {
        if (_isGrounded && _velocity.y < 0)
        {
            //weird thing, brackeys said so, might need to check that
            _velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        _velocity.y += gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }

    void CheckGrounded()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, groundDistance, groundMask);
    }

}
