using System.Collections;
using UnityEngine;

public class shootleftrep : MonoBehaviour
{
    private float fireRate = 1.6f;
    private float nextFire;
    private WaitForSeconds shotDuration = new WaitForSeconds(.5f);
    private LineRenderer laserLine;
    private AudioSource laserAudio; RaycastHit hit;

    public float laserRange = 100;
    Health a;
    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        laserAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && !Controller.controller.myAnimator.GetBool("isflying"))        {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
            
            laserLine.SetPosition(0, transform.position);
            if (Physics.Raycast(rayOrigin, Camera.main.transform.forward, out hit, laserRange))
            {
                laserLine.SetPosition(1, hit.point);
                a = hit.collider.GetComponent<Health>();

            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + Camera.main.transform.forward * laserRange);
            }
            
        }
    }
    private IEnumerator ShotEffect()
    {
        WaitForSeconds laserTime = new WaitForSeconds(1);
        laserAudio.Play();
        yield return laserTime;
        
        if(a!=null)
        {
            a.damage(50);
        }
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;

    }
}
