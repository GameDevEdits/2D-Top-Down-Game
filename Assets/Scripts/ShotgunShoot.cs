using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShoot : MonoBehaviour
{
    public Animator aimAnimator;
    public float fireRate = 0.5f; // Adjust this value for the desired fire rate

    private bool canShoot = true;

    private void Awake()
    {
        aimAnimator = aimAnimator.GetComponent<Animator>();
    }

    private void Update()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            aimAnimator.SetTrigger("Shoot");
            StartCoroutine(FireRateCooldown());

        }
    }
    IEnumerator FireRateCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(1f / fireRate);
        canShoot = true;
    }
}
