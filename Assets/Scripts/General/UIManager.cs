using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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
    [SerializeField] float durationBtwnText;
    [SerializeField] float textFadeDuration;

    [Header("IR")]
    [SerializeField] Image fadeImage;
    [SerializeField] Image[] filledHealthImages;
    [SerializeField] Sprite filledHealthSprite;
    [SerializeField] Sprite emptyHealthSprite;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartingGameDialogueCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGameFade(float duration)
    {
        fadeImage.DOFade(0, duration);
    }

    public void DeathFade(float deathDuration, float respawnDuration)
    {
        StartCoroutine(DeathFadeCoroutine(deathDuration, respawnDuration));
    }

    IEnumerator DeathFadeCoroutine(float deathDuration, float respawnDuration)
    {
        fadeImage.DOFade(0, deathDuration);

        yield return new WaitForSeconds(deathDuration);

        player.CurrentHP = 3;
        player.Respawn();
        ResetHealth();

        yield return new WaitForSeconds(2);

        fadeImage.DOFade(1, respawnDuration);
    }

    public void TakeDamage(int currentHP)
    {
        filledHealthImages[currentHP].transform.DOShakePosition(shakeDuration, shakeIntensity, shakeVibrato, shakeRandomness, shakeFadeOut);
        filledHealthImages[currentHP].sprite = emptyHealthSprite;
    }

    // for GameManager's testing controls
    public void ResetHealth()
    {
        foreach (Image i in filledHealthImages)
        {
            i.sprite = filledHealthSprite;
        }
    }

    IEnumerator StartingGameDialogueCoroutine()
    {
        dialogueText.text = introDialogueSequence[0];
        dialogueText.DOFade(1, textFadeDuration);

        yield return new WaitForSeconds(textFadeDuration);

        dialogueText.DOFade(0, textFadeDuration);

        yield return new WaitForSeconds(textFadeDuration);
        yield return new WaitForSeconds(durationBtwnText);

        dialogueText.text = introDialogueSequence[1];
        dialogueText.DOFade(1, textFadeDuration);

        yield return new WaitForSeconds(textFadeDuration);

        dialogueText.DOFade(0, textFadeDuration);

        yield return new WaitForSeconds(textFadeDuration);
        yield return new WaitForSeconds(durationBtwnText);

        dialogueText.text = introDialogueSequence[2];
        dialogueText.DOFade(1, textFadeDuration);

        yield return new WaitForSeconds(textFadeDuration);

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

        yield return new WaitForSeconds(durationBtwnText);

        dialogueText.DOFade(0, textFadeDuration);
    }

    public void DeathDialogue()
    {
        numDeaths++;
        StartCoroutine(StartFallingDialogueCoroutine());
    }

    IEnumerator DeathDialogueCoroutine()
    {
        dialogueText.text = fallingDialogueSequence[numDeaths];
        dialogueText.DOFade(1, 0);

        yield return new WaitForSeconds(durationBtwnText);

        dialogueText.DOFade(0, textFadeDuration);
    }
}
