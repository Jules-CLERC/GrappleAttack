using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    public Vector3 offset;
    public float smoothTime = 0.5F;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        if(player != null)
        {
            Vector3 targetPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        else
        {
            GetPlayer();
        }
    }

    void GetPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
}