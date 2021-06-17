using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Shoot : MonoBehaviour
{
    public Camera cam;
    public GameObject projectile;
    public Transform FirePos;
    public float projectileSpeed = 10;
    public float coolTime = 4;

    public AudioClip clip;

    private Vector3 destination;
    private float timeToFire;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / coolTime;
            ShootProjectile();
            SoundManager.instance.SFXPlay("fire", clip);
        }
    }

    void ShootProjectile()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //LayerMask monsterLayerMask = 1 << LayerMask.NameToLayer("Monster");
        //LayerMask doorLayerMask = 1 << LayerMask.NameToLayer("Door");

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }
        InstantiateProjectile();
    }

    void InstantiateProjectile()
    {
        var projectileObj = Instantiate(projectile, FirePos.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity =
            (destination - FirePos.position).normalized * projectileSpeed;
        projectile.tag = "Bullet";
    }
}