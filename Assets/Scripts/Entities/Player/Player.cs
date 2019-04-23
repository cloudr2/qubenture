using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IDestructible
{
    void Update() {
        LC.Move(MoveDirection());
    }

    private Vector3 MoveDirection() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        return new Vector3(horizontal, 0, vertical);
    }

    public void Destroy() {
        Destroy(gameObject);
    }
}
