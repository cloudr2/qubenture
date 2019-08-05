using UnityEngine;
using UnityEngine.UI;


public class WAchievementWidget : MonoBehaviour
{
    public Text titleTexbox;
    public Text descTextBox;
    public bool isShown { get { return this.gameObject.activeSelf; } }

    public void HideWidget() {
        this.gameObject.SetActive(false);
    }

}
