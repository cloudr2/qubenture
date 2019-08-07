using UnityEngine;
using TMPro;

public class WAchievementWidget : MonoBehaviour
{
    public TextMeshProUGUI titleTexbox;
    public TextMeshProUGUI descTextBox;

    public bool isShown { get { return gameObject.activeSelf; } }

    public void HideWidget() {
        gameObject.SetActive(false);
    }

}
