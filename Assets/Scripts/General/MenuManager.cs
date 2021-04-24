using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float fadeInDuration;
    [SerializeField] float fadeOutDuration;
    bool fadeIn;

    [Header("IR")]
    [SerializeField] Image fadeImage;

    // Start is called before the first frame update
    void Start()
    {
        fadeIn = true;
        Fade(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    IEnumerator StartGameCoroutine()
    {
        fadeIn = false;
        Fade(1);

        yield return new WaitForSeconds(fadeOutDuration + 1);

        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        StartCoroutine(QuitGameCoroutine());
    }

    IEnumerator QuitGameCoroutine()
    {
        fadeIn = false;
        Fade(1);

        yield return new WaitForSeconds(fadeOutDuration + 1);

        Application.Quit();
    }

    public void Fade(float alpha)
    {
        if (fadeIn)
            fadeImage.DOFade(alpha, fadeInDuration);
        else
            fadeImage.DOFade(alpha, fadeOutDuration);
    }
}
