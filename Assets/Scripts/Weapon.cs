using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int magSize;
    [SerializeField] private float range;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private AudioClip shootSFX;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private Camera mainCam;

    public Action Fire
    {
        get
        {
            return () =>
            {

            };
        }
    }
    public void Shoot()
    {
    }
}
