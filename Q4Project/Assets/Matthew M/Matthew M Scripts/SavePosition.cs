using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePosition : MonoBehaviour
{
    public Transform target;
    public float speed;
    public Vector3 offset;
    public GameObject[] gameobje;

    void FixedUpdate()
    {
        gameobje = GameObject.FindGameObjectsWithTag("Player");

        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.fixedDeltaTime * speed) + offset;

        }
    }
}
