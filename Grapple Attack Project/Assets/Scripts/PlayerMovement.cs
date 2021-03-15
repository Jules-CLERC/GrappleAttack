using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //speed of the player
    public float Speed;
    //max speed of the player
    public float MaxSpeed = 10f;
    // less than 1 to decrease velocity
    public float decreaseSpeed = 0.9f;
    private Rigidbody2D Rb;
    //Grap is a child object
    private GameObject Grap;
    //skin is a child object
    private GameObject Skin;
    //Is the time of click
    private float StartTime;

    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Grap = transform.GetChild(0).gameObject;
        Skin = transform.GetChild(2).gameObject;
        StartTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetGrapState() == Grapple.GrapState.GrapIsAnchored)
        {
            Vector3 target = GetGrapPosition();
            if((transform.position - target).magnitude > 1f)
            {
                Rb.AddForce((target - transform.position).normalized * Speed);
                Debug.DrawLine(transform.position, target, Color.green);
            }
        }
        if(Rb.velocity.magnitude > MaxSpeed)
        {
            Rb.velocity = Rb.velocity * decreaseSpeed;
        }

        if(Input.GetMouseButtonDown(0))
        {
            StartTime = Time.time;
        }
        if (Input.GetMouseButtonUp(0))
        {
            StartTime = 0f;
        }
        if(StartTime != 0f && Time.time - StartTime > 0.5f)
        {
            if (Rb.velocity.magnitude < MaxSpeed)
            {
                Rb.velocity = Rb.velocity * 1.1f;
            }
        }
    }

    private Vector3 GetGrapPosition()
    {
        return Grap.transform.position;
    }

    private Grapple.GrapState GetGrapState()
    {
        return Grap.GetComponent<Grapple>().grapState;
    }
}
