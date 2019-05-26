using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    public Canvas uICanvas;
    public GameObject uIPanel;
    public Text resultText;

    void Awake() {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start() {
        Initialize();
    }

    private void Initialize() {
        if (uIPanel)
            uIPanel.SetActive(false);
    }

    public void ShowResultScreen(bool state) {
        if (uIPanel)
            uIPanel.SetActive(state);
    }

    public void SetResultText(string state) {
        if(resultText)
        resultText.text = "YOU " + state;
    }
}
