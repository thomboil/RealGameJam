﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerMouvement : MonoBehaviour
{

    Vector3 direction;
    float rotation;
    Rigidbody rb;

    public float speedMouvement = 2;
    public float speedRotation = 2;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        spotlight = GameObject.Find("Player").GetComponentsInChildren<Light>().First(x => x.name == "Spot Light");
        strenghtLumiereIni = spotlight.range;
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector3(0, 0, Input.GetAxis("Vertical")).normalized;
        direction = new Vector3(0, 0, 1);
        rotation = Input.GetAxis("Horizontal");
        Touch();
    }

    void FixedUpdate()
    {
        //rb.AddForce(direction * speed);
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(direction) * speedMouvement * Time.deltaTime);
        transform.Rotate(0, rotation* speedRotation, 0);
    }

    private Vector2 touchOrigin;

    private void Touch()
    {
        if (Input.touchCount > 0)
        {
            //Store the first touch detected.
            Touch myTouch = Input.touches[0];

            //Check if the phase of that touch equals Began
            if (myTouch.phase == TouchPhase.Began)
            {
                //If so, set touchOrigin to the position of that touch
                touchOrigin = myTouch.position;
            }

            //If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
            else if (myTouch.phase == TouchPhase.Moved)
            {
                float x = myTouch.position.x - touchOrigin.x;

                x = x > 0 ? 1 : -1;
                rotation = x;

            }

            else if(myTouch.phase == TouchPhase.Stationary)
            {
                touchOrigin = myTouch.position;
            }
        }
    }

    Light spotlight;
    float strenghtLumiereIni;

    private void PasRapportMaisSpothlight()
    {
        if(lumineuxSpotlight && spotlight.range < strenghtLumiereIni)
        {
            spotlight.range += 0.4f;
        }
        else if(!lumineuxSpotlight)
        {
            spotlight.range -= 0.4f;

        }
    }

    bool lumineuxSpotlight = true;

    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.tag == "Planet")
        {

            lumineuxSpotlight = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Planet")
        {
            lumineuxSpotlight = true;
        }

    }
}
