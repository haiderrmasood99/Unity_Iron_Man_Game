using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float life=100;
  
   
    public void damage(int dam)
    {
        life -= dam;
        
        if (life <= 0)
            gameObject.SetActive(false);
    }

}

