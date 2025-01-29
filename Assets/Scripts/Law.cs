using UnityEngine;

public class Law : MonoBehaviour
{
    public float scrollSpeed;  // Control the speed of scrolling
    public int value;
    public Vector3 direction;

    void Start()
    {
        // Remember the initial position of the sprite
    }

    void Update()
    { 
        transform.position += direction * scrollSpeed * Time.deltaTime;

        // Check if the image has fully scrolled out of the screen
        if (transform.position.y > Screen.height) 
        {
            // Trigger flag to deduct score or other event
            OnImageScrolledOut();
        }

}

    // Called when the image scrolls out of the screen
    void OnImageScrolledOut()
    {
        // Your flag to deduct score
        // Example: ScoreManager.Instance.DeductScore();

        // Optionally destroy the object or disable it
        Destroy(gameObject); // Remove the object from the scene
    }
}
