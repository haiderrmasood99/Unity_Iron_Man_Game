using System.Collections;
using UnityEngine;

public class shootarc : MonoBehaviour
{
    private float fireRate = 1.6f;
    private float nextFire;
    private WaitForSeconds shotDuration = new WaitForSeconds(.5f);
    private LineRenderer laserLine;
    private AudioSource laserAudio;
    Health a;
    public float laserRange = 100;
    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        laserAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
            RaycastHit hit;
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
        laserLine.enabled = true;
        if (a != null)
        {
            a.damage(100);
        }
        yield return shotDuration;
        laserLine.enabled = false;

    }
}
