using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingUI : MonoBehaviour
{
    private void Awake()
    {
        this.gameObject.SetActive(false);   
    }
    public void OnClickButtonSetting()
    {
        GameManager.Instance.gameState = GameState.Pause;
    }
    public void OnClickResume() 
    {

        GameManager.Instance.gameState = GameState.Running;
        this.gameObject.SetActive(false);

    }
    public void OnClickHome() 
    {

        GameManager.Instance.score = 0;
        GameManager.Instance.gameState = GameState.End;
        SceneManager.LoadScene("MenuScene");
    }
}
