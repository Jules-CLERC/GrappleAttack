using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public float G = 6f;

    public static List<Attractor> Attractors;

    public Rigidbody2D rb = null;
    public string Tag;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Tag = transform.tag;
    }

    private void FixedUpdate()
    {
        foreach (var attractor in Attractors)
        {
            if (attractor != this)
            {
                Attract(attractor);
            }
        }
    }

    private void OnEnable()
    {
        if (Attractors == null)
        {
            Attractors = new List<Attractor>();
        }
        Attractors.Add(this);
    }

    private void OnDisable()
    {
        Attractors.Remove(this);
    }

    void Attract (Attractor objToAttract)
    {
        Rigidbody2D rbToAttract = objToAttract.rb;

        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;

        if (distance == 0f)
        {
            return;
        } 

        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        if(Tag == "Environment" && objToAttract.Tag == "Environment")
        {
            force = force / 100f;
        }

        rbToAttract.AddForce(force);
    }
}
