using UnityEngine;
using System.Collections;

namespace Phys
{
    /// <summary>
    /// Отображение векторов физики и представления.
    /// </summary>
    class ViewTransforms : Singleton<ViewTransforms>
    {
        public static double PhysicsVectorScale = 1;

        public Vector3d VectorView2Physics(Vector3 v)
        {
            return new Vector3d(v.x / PhysicsVectorScale, v.y / PhysicsVectorScale, v.z / PhysicsVectorScale);
        }

        public Vector3 VectorPhysics2View(Vector3d v)
        {
            return new Vector3((float)(v.x * PhysicsVectorScale), (float)(v.y * PhysicsVectorScale), (float)(v.z * PhysicsVectorScale));
        }

        public Vector3d PointView2Physics(Vector3 v, Vector3 center)
        {
            Vector3d fv = VectorView2Physics(v);
            return fv + new Vector3d(center);
        }

        public Vector3 PointPhysics2View(Vector3d v, Vector3 center)
        {
            Vector3 fv = VectorPhysics2View(v);
            return fv;// -center;
        }

    }
}