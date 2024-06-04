using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGamePanelUI : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text finalScore;
    [SerializeField] private TMPro.TMP_Text highestScore;

    private ScoreTxtUI scoreTxtUI;

    private PipeSpawner pipeSpawner;
    private BirdFlying birdFlying;
    private void Awake()
    {
        pipeSpawner = FindObjectOfType<PipeSpawner>(); 

        birdFlying = FindObjectOfType<BirdFlying>();

        scoreTxtUI = FindObjectOfType<ScoreTxtUI>();

        gameObject.SetActive(false);
    }
    public void EndGame()
    {
        int score = GameManager.Instance.score;
        finalScore.text = string.Concat("Score: " ,score.ToString());

        var hScore = score;

        if (PlayerPrefs.HasKey("HS"))
        {
            if(PlayerPrefs.GetInt("HS") > hScore)
            {
                hScore = PlayerPrefs.GetInt("HS");
            }  
        }

        PlayerPrefs.SetInt("HS", hScore);

        highestScore.text = string.Concat("Highest Score: " , hScore.ToString());

        gameObject.SetActive(true);
    }
    public void OnClickRePlay()
    {
        pipeSpawner.ResetAllPipe();

        birdFlying.transform.position = new Vector3(-2,0,0);

        gameObject.SetActive(false);

        GameManager.Instance.gameState = GameState.Running;

        GameManager.Instance.score = 0;
        scoreTxtUI.UpdateScore(0);

    }
    public void OnClickMenu()
    {
        SceneManager.LoadScene("MenuScene");
        GameManager.Instance.score = 0;
        GameManager.Instance.gameState = GameState.End;
    }
}
