using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] Transform bossTransform;
    [SerializeField] GameObject effect;

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(boss, bossTransform.position, bossTransform.rotation);
        
        GameObject effect1 = Instantiate(effect, bossTransform.position, bossTransform.rotation);
        Destroy(effect1, 2f);

        gameObject.SetActive(false);
    }
}
