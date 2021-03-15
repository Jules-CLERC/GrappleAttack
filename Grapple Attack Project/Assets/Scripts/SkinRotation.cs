using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinRotation : MonoBehaviour
{
    //diff between the image player and the real rotation
    public float diffAngle = 0f;
    private Rigidbody2D RbPlayer;

    // Start is called before the first frame update
    void Start()
    {
        RbPlayer = transform.parent.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSpaceShipRotation();
    }

    private void ChangeSpaceShipRotation()
    {
        float angle = Vector2.Angle(RbPlayer.velocity.normalized, Vector2.right);
        if (RbPlayer.velocity.normalized.y < 0f)
        {
            angle = -angle;
        }
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, angle + diffAngle);
    }
}
