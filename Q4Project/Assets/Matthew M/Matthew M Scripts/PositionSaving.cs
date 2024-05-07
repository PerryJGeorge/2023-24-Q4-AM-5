using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSaving : MonoBehaviour
{

    public static Vector2 Position;

    private void Awake()
    {
        transform.position = Position;
    }

    void Update()
    {
        Position = transform.position;
    }
}
