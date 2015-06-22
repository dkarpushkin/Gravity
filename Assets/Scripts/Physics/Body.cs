using UnityEngine;
using System.Collections;
using Phys;

/// <summary>
/// Тело, на к-рое действуют силы гравитации от других таких же тел.
/// </summary>
public class Body : MonoBehaviour
{
    public Vector3 InitialVelocity;

    public double PhysicalInitialPositionX;
    public double PhysicalInitialPositionY;
    public double PhysicalInitialPositionZ;

    public virtual double GravConst
    {
        get { return _gravCoef; }
    }

    public Vector3d Position;

    [SerializeField]
    private double _mass;   //  kg
    public virtual double Mass
    {
        get { return _mass; }
        protected set { _mass = value; }
    }
    
    //[SerializeField]
    private Vector3d _velocity; //  m/s
    public Vector3d Velocity
    {
        get { return _velocity; }
        private set { _velocity = value; }
    }
    //[SerializeField]
    private Vector3d _acceleration; //  m/s2
    public Vector3d Acceleration
    {
        get { return _acceleration; }
        private set { _acceleration = value; }
    }
    //[SerializeField]
    private double _gravCoef;

    private Transform _viewAnchor;

    private Vector3d _lastAcceleration;

    protected Vector3d _lastPosition;

    #region Unity Callbacks

    public virtual void Start()
    {
        _gravCoef = Forces.GravConst * _mass;
        _velocity = new Vector3d(InitialVelocity.x, InitialVelocity.y, InitialVelocity.z);
        _lastPosition = new Vector3d(PhysicalInitialPositionX, PhysicalInitialPositionY, PhysicalInitialPositionZ);
        Position = _lastPosition;
        _viewAnchor = GameObject.FindGameObjectWithTag("Player").transform;
        _lastAcceleration = GravAccelerations();
    }

    public virtual void Update()
    {
        UpdateView();
    }

    #endregion // UnityCallbacks

    protected void UpdateAcceleration(float dt)
    {
        _acceleration = GravAccelerations();
    }

    /// <summary>
    /// Ускорение сообщаемое телу
    /// </summary>
    public virtual Vector3d GAccelerationFor(Body body)
    {
        Vector3d direction = Position - body.Position;
        double distance = direction.magnitude;
        double force = _gravCoef / (Mathd.Abs(distance) >= Mathd.Epsilon ? (distance * distance) : Mathd.Epsilon);

        return direction.normalized * force;
    }

    /// <summary>
    /// Сумма действующих на тело гравитаций в точке Position
    /// </summary>
    /// <returns></returns>
    public Vector3d GravAccelerations()
    {
        Vector3d accel = Vector3d.zero;
        foreach (var body in FindObjectsOfType<Body>())
            if (body != this)
            {
                accel += body.GAccelerationFor(this);
            }
        return accel;
    }

    /// <summary>
    /// Вызывать до UpdateVelocity
    /// </summary>
    public void UpdatePosition(float dt)
    {
        UpdateAcceleration(Time.fixedDeltaTime);

        _velocity += _acceleration * Time.fixedDeltaTime / 2;
        Position += _velocity * Time.fixedDeltaTime;
    }
    
    /// <summary>
    /// Вызывать после того, как UpdatePosition будут вызваны для всех тел с гравитацией
    /// </summary>
    /// <param name="dt"></param>
    public void UpdateVelocity(float dt)
    {
        UpdateAcceleration(Time.fixedDeltaTime);
        _velocity += _acceleration * Time.fixedDeltaTime / 2;
    }

    // TODO: кандидат на вынос
    protected virtual void UpdateView()
    {
        transform.position = ViewTransforms.Instance.PointPhysics2View(Position, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0.0f));
    }

    public void Awake()
    {
        Debug.Log(PhysicsManager.Instance);
    }
}
