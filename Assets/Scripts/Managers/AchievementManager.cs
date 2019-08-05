using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AchievementManager : MonoBehaviour {
    #region Singleton

    private static AchievementManager _instance;
    public static AchievementManager Instance { get { return _instance; } }

    void Awake() {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    //win a stage without receiving damage
    //kill n enemies with grenades
    //kill n enemies with sword
    //speedster
    //win game in 1 try

    private WAchievementWidget achWidget;
    private int numberOfWidgetsToShow;
    public GameObject achWidgetPrefab;

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        achWidget = FindObjectOfType<WAchievementWidget>();
        if(achWidget)
            HideWidget();
    }

    public void HideWidget() {
        achWidget.gameObject.SetActive(false);
    }

    public void ShowWidget(string title, string desc) {
        if (achWidget) {            
            achWidget.titleTexbox.text = title;
            achWidget.descTextBox.text = desc;
            achWidget.gameObject.SetActive(true);
        }
    }
}
