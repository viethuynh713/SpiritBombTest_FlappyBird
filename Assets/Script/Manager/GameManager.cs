using UnityEngine;
using VPackage.Singleton;

public class GameManager : ManualSingletonMono<GameManager>
{
    public int score = 0;
    public GameMode gameMode;
    public GameState gameState;
    private ScoreTxtUI scoreUI;
    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        Application.targetFrameRate = 60;
    }

    public void AddScore()
    {
        if (gameState == GameState.Running)
        {
            if(scoreUI == null)
            {
                scoreUI = FindObjectOfType<ScoreTxtUI>();
            }
            score++; 
            scoreUI.UpdateScore(score);
        }
    }

    public void GameOver()
    {
        gameState = GameState.End;
    }
}
