using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    public Slider progressbar;

    void Start()
    {
        StartCoroutine(LoadAsync(1));
    }

    private IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneIndex);
        while (!op.isDone)
        {
            float prog = op.progress * 100;
            progressbar.value = op.progress;
            loadingText.text = ("Loading " + prog.ToString() + "% ...");
            yield return null;
        }
    }
}
