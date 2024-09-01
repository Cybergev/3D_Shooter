using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject projectilePref;
    [SerializeField] private int magSize;
    [SerializeField] private float fireRate;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private Sound shootSFX;

    [SerializeField] private Camera mainCam;

    private Timer refireTimer;

    private void FixedUpdate()
    {
        refireTimer ??= new Timer(fireRate);
        if (!refireTimer.IsFinished)
            refireTimer.RemoveTime(Time.deltaTime);
    }

    public void Shoot()
    {
        if (!projectilePref || !refireTimer.IsFinished)
            return;
        Instantiate(projectilePref, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>();
        muzzleFlash.Play();
        shootSFX.Play();
        refireTimer = new Timer(fireRate);
    }

}