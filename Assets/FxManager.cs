using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxManager : MonoBehaviour
{
    public static FxManager instance = null;
    public GameObject playerHitFx;
    public GameObject enemyHitFx;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void PlayFx(GameObject fx, Vector3 position)
    {
        GameObject newFx = Instantiate(fx, position, Quaternion.identity).GetComponent<GameObject>();
    }
}
