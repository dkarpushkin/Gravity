using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrajectoryDrawer : MonoBehaviour
{
    protected IDictionary<string, GameObject> _trajectories;

    // Use this for initialization
    void Start()
    {

    }

    public void FixedUpdate()
    {
        foreach (var actor in FindObjectsOfType<ActorBody>())
            DrawTrajectoryFor(actor);
    }

    protected void DrawTrajectoryFor(Body body)
    {
        Vector3 position = body.gameObject.transform.position;

        //for (int i = 0; i < 5; ++i)
        //{
        //    var shift = body.FuturePositionShift(Time.fixedDeltaTime * 10);
        //}
    }

    protected GameObject CreatePolyLine(IList<Vector3> points, Color color)
    {
        var obj = new GameObject("Line");
        obj.AddComponent<LineRenderer>();

        var lineRenderer = obj.GetComponent<LineRenderer>();
        lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        lineRenderer.SetColors(color, color);
        lineRenderer.receiveShadows = false;
        lineRenderer.SetVertexCount(points.Count);

        for (int i = 0; i < points.Count; ++i)
        {
            lineRenderer.SetPosition(i, points[i]);
        }

        return obj;
    }
}
