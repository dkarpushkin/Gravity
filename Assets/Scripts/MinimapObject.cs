using UnityEngine;
using System.Collections;

public class MinimapObject : MonoBehaviour
{
    public GameObject OriginalBody;
    public static double Ratio = 1.0/100.0;

    void Update()
    {
        var component = OriginalBody.GetComponent<Body>();
        Vector3d viewPos = component.Position * Ratio;
        transform.localPosition = viewPos.FloatVector;
    }
}
