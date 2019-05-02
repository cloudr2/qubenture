using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightProyectile : MonoBehaviour
{
    float Velocity;
    Vector3 Direction;

    public void Initialize(Vector3 origin, Vector3 destination, float time)
    {
        Velocity = Vector3.Distance(origin, destination) / time;
        Direction = destination - origin;
        Direction.Normalize();
    }

    void Update()
    {
        transform.localPosition += Direction * Velocity * Time.deltaTime;
    }   
}
