using UnityEngine;
using System.Collections;
using Phys;

/// <summary>
/// Тело, которое не действует гравитацией на другие тела.
/// </summary>
public class ActorBody : Body
{
    public Vector3 Forward;
    public Vector3 Up;
    public Vector3 Right;

    [SerializeField]
    private Vector3d _forward;
    [SerializeField]
    private Vector3d _up;
    [SerializeField]
    private Vector3d _right;

    public override double GravConst
    {
        get { return 0; }
    }

    public void Start()
    {
        base.Start();

        _forward = ViewTransforms.Instance.VectorView2Physics(Forward);
        _up = ViewTransforms.Instance.VectorView2Physics(Up);
        _right = ViewTransforms.Instance.VectorView2Physics(Right);
    }

    public override Vector3d GAccelerationFor(Body body)
    {
        return Vector3d.zero;
    }

}
