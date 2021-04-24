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

    [Header("IR")]
    [SerializeField] Image[] filledHealthImages;
    [SerializeField] Sprite filledHealthSprite;
    [SerializeField] Sprite emptyHealthSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
