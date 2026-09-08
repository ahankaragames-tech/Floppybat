using UnityEngine;

public class Birdscript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public logicScript logic;
    public static bool batAlive = true;

    void Start()
    {
        batAlive = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!batAlive) return;

        else
        {
            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

            // viewPos.y > 1.0f means above top edge
            // viewPos.y < 0.0f means below bottom edge
            if (viewPos.y > 1.0f || viewPos.y < 0.0f)
            {
                logic.gameOver();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.linearVelocity = Vector2.up * flapStrength;    
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if ((logic != null) || (!batAlive))
        {
            logic.gameOver();
        }
        else
        {
            Debug.LogError("Logic reference is missing on the bird script!");
        }
        batAlive = false;
    }
}
