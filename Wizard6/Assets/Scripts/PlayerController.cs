using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public Rigidbody rb;
    public Slider HealthBar;
    public Text PotionText;

    public float speed;
    public float mouseSensitivity = 1;
    public int maxPotion = 5;
    
    private int Playerhealth = 100;
    private int maxHealth = 100;
    private int potionCnt = 0;

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

        //포션 사용
        if (potionCnt > 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                --potionCnt;
                Playerhealth += 10;
            }
        }
        
        //UI출력
        HealthBar.value = Playerhealth;
        PotionText.text = "" + potionCnt;
    }

    //포션 획득
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Potion")
        {
            if (potionCnt < maxPotion)
            {
                ++potionCnt;
                Destroy(other.gameObject);
            }
        }
    }

    //몬스터 충돌
    int num = 0;
    void OnTriggerStay(Collider other)
    {
        ++num;
        if (other.CompareTag("Monster"))
        {
            if (num % 50 == 1)
            {
                Playerhealth -= 5;
            }
        }
    }
}
