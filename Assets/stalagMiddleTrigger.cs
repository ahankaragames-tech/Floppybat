using Unity.VisualScripting;
using UnityEngine;

public class stalagMiddleTrigger : MonoBehaviour
{
    [Tooltip("Assign the Logic Manager from the Inspector")]
    public logicScript logic;

    void Start()
    {
        // Fallback safety check if you forgot to assign it in the Inspector
        if (logic == null)
        {
            GameObject logicObject = GameObject.FindGameObjectWithTag("Logic");
            if (logicObject != null)
            {
                logic = logicObject.GetComponent<logicScript>();
            }
            else
            {
                Debug.LogError("Logic Script tag error");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object belongs to your player layer (assuming Layer 3 is Player)
        if (collision.gameObject.layer == 3)
        {
            Debug.Log("Collided with Player, adding score.");
            
            if (logic != null)
            {
                logic.addScore();
            }
            else
            {
                Debug.LogError("Logic Script reference is missing!");
            }
        }
    }
}