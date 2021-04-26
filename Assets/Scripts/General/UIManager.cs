using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] float shakeDuration;
    [SerializeField] Vector3 shakeIntensity;
    [SerializeField] int shakeVibrato;
    [SerializeField] float shakeRandomness;
    [SerializeField] bool shakeFadeOut;

    [Header("Dialogue")]
    [SerializeField] TextMeshProUGUI dialogueText;
    [TextArea(0, 5)]
    [SerializeField] string[] introDialogueSequence;
    [TextArea(0, 5)]
    [SerializeField] string[] fallingDialogueSequence;
    public bool hasFallen;
    [TextArea(0, 5)]
    [SerializeField] string[] deathDialogueSequence;
    public int numDeaths;
    [TextArea(0, 5)]
    [SerializeField] string[] endDialogueSequence;

    [Header("Dialogue Settings")]
    [SerializeField] float durationBtwnText;
    [SerializeField] float textDuration;
    [SerializeField] float textFadeDuration;

    [Header("IR")]
    [SerializeField] Image fadeImage;
    [SerializeField] Image[] filledHealthImages;
    [SerializeField] Sprite filledHealthSprite;
    [SerializeField] Sprite emptyHealthSprite;
    [SerializeField] Animator endAnimator;

    [SerializeField] Player player;
    Coroutine deathCoroutine;
    Coroutine deathDialogueCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        StartGameFade(1);
        if (SceneManager.GetActiveScene().buildIndex == 1)
            StartCoroutine(StartingGameDialogueCoroutine());
        if (SceneManager.GetActiveScene().buildIndex == 2)
            StartCoroutine(EndDialogueCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGameFade(float duration)
    {
        fadeImage.DOFade(0, duration);
    }

    public void EndGameFade(float duration)
    {
        fadeImage.DOFade(1, duration);
    }

    IEnumerator EndGameFadeCoroutine(float duration)
    {
        fadeImage.DOFade(1, duration);

        yield return new WaitForSeconds(duration + 1);

        GameManager.instance.ChangeScenes(2);
    }

    IEnumerator StartingGameDialogueCoroutine()
    {
        dialogueText.text = introDialogueSequence[0];
        dialogueText.DOFade(1, textFadeDuration);

        yield return new WaitForSeconds(textDuration + textFadeDuration);

        dialogueText.DOFade(0, textFadeDuration);

        yield return new WaitForSeconds(textFadeDuration);
        yield return new WaitForSeconds(durationBtwnText);

        dialogueText.text = introDialogueSequence[1];
        dialogueText.DOFade(1, textFadeDuration);

        yield return new WaitForSeconds(textDuration + textFadeDuration);

        dialogueText.DOFade(0, textFadeDuration);
    }

    public void StartFallingDialogue()
    {
        if (!hasFallen)
        {
            StopAllCoroutines();
            StartCoroutine(StartFallingDialogueCoroutine());
            hasFallen = true;
        }
    }

    IEnumerator StartFallingDialogueCoroutine()
    {
        dialogueText.text = fallingDialogueSequence[0];
        dialogueText.DOFade(1, 0);

        yield return new WaitForSeconds(textFadeDuration);

        dialogueText.DOFade(0, textFadeDuration);
    }

    public void TakeDamage(int currentHP)
    {
        if (currentHP >= 0)
        {
            filledHealthImages[currentHP].transform.DOShakePosition(shakeDuration, shakeIntensity, shakeVibrato, shakeRandomness, shakeFadeOut);
            filledHealthImages[currentHP].sprite = emptyHealthSprite;
        }
    }

    public void DeathFade(float deathDuration, float respawnDuration)
    {
        /*
        if (deathCoroutine != null)
            StopCoroutine(deathCoroutine);
        */
        deathCoroutine = StartCoroutine(DeathFadeCoroutine(deathDuration, respawnDuration));
    }

    IEnumerator DeathFadeCoroutine(float deathDuration, float respawnDuration)
    {
        fadeImage.DOFade(1, deathDuration);
        player.isDead = true;

        yield return new WaitForSeconds(deathDuration + 1);

        player.CurrentHP = filledHealthImages.Length;
        player.isFalling = false;
        player.Respawn();

        ResetHealth();

        yield return new WaitForSeconds(2);

        fadeImage.DOFade(0, respawnDuration);

        yield return new WaitForSeconds(respawnDuration);

        DeathDialogue();
        player.isDead = false;
    }

    // for GameManager's testing controls
    public void ResetHealth()
    {
        foreach (Image i in filledHealthImages)
        {
            i.sprite = filledHealthSprite;
        }
    }

    public void DeathDialogue()
    {
        /*
        if (deathDialogueCoroutine != null)
            StopCoroutine(deathDialogueCoroutine);
        */
        deathDialogueCoroutine = StartCoroutine(DeathDialogueCoroutine());
    }

    IEnumerator DeathDialogueCoroutine()
    {
        dialogueText.text = deathDialogueSequence[numDeaths];
        dialogueText.DOFade(1, 0);

        yield return new WaitForSeconds(textFadeDuration);

        dialogueText.DOFade(0, textFadeDuration);
        numDeaths++;
    }

    IEnumerator EndDialogueCoroutine()
    {
        foreach (Image i in filledHealthImages)
        {
            i.gameObject.SetActive(false);
        }

        dialogueText.text = endDialogueSequence[0];
        dialogueText.DOFade(1, textFadeDuration);

        yield return new WaitForSeconds(textDuration + textFadeDuration);

        dialogueText.DOFade(0, textFadeDuration);

        yield return new WaitForSeconds(textFadeDuration);
        yield return new WaitForSeconds(durationBtwnText);

        dialogueText.text = endDialogueSequence[1];
        dialogueText.DOFade(1, textFadeDuration);

        yield return new WaitForSeconds(textDuration + textFadeDuration);

        dialogueText.DOFade(0, textFadeDuration);

        yield return new WaitForSeconds(textFadeDuration);
        yield return new WaitForSeconds(durationBtwnText);

        dialogueText.text = endDialogueSequence[2];
        dialogueText.DOFade(1, textFadeDuration);

        endAnimator.CrossFadeInFixedTime("WakeUp", 0.25f);
    }
}
