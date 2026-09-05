using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 1.2f, 0);

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.rotation = target.rotation;
        }
    }
}