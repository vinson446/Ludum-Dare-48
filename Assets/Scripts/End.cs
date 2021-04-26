using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class End : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.audioSource.Stop();
        StartCoroutine(EndCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EndCoroutine()
    {
        text.DOFade(1, 4);

        yield return new WaitForSeconds(4 + 1);

        text.DOFade(0, 4);

        yield return new WaitForSeconds(4 + 1);

        SceneManager.LoadScene(0);
    }
}
