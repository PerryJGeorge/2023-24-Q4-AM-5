using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_thing : MonoBehaviour
{
    public Transform target;
    public float speed;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.fixedDeltaTime * speed) + offset;

        }
    }
}
