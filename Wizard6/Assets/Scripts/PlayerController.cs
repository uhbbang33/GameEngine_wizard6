using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public Rigidbody rb;

    public float speed;
    public int health;
    public int maxHealth;

    public int potion;
    public int maxPotion;

    public float mouseSensitivity = 1;

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

        if (potion > 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                --potion;
                health += 50;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Potion")
        {
            if (potion < maxPotion)
                ++potion;
            Destroy(other.gameObject);
        }
    }

    int num = 0;
    void OnTriggerStay(Collider other)
    {
        ++num;
        if (other.CompareTag("Monster"))
        {
            if (num % 50 == 1)
                health -= 50;
        }
    }
}
