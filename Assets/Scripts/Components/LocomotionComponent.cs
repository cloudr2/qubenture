using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionComponent : MonoBehaviour
{
    public float speed;

    public void Move(Vector3 direction) {
        transform.position += direction * speed * Time.deltaTime;
        
    }

    public void PlayWalk(Animator anim) {
        anim.SetFloat("speed", Mathf.Lerp(0, 1, 0.5f));
    }
}
