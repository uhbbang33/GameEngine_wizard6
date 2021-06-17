using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateMonster : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private GameObject effect;

    [SerializeField] private Transform monsterTransform;
    [SerializeField] private Transform monsterTransform2;
    [SerializeField] private Transform monsterTransform3;
    [SerializeField] private Transform monsterTransform4;

    bool isCreateMonster;
    bool isDoorOpen;

    GameObject[] monster;

    [SerializeField] private GameObject doorTrigger;
    Door door;

    private void Start()
    {
        door = doorTrigger.GetComponent<Door>();

        monster = new GameObject[4];

        isCreateMonster = false;
        isDoorOpen = false;
    }

    private void Update()
    {
        if (isCreateMonster == true)
        {
            if (!monster[0] && !monster[1] && !monster[2] && !monster[3]
                && !isDoorOpen)
            {
                door.myDoor.Play("DoorOpen", 0, 0.0f);
                door.pairDoor.Play("DoorOpen", 0, 0.0f);

                isDoorOpen = true;
            }
        }
    }

    public void CreateMonster()
    {
        monster[0] = Instantiate(monsterPrefab, monsterTransform.position, monsterTransform.rotation);
        monster[1] = Instantiate(monsterPrefab, monsterTransform2.position, monsterTransform2.rotation);
        monster[2] = Instantiate(monsterPrefab, monsterTransform3.position, monsterTransform3.rotation);
        monster[3] = Instantiate(monsterPrefab, monsterTransform4.position, monsterTransform4.rotation);

        isCreateMonster = true;

        GameObject effect1 = Instantiate(effect, monsterTransform.position, monsterTransform.rotation);
        GameObject effect2 = Instantiate(effect, monsterTransform2.position, monsterTransform2.rotation);
        GameObject effect3 = Instantiate(effect, monsterTransform3.position, monsterTransform3.rotation);
        GameObject effect4 = Instantiate(effect, monsterTransform4.position, monsterTransform4.rotation);

        Destroy(effect1, 2f);
        Destroy(effect2, 2f);
        Destroy(effect3, 2f);
        Destroy(effect4, 2f);
    }

}
