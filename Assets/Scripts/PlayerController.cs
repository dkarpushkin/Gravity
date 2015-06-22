using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float MovingSpeed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool f = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool b = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool l = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool r = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        float vert = (f ? Time.deltaTime * MovingSpeed : 0f) - (b ? Time.deltaTime * MovingSpeed : 0f);
        float horiz = (r ? Time.deltaTime * MovingSpeed : 0f) - (l ? Time.deltaTime * MovingSpeed : 0f);

        transform.Translate(horiz, vert, 0);
    }
}
