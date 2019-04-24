using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionComponent : MonoBehaviour
{
    public float speed;

    public void Move(Vector3 direction) {
        transform.position += direction * speed * Time.deltaTime; 
    }
}
