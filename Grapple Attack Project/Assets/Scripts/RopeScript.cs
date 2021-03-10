using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    private LineRenderer LineRend;
    private GameObject Player;
    private GameObject Grap;

    // Start is called before the first frame update
    void Start()
    {
        LineRend = GetComponent<LineRenderer>();
        Player = transform.parent.gameObject;
        Grap = Player.transform.GetChild(0).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 sp = GetPlayerPosition();
        Vector3 ep = GetGrapPosition();
        LineRend.SetPosition(0, sp);
        LineRend.SetPosition(1, ep);
    }

    private Vector3 GetPlayerPosition()
    {
        return Player.transform.position;
    }

    private Vector3 GetGrapPosition()
    {
        return Grap.transform.position;
    }
}
