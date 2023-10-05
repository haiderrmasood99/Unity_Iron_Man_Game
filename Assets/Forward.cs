using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forward : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        x();
        transform.Translate(new Vector3( Camera.main.transform.forward.x,0,Camera.main.transform.forward.z) * Time.deltaTime);
    }
    IEnumerator x()
    {
        WaitForSeconds f = new WaitForSeconds(10);
        yield return f;
    }
}
