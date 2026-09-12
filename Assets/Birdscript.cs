using UnityEngine;

public class Birdscript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public logicScript logic;
    public static bool batAlive = true;
    
    public Animator animator;

    void Start()
    {
        batAlive = true;
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
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
        
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.linearVelocity = Vector2.up * flapStrength;
            
            if (animator != null)
            {
                animator.SetTrigger("Flap");
            }
            
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
        
        // 1. Play immediate physical impact sound upon collision
        if (AudioManager.instance != null)
        {
            AudioManager.instance.playSFX(AudioManager.instance.deathSound);
        }

        // 2. Wait 0.5 seconds for death sound to finish before opening Game Over UI
        StartCoroutine(DelayedGameOver());
        
    }
    private System.Collections.IEnumerator DelayedGameOver()
    {
        yield return new WaitForSeconds(0.5f);
        if (logic != null)
        {
            logic.gameOver();
        }
    }
}
