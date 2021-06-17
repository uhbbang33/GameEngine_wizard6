using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyUpDoor : MonoBehaviour
{
    [SerializeField] public Animator myDoor = null;
    public AudioClip clip;
    
    private void OnTriggerEnter(Collider other)
    {
        SoundManager.instance.SFXPlay("door", clip);
        myDoor.Play("DoorUp", 0, 0.0f);
        gameObject.SetActive(false);
    }
}
