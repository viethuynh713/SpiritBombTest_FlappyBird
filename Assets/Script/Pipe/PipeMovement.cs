using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    public SpriteRenderer upperPipe;
    public SpriteRenderer lowerPipe;
    public float speed = 2f;
    public float gap = 9f;
    public bool passed;
    private Camera mainCamera;
    private PipeSpawner pipeSpawner;
    private void Awake()
    {
        mainCamera = Camera.main;
        pipeSpawner = gameObject.GetComponentInParent<PipeSpawner>();
        SetPipeGap();
    }

    void Update()
    {
        if (GameManager.Instance.gameState == GameState.Running)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;

            if (mainCamera.WorldToViewportPoint(gameObject.transform.position).x < -0.2f)
            {
                pipeSpawner.ReleasePipe(this);
            }
        }
    }

    void OnEnable()
    {
        var xPos = mainCamera.ViewportToWorldPoint(new Vector3(1.2f,0,0)).x;
        transform.position = new Vector3(xPos, Random.Range(-1f, 3f), 0);
        passed = false;
    }
    private void SetPipeGap()
    {
        upperPipe.transform.localPosition = new Vector3(0, (gap + upperPipe.bounds.size.y)/2, 0);
        lowerPipe.transform.localPosition = new Vector3(0, -(gap + upperPipe.bounds.size.y) / 2, 0);
    }
}
