using UnityEngine;

public class BirdFlying : MonoBehaviour
{
    public float flapStrength = 5f;
    public float gravity = -9.8f;

    private Vector2 velocity;
    private PipeMovement nearestPipe;
    private SpriteRenderer spriteRenderer;
    private PipeSpawner spawner;
    private float targetHeight = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawner = FindObjectOfType<PipeSpawner>();
    }
    private void Update()
    {
        if (GameManager.Instance.gameMode == GameMode.Normal)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                Flap();
            }
#else
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Flap();
            }
#endif
        }
        else if (GameManager.Instance.gameMode == GameMode.Bot)
        {
            AutoFlap();
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.gameState == GameState.Running)
        {
            ApplyGravity();
        }
    }

    private void Flap()
    {
        velocity.y = flapStrength;
    }

    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        transform.position += (Vector3)velocity * Time.fixedDeltaTime;
    }


    private void AutoFlap()
    {
        if (nearestPipe == null || nearestPipe.passed)
        {
            nearestPipe = FindNearestPipe();
        }

        if (nearestPipe != null)
        {
            float pipeY = nearestPipe.transform.position.y;
            
            targetHeight = pipeY + 0.2f + spriteRenderer.bounds.size.y/2 - nearestPipe.gap / 2;
        }

        if (transform.position.y < targetHeight)
        {
            Flap();
        }
    }

    private PipeMovement FindNearestPipe()
    {
        float minDistance = float.MaxValue;
        PipeMovement[] pipes = spawner.GetAllPipe();
        PipeMovement pipeResult = null;

        foreach (PipeMovement pipe in pipes)
        {
            if (pipe.passed || !pipe.gameObject.activeInHierarchy) continue;
            float distance = pipe.transform.position.x - transform.position.x;
            if (distance > 0 && distance < minDistance)
            {
                minDistance = distance;
                pipeResult = pipe;
            }
        }

        if (minDistance == float.MaxValue)
        {
            return null;
        }
        return pipeResult;
    }
}
