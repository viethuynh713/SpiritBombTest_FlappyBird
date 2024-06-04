using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public void OnClickPlay()
    {
        GameManager.Instance.gameMode = GameMode.Normal;
        GameManager.Instance.gameState = GameState.Running;
        SceneManager.LoadScene("GamePlayScene");
    }
    public void OnClickAutoPlay()
    {
        GameManager.Instance.gameMode = GameMode.Bot;
        GameManager.Instance.gameState = GameState.Running;
        SceneManager.LoadScene("GamePlayScene");
    }
    public void OnClickExit()
    {
        Application.Quit();
    }
}
