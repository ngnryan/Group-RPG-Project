using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;     // MC
    public Vector3 offset;       // distance between camera and MC
    public float smoothTime = 0.2f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // smooth follow mechanic
        Vector3 targetPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
