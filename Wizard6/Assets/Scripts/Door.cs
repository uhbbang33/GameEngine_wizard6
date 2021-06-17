using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] public Animator myDoor = null;
    
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    [SerializeField] private GameObject pairTrigger;
    Animator pairAnimator;

    InstantiateMonster createMonster;

    public bool monsterInit;

    private void Start()
    {
        if (closeTrigger)
        {
            createMonster = GetComponent<InstantiateMonster>();
        }

        monsterInit = false;

        pairAnimator = pairTrigger.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                myDoor.Play("DoorOpen", 0, 0.0f);
                gameObject.SetActive(false);
                pairTrigger.SetActive(true);
            }
            else if (closeTrigger)
            {
                monsterInit = true;
                myDoor.Play("DoorClose", 0, 0.0f);
                gameObject.SetActive(false);
                pairTrigger.SetActive(true);

                createMonster.CreateMonster();
            }
        }
    }
}
