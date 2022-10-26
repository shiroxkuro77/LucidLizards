
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public float speed;

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
        if (transform.position.x > end.position.x) {
            transform.position = start.position;
        }
    }

}
