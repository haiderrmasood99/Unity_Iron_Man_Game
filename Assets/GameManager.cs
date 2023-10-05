using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Slider health;
    public static GameManager gminstance;
    void Start()
    {
        gminstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(health.value<=0)
        {
            
        }
    }
    public void walk()
    {
        health.value -= .01f;
    }
    public void run()
    {
        health.value -= .05f;
    }
    public void fly()
    {
        health.value -= .1f;
    }
    public void shoot()
    {
        health.value -= .2f;
    }
    public void fastfly()
    {
        health.value -= .2f;
    }
    public void unibeam()
    {
        health.value -= 100;   
    }
    public void kick()
    {
        health.value -= .2f;
    }
    public void punch()
    {
        health.value -=.2f;
    }

}
