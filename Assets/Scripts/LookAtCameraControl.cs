using UnityEngine;
using System.Collections;

public class LookAtCameraControl : MonoBehaviour
{
    public Transform TargetBody;

    /// <summary>
    /// Distance between camera and target body
    /// </summary>
    public float Distance;

    public float ZoomSpeed = 1;

    public float RotateSpeed = 1;

    // Use this for initialization
    void Start()
    {
        LookAtTarget();
    }

    void FixedUpdate()
    {
        bool lmbDown = Input.GetMouseButton(0);
        bool rmbDown = Input.GetMouseButton(1);
        bool mmbDown = Input.GetMouseButton(2);

        if (lmbDown)
        {
            float mX = Input.GetAxis("Mouse X");
            float mY = Input.GetAxis("Mouse Y");

            RotateAroundTarget(mX * RotateSpeed, -mY * RotateSpeed);
        }

        var wheel = Input.GetAxis("Mouse ScrollWheel");
        if (wheel != 0)
            Distance += wheel * ZoomSpeed;
    }

    void LateUpdate()
    {
        FollowTarget();
        //LookAtTarget();
    }

    private void RotateAroundTarget(float angleX, float angleY)
    {
        var rotation = Quaternion.AngleAxis(angleX, transform.up) * Quaternion.AngleAxis(angleY, transform.right);

        transform.rotation *= rotation;

        transform.position = TargetBody.position + rotation * (transform.position - TargetBody.position);
    }

    private void FollowTarget()
    {
        transform.position = TargetBody.position - transform.forward.normalized * Distance;
    }

    private void LookAtTarget()
    {
        transform.LookAt(TargetBody);
        FollowTarget();
    }
}
