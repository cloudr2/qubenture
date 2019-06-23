using System.Collections.Generic;
using UnityEngine;

public interface IUpdateable
{
    void CustomUpdate();
    void CustomLateUpdate();
}

public class UpdateManager : MonoBehaviour
{
    #region singleton
    private static UpdateManager _instance;
    public static UpdateManager Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    private static List<IUpdateable> updateableObjects = new List<IUpdateable>();

    public static void Register(IUpdateable obj)
    {
        if (obj == null)
            return;

        updateableObjects.Add(obj);
    }

    public static void Unregister(IUpdateable obj)
    {
        if (obj == null)
            return;

        updateableObjects.Remove(obj);
    }

    void Update()
    {
        for (int i = 0; i < updateableObjects.Count; i++)
        {
            updateableObjects[i].CustomUpdate();
        }
    }

    void LateUpdate() {
        for (int i = 0; i < updateableObjects.Count; i++) {
            updateableObjects[i].CustomLateUpdate();
        }
    }
}
