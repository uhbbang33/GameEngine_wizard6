using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyOpenDoor : MonoBehaviour
{
    [SerializeField] public Animator myDoor = null;

    private void OnTriggerEnter(Collider other)
    {
        myDoor.Play("DoorOpen", 0, 0.0f);
        gameObject.SetActive(false);
    }
}
