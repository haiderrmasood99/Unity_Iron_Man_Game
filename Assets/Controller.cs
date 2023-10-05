using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    float horMov, vertMov;
    private Vector3 myMove;
    private float movSpeed = .9f;
    public Animator myAnimator;
    private Quaternion newDirection;
    private float rotSpeed = 3;
    Rigidbody rb;
    private float jumpForce = .2f;
    private bool isGrounded = true;
    private float runSpeed = 5;
    private float flySpeed = 3;
    public GameObject explosion;
    public GameObject exploclone;
    public static Controller controller;
    Health a;
    public static float nextAction;
    RaycastHit hit;
    //public ParticleSystem particleEffect;

    
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        controller = this;
        nextAction = Time.time;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
      

        if (Time.time>nextAction)
        {
            Vector3 a = Camera.main.transform.forward;
            float x = Input.GetAxis("Vertical");
            float y = Input.GetAxis("Horizontal");
            Vector3 myMove = new Vector3(a.x * x + a.z * y, 0, a.z * x - a.x * y);
            if (myMove.magnitude > 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (isGrounded)
                    {
                        myAnimator.SetBool("isrunning", true);
                        transform.position += myMove * Time.deltaTime * runSpeed;
                        GameManager.gminstance.run();
                    }
                    else
                    {
                        transform.position += myMove * Time.deltaTime * runSpeed * flySpeed;
                        GameManager.gminstance.fly();
                        if (myAnimator.GetBool("isflying"))
                            myAnimator.SetBool("isrunning", true);
                        else
                            myAnimator.SetBool("isrunning", false);
                    }
                }

                else
                {
                    if (isGrounded)
                    {
                        transform.position += myMove * Time.deltaTime * movSpeed;
                        myAnimator.SetBool("isrunning", false);
                        GameManager.gminstance.walk();
                    }
                    else
                    {
                        transform.position += myMove * Time.deltaTime * movSpeed * flySpeed / 1.5f;
                        myAnimator.SetBool("isrunning", false);
                        GameManager.gminstance.run();
                    }
                }
            }

            if (myMove.magnitude > 0)
            {
                newDirection = Quaternion.LookRotation(myMove);
                transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * rotSpeed);
            }
            myAnimator.SetFloat("speed", myMove.magnitude);
            CheckJump();

        }
        action();
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isGrounded)
            {
                if (myAnimator.GetBool("isflying"))
                {
                    myAnimator.SetBool("isflying", false);
                    myAnimator.SetBool("isfalling", false);
                }
                else
                {
                    myAnimator.SetBool("isflying", true);
                }
            }
            else
            {
                myAnimator.SetBool("isfalling", true);
                myAnimator.SetBool("isflying", false);
            }
        }
        if (myAnimator.GetBool("isflying") && Input.GetKey(KeyCode.Space))
        {
       
            transform.Translate(Vector3.up * Time.deltaTime);
        }
        if (myAnimator.GetBool("isflying") && Input.GetKey(KeyCode.LeftControl))
        {

            transform.Translate(Vector3.down * Time.deltaTime);
        }


    }
    private void action()
    {
        if (Input.GetKey(KeyCode.LeftAlt) && Time.time > nextAction)
        {
            nextAction = Time.time + 1.8f;

            myAnimator.SetBool("shield", true);
            transform.forward = new Vector3(Camera.main.transform.forward.x,0,Camera.main.transform.forward.z);

        }
        else if (Input.GetKeyDown(KeyCode.Q) && Time.time > nextAction)
        {
            transform.forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
            nextAction = Time.time + 1.8f;
            
            Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
            RaycastHit hit;
            
            if (Physics.Raycast(rayOrigin, Camera.main.transform.forward, out hit))
            {
               if(hit.point != null)
                {
                    a = hit.collider.GetComponent<Health>();
                    Instantiate(explosion, hit.point, Quaternion.identity);
                    a.damage(100);
                }

            }
            myAnimator.SetBool("armrocket", true);
           
        }

        else if (Input.GetKeyDown(KeyCode.V) && Time.time > nextAction)
        {
            transform.forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
            nextAction = Time.time + 1.8f;

            myAnimator.SetBool("kick", true);
        }

        else if (Input.GetKeyDown(KeyCode.E) && Time.time > nextAction)
        {
            transform.forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
            nextAction = Time.time + 1.8f;
            GameManager.gminstance.unibeam();
            myAnimator.SetBool("unibeam", true);
        }

        else if (Input.GetButtonDown("Fire2") && Time.time > nextAction)
        {
            transform.forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
            nextAction = Time.time + 1.8f;

            myAnimator.SetBool("rightrepulsor", true);
        }

        else if (Input.GetButtonDown("Fire1")&&Time.time>nextAction)
        {
            transform.forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
            nextAction =Time.time+1.8f;
            myAnimator.SetBool("leftrepulsor", true);
        }

        else if (Input.GetKeyDown(KeyCode.X) && Time.time > nextAction)
        {
            transform.forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
            nextAction = Time.time + 1.2f;

            myAnimator.SetBool("shoulderrocket", true);
        }

        else if (Input.GetKeyDown(KeyCode.C) && Time.time > nextAction)
        {
            transform.forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
            nextAction = Time.time + 1.8f;


            myAnimator.SetBool("punch", true);
        }
        else
        {
            myAnimator.SetBool("punch", false);
            myAnimator.SetBool("shield", false);
            myAnimator.SetBool("armrocket", false);
            myAnimator.SetBool("shoulderrocket", false);
            myAnimator.SetBool("leftrepulsor", false);
            myAnimator.SetBool("rightrepulsor", false);
            myAnimator.SetBool("unibeam", false);
            myAnimator.SetBool("kick", false);
        }



    }
    private void FixedUpdate()
    {
        if (myAnimator.GetBool("isflying"))
            rb.AddRelativeForce(Vector3.up * (rb.mass * Mathf.Abs(Physics.gravity.y)));
    }
    private void CheckJump()
    {

        if (Input.GetKey(KeyCode.Space) && (myAnimator.GetBool("isflying") == false))
        {


            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            myAnimator.SetBool("jumpup", true);
            myAnimator.SetBool("jumpland", false);

        }

    }
    void OnCollisionEnter(Collision theCollision)
    {
        if (theCollision.gameObject.tag == "floor")
        {
            isGrounded = true;
            myAnimator.SetBool("jumpland", true);
            myAnimator.SetBool("jumpup", false);

        }

    }

    void OnCollisionExit(Collision theCollision)
    {

        if (theCollision.gameObject.tag == "floor")
        {
            isGrounded = false;
            myAnimator.SetBool("jumpland", false);
        }
    }
    
}
