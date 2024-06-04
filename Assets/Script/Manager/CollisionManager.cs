using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public BirdFlying bird;
    public PipeSpawner pipeSpawner;
    public float groundY = -4.5f;
    public float ceilingY = 5f;
    [SerializeField]private EndGamePanelUI endGamePanelUI;

    private Vector2 birdSize;

    void Start()
    {
        birdSize = bird.GetComponent<SpriteRenderer>().bounds.size;
    }

    void Update()
    {
        
        if (GameManager.Instance.gameState == GameState.Running)
        {
            CheckCollisionsCeiling();
            CheckCollisionsGround();
            CheckCollisionsPipe();
            CheckPassPipe();
        }
    }

    void CheckCollisionsCeiling()
    {
        if (bird.transform.position.y + birdSize.y / 2 > ceilingY)
        {
            GameManager.Instance.GameOver();
            endGamePanelUI.gameObject.SetActive(true);
            endGamePanelUI.EndGame();
        }
    }

    void CheckCollisionsGround()
    {
        if (bird.transform.position.y - birdSize.y / 2 < groundY)
        {
            GameManager.Instance.GameOver();
            endGamePanelUI.gameObject.SetActive(true);
            endGamePanelUI.EndGame();
        }
    }

    void CheckCollisionsPipe()
    {
        Vector2 birdPos = bird.transform.position;

        foreach (PipeMovement pipe in pipeSpawner.GetComponentsInChildren<PipeMovement>())
        {
            if (!pipe.gameObject.activeInHierarchy) continue;

            Vector2 upperPipeSize = pipe.upperPipe.bounds.size;
            Vector2 lowerPipeSize = pipe.lowerPipe.bounds.size;

            Vector2 upperPipePos = pipe.upperPipe.transform.position;
            Vector2 lowerPipePos = pipe.lowerPipe.transform.position;

            Vector2 upperPipeTopLeft = new Vector2(upperPipePos.x - upperPipeSize.x / 2, upperPipePos.y + upperPipeSize.y / 2);
            Vector2 upperPipeBottomRight = new Vector2(upperPipePos.x + upperPipeSize.x / 2, upperPipePos.y - upperPipeSize.y / 2);

            Vector2 lowerPipeTopLeft = new Vector2(lowerPipePos.x - lowerPipeSize.x / 2, lowerPipePos.y + lowerPipeSize.y / 2);
            Vector2 lowerPipeBottomRight = new Vector2(lowerPipePos.x + lowerPipeSize.x / 2, lowerPipePos.y - lowerPipeSize.y / 2);

            if (IsOverlap(birdPos, birdSize, upperPipeTopLeft, upperPipeBottomRight) ||
                IsOverlap(birdPos, birdSize, lowerPipeTopLeft, lowerPipeBottomRight))
            {
                GameManager.Instance.GameOver();
                endGamePanelUI.gameObject.SetActive(true);
                endGamePanelUI.EndGame();
            }
        }
    }

    void CheckPassPipe()
    {
        Vector2 birdPos = bird.transform.position;

        foreach (PipeMovement pipe in pipeSpawner.GetComponentsInChildren<PipeMovement>())
        {
            if (!pipe.gameObject.activeInHierarchy) continue;

            if (!pipe.passed && birdPos.x - birdSize.x > pipe.transform.position.x + pipe.upperPipe.bounds.size.x / 2)
            {
                GameManager.Instance.AddScore();
                pipe.passed = true;
            }
        }
    }

    bool IsOverlap(Vector2 pos1, Vector2 size1, Vector2 topLeft2, Vector2 bottomRight2)
    {
        Vector2 topLeft1 = new Vector2(pos1.x - size1.x / 2, pos1.y + size1.y / 2);
        Vector2 bottomRight1 = new Vector2(pos1.x + size1.x / 2, pos1.y - size1.y / 2);

        return topLeft1.x < bottomRight2.x &&
               bottomRight1.x > topLeft2.x &&
               topLeft1.y > bottomRight2.y &&
               bottomRight1.y < topLeft2.y;
    }
}
