using UnityEngine;

public class stalagSpawner : MonoBehaviour
{
    public GameObject stalagPrefab;
    public float spawnRate;
    private float timer = 0;
    public float stalagOffset;

    private void Start()
    {
        if (!logicScript.isGameOver)
        {
            spawnStalag();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (logicScript.isGameOver) return;
        
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            spawnStalag();
            timer = 0;
        }
    }

    void spawnStalag()
    {
        float lowestPoint= transform.position.y - stalagOffset;
        float highestPoint = transform.position.y + stalagOffset;
        
        Instantiate(stalagPrefab, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        
    }
}
