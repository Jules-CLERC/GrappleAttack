using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //speed of the player
    public float Speed;
    private Rigidbody2D Rb;
    //Grap is a child object
    private GameObject Grap;

    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Grap = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetGrapState() == Grapple.GrapState.GrapIsAnchored)
        {
            Vector3 target = GetGrapPosition();
            Rb.AddForce((target - transform.position).normalized * Speed);
            Debug.DrawLine(transform.position, target, Color.green);
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
