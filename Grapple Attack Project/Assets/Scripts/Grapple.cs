﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    private Rigidbody2D Rb;
    //speed of the grap
    public float SpeedGrap;
    //position of grapple target
    private Vector3 Target = Vector3.zero;
    public GrapState grapState = GrapState.GrapIsReadyToBeLaunched;
    private GameObject Player;

    public enum GrapState
    {
        GrapIsReadyToBeLaunched,
        GrapIsLaunched,
        GrapIsAnchored,
        GrapIsReturned
    }

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Player = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //When the player click
        if (Input.GetMouseButtonDown(0))
        {
            if(grapState == GrapState.GrapIsReadyToBeLaunched)
            {
                grapState = GrapState.GrapIsLaunched;
                //get the position of the touch
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Target = ray.origin;
                Target.z = 0;
                //launch the grap to the touch position
                Rb.velocity = (Target - transform.position).normalized * SpeedGrap;
            }
            else
            {
                grapState = GrapState.GrapIsReturned;
            }
        }
        if(grapState == GrapState.GrapIsReadyToBeLaunched)
        {
            transform.position = GetPlayerPosition();
        }
        else if(grapState == GrapState.GrapIsReturned)
        {
            Rb.velocity = (GetPlayerPosition() - transform.position).normalized * SpeedGrap;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //If the grapple touch an object
        if (col.gameObject.tag == "Environment" && grapState == GrapState.GrapIsLaunched)
        {
            Rb.velocity = Vector3.zero;
            grapState = GrapState.GrapIsAnchored;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && grapState == GrapState.GrapIsReturned)
        {
            Rb.velocity = Vector3.zero;
            grapState = GrapState.GrapIsReadyToBeLaunched;
        }
    }

    private Vector3 GetPlayerPosition()
    {
        return Player.transform.position;
    }
}
