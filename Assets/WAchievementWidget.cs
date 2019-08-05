using UnityEngine;
using UnityEngine.UI;


public class WAchievementWidget : MonoBehaviour
{
    public Text titleTexbox;
    public Text descTextBox;

    public void HideWidget() {
        this.gameObject.SetActive(false);
    }
}
