using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameTester();
    }

    public void ChangeScenes(int index)
    {
        SceneManager.LoadScene(index);
    }

    void GameTester()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Player player = FindObjectOfType<Player>();
            player.CurrentHP = 3;

            UIManager uiManager = FindObjectOfType<UIManager>();
            uiManager.ResetHealth();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Player player = FindObjectOfType<Player>();
            player.Die();
        }
    }
}
