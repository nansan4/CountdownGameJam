using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenuButtons : MonoBehaviour
{

    [SerializeField] private Button PlayButton;
    [SerializeField] private Button QuitButton;
    [SerializeField] private Image fadeOutImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayButton.onClick.AddListener(StartGame);
        QuitButton.onClick.AddListener(QuitGame);
    }

    private IEnumerator FadeInPlay()
    {
        yield return new WaitForSeconds(1f);
        float fadeDuration = 1f;
        float elapsedTime = 0f;
        Color color = fadeOutImage.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeOutImage.color = color;
            yield return null;
        }
        SceneManager.LoadScene(1);
    }
    private IEnumerator FadeInQuit()
    {
        yield return new WaitForSeconds(1f);
        float fadeDuration = 1f;
        float elapsedTime = 0f;
        Color color = fadeOutImage.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeOutImage.color = color;
            yield return null;
        }
        Application.Quit();
    }
    private void StartGame()
    {
        StartCoroutine(FadeInPlay());
        
    }

    private void QuitGame()
    {

        Debug.Log("Goodbye");
        StartCoroutine(FadeInQuit());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
