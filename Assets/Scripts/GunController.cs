using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Shooting Settings")]
    public float shootRange = 100f;
    public float laserDuration = 0.05f;  

    [Header("References")]
    public LineRenderer laserLine;          
    public ParticleSystem muzzleFlash;     
    public AudioSource gunAudioSource;      
    public AudioClip shootSound;            

    [Header("Camera")]
    public Camera fpsCam;  
    private WaitForSeconds laserWait;

    void Start()
    {
        laserWait = new WaitForSeconds(laserDuration);

        laserLine.enabled = false;
        laserLine.startWidth = 0.01f;
        laserLine.endWidth = 0.01f;
    }

    void Update()
    {
        // Tembak saat klik kiri mouse
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();

        gunAudioSource.PlayOneShot(shootSound);

        Vector3 rayOrigin = fpsCam.transform.position;
        Vector3 rayDirection = fpsCam.transform.forward;

        laserLine.SetPosition(0, transform.TransformPoint(new Vector3(0, 0.3f, 0.9f)));

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, shootRange))
        {
            laserLine.SetPosition(1, hit.point);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.GetHit();
            }
        }
        else
        {
            laserLine.SetPosition(1, rayOrigin + rayDirection * shootRange);
        }

        StartCoroutine(ShowLaser());
    }

    IEnumerator ShowLaser()
    {
        laserLine.enabled = true;
        yield return laserWait;
        laserLine.enabled = false;
    }
}