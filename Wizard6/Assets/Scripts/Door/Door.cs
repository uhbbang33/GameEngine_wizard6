using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] public Animator myDoor = null;
    [SerializeField] public Animator pairDoor = null;
    
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    [SerializeField] private GameObject pairTrigger;

    [SerializeField] private InstantiateMonster createMonster;

    public bool monsterInit;
    public AudioClip clip;
    
    private void Start()
    {
        monsterInit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                SoundManager.instance.SFXPlay("door", clip);
                myDoor.Play("DoorOpen", 0, 0.0f);
                gameObject.SetActive(false);
                
                pairTrigger.SetActive(false);
            }
            else if (closeTrigger)
            {
                monsterInit = true;
                myDoor.Play("DoorClose", 0, 0.0f);
                gameObject.SetActive(false);
                
                pairTrigger.SetActive(false);

                createMonster.CreateMonster();
            }
        }
    }
}
