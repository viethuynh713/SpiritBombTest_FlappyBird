using System;
using UnityEngine;
using UnityEngine.Pool;

public class PipeSpawner : MonoBehaviour
{
    public PipeMovement pipePrefab;
    public float spawnRate = 2f;
    private float timer = 0f;

    private ObjectPool<PipeMovement> pipePool;

    void Start()
    {
        pipePool = new ObjectPool<PipeMovement>(CreatePipe, OnGetPipe, OnReleasePipe, OnDestroyPipe, false, 5, 10);
    }

    void Update()
    {
        if (GameManager.Instance.gameState == GameState.Running)
        {
            timer += Time.deltaTime;
            if (timer >= spawnRate)
            {
                timer = 0f;
                SpawnPipe();
            }
        }
        
    }

    PipeMovement CreatePipe()
    {
        return Instantiate(pipePrefab,gameObject.transform);
    }
    
    void OnGetPipe(PipeMovement pipe)
    {
        pipe.gameObject.SetActive(true);
    }
    
    void OnReleasePipe(PipeMovement pipe)
    {
        pipe.gameObject.SetActive(false);
    }
    
    void OnDestroyPipe(PipeMovement pipe)
    {
        Destroy(pipe);
    }

    public void SpawnPipe()
    {
        pipePool.Get();
    }

    public void ReleasePipe(PipeMovement pipe)
    {
   
        pipePool.Release(pipe);
    }

    public PipeMovement[] GetAllPipe()
    {
        return GetComponentsInChildren<PipeMovement>();
    }

    public void ResetAllPipe()
    {
        foreach (PipeMovement pipe in GetAllPipe())
        {
            ReleasePipe(pipe);
        }
        timer = 0;
    }
}
