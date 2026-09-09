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
        if (!batAlive)
        {
            return;
        }
        else 
        {
            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

            // viewPos.y > 1.0f means above top edge
            // viewPos.y < 0.0f means below bottom edge
            if (viewPos.y > 1.0f || viewPos.y < 0.0f)
            {
                logic.gameOver();
                batAlive = false;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.linearVelocity = Vector2.up * flapStrength;
            // Inside your Jump / Flap function:
            if (AudioManager.instance != null)
            {
                AudioManager.instance.playSFX(AudioManager.instance.flySound);
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if (!batAlive) return;
        
        batAlive = false;
        
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSFX(AudioManager.instance.deathSound);
        }
        
        if ((logic != null))
        {
            logic.gameOver();
        }
    }
}
