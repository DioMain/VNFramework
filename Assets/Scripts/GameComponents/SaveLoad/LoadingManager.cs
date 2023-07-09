using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static bool GameIsReady;

    [SerializeField]
    private TextMeshProUGUI loadingText;

    [SerializeField]
    private Image background;

    [SerializeField]
    private GameObject container;

    private void Start()
    {
        container.SetActive(false);
    }

    public void LoadMainMenuScene() => StartCoroutine(LoadMainMenuCoroutine());

    public void LoadGameScene(int usingSlot = -1) => StartCoroutine(LoadGameCoroutine(usingSlot));

    private IEnumerator LoadMainMenuCoroutine()
    {
        container.SetActive(true);

        GameManager.Instance.Audio.StopBGM();
        GameManager.Instance.Audio.StopBGS();

        AsyncOperation loadasync = SceneManager.LoadSceneAsync(0);

        while (loadasync.isDone)
        {
            loadingText.text = $"Загрузка {loadasync.progress * 100}%";

            yield return null;
        }

        container.SetActive(false);
    }

    private IEnumerator LoadGameCoroutine(int usingSlot)
    {
        GameIsReady = false;

        container.SetActive(true);

        AsyncOperation loadasync = SceneManager.LoadSceneAsync(1);

        while (loadasync.isDone)
        {
            loadingText.text = $"Загрузка {loadasync.progress * 100}%";

            yield return null;
        }


        loadingText.text = $"Почти готово!";

        yield return new WaitForSeconds(1f);

        if (usingSlot > -1)
        {

        }

        yield return new WaitForSeconds(1f);

        GameIsReady = true;

        container.SetActive(false);
    }
}
