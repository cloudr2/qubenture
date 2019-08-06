using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneOnEnable : MonoBehaviour
{
    void OnEnable()
    {
        GameManager.Instance.NextLevel();
    }
}
