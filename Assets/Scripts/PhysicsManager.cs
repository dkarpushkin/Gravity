using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhysicsManager : Singleton<PhysicsManager>
{
    public void FixedUpdate()
    {
        Body[] bodies = FindObjectsOfType<Body>();

        foreach (var body in bodies)
        {
            body.UpdatePosition(Time.fixedDeltaTime);
        }

        foreach (var body in bodies)
        {
            body.UpdateVelocity(Time.fixedDeltaTime);
        }
    }
}
