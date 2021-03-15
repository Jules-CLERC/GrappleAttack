using System.Collections;
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
    //object grap by the grapple
    private GameObject ObjectGrapped = null;
    //Position of the grapple in the ObjectGrapped
    private Vector3 GrapPoint = Vector3.zero;
    //Is the time of click
    private float StartTime, EndTime;

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

        StartTime = 0f;
        EndTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //When the player click
        if (Input.GetMouseButtonDown(0))
        {
            StartTime = Time.time;
        }
        if (Input.GetMouseButtonUp(0))
        {
            EndTime = Time.time;
        }
        //if it's short click
        if (StartTime != 0f && EndTime != 0f && EndTime - StartTime <= 0.5f)
        {
            Debug.Log("short");
            StartTime = 0f;
            EndTime = 0f;

            if (grapState == GrapState.GrapIsReadyToBeLaunched)
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
                GrapPoint = Vector3.zero;
                ObjectGrapped = null;
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
        else if(grapState == GrapState.GrapIsAnchored)
        {
            transform.position = ObjectGrapped.transform.position - GrapPoint;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //If the grapple touch an object
        if (col.gameObject.tag == "Environment" && grapState == GrapState.GrapIsLaunched)
        {
            Rb.velocity = Vector3.zero;
            ObjectGrapped = col.gameObject;
            GrapPoint = ObjectGrapped.transform.position - transform.position;
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
