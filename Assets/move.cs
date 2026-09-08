using UnityEngine;

public class move : MonoBehaviour
{
    public float moveSpeed = 5;
    public float deadZone = -10;
    // Update is called once per frame
    void Update()
    {
        // Stop movement if the game is over
        if (logicScript.isGameOver) return;
        
        transform.position = transform.position + (Vector3.left * moveSpeed * Time.deltaTime);
        if (transform.position.x < deadZone)
        {
            Debug.Log("pipe deleted");
            Destroy(gameObject);
        }
    }
}
