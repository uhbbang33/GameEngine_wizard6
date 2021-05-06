using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public Rigidbody rb;

    public float mouseSensitivity = 1;
    public float speed = 10;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //camera rotation
        var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.eulerAngles = transform.eulerAngles + new Vector3(0, mouseX, 0);

        cam.transform.eulerAngles = cam.transform.eulerAngles + new Vector3(-mouseY, 0, 0);

        //player movement
        var keyboardX = Input.GetAxis("Horizontal");
        var keyboardY = Input.GetAxis("Vertical");

        rb.velocity = (transform.forward * (keyboardY * speed)) + 
                      (transform.right * (keyboardX * speed));
    }
}
