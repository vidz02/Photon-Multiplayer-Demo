using UnityEngine;
using Photon.Pun;

public class CameraFollow : MonoBehaviourPun
{
    public Transform target; // Player
    public Vector3 offset = new Vector3(0, 3, -7);
    public float smoothSpeed = 5f;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        if (!photonView.IsMine)
        {
            // Disable this script for remote players
            this.enabled = false;
            return;
        }

        // Move camera on spawn
        if (cam != null)
        {
            cam.transform.position = target.position + offset;
            cam.transform.LookAt(target);
        }
    }

    void LateUpdate()
    {
        if (cam == null || target == null) return;

        Vector3 desiredPos = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(cam.transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        cam.transform.position = smoothedPos;

        cam.transform.LookAt(target);
    }
}
