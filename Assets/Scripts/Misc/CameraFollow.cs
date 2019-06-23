using UnityEngine;

public class CameraFollow : MonoBehaviour, IUpdateable {

    public float smoothness = 0.125f;
    public Vector3 offset;

    private Player player;

    public void OnEnable() {
        UpdateManager.Register(this);
    }

    public void OnDisable() {
        UpdateManager.Unregister(this);
    }

    public void CustomUpdate() {
        return;
    }

    private void Awake() {
        player = FindObjectOfType<Player>();
    }

    public void CustomLateUpdate() {
        Vector3 targetPos = player.transform.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, smoothness);
        transform.position = new Vector3(smoothPos.x, smoothPos.y, smoothPos.z);
    }
}
