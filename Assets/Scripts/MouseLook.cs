using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float minAngle;
    public float maxAngle;

    private Transform _playerBody;
    private Transform _frisbeePosition;

    private float mouseX;
    private float mouseY;
    
    private float _yRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        _frisbeePosition = this.transform.parent.Find("FrisbeePositionContainer").transform;
        _playerBody = this.transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        _yRotation -= mouseY;
        _yRotation = Mathf.Clamp(_yRotation, minAngle, maxAngle);
        
        transform.localRotation = Quaternion.Euler(_yRotation, 0f, 0f);
        _frisbeePosition.localRotation = Quaternion.Euler(_yRotation, 0f, 0f);
        _playerBody.Rotate(Vector3.up*mouseX);
    }

    public float YRotation
    {
        get => _yRotation;
        set => _yRotation = value;
    }

    public float MouseX => mouseX;
    public float MouseY => mouseY;
}
