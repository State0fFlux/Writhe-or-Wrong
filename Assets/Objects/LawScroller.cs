using UnityEngine;
using System.Collections;

public class LawScroller : MonoBehaviour
{
    public GameObject[] laws;  // Array of PNG GameObjects (Sprites)
    private int currentIndex = 0;
    public static bool lawStamped = false;
    public float timePerImage = 45f;

    private void Start()
    {
        StartCoroutine(ScrollLaws());
    }

    private IEnumerator ScrollLaws()
    {
        while (true)
        {
            GameObject currentLaw = Instantiate(laws[currentIndex], new Vector3(120, -140 + 30, 0), Quaternion.identity); // Start offscreen
            SpriteRenderer spriteRenderer = currentLaw.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -50;
            currentLaw.transform.localScale = new Vector3(7, 7, 0);
            currentLaw.transform.SetParent(transform); // Ensure the image is a child of the manager

            // Calculate screen and image dimensions
            float screenHeight = Camera.main.orthographicSize * 2; // Full screen height in world units
            float imageHeight = spriteRenderer.bounds.size.y;  // Consider scale

            // Dynamically calculate scroll speed
            float scrollSpeed = (screenHeight + imageHeight) / timePerImage * 1.25f; // had to manually adjust, there's gotta be a better mathematical approach to this

            // Start scrolling this image
            Law scroller = currentLaw.GetComponent<Law>();
            scroller.direction = Vector3.up;
            scroller.scrollSpeed = scrollSpeed;  // Set scrolling speed as desired
            
            //Debug.Log(currentIndex);
            while (scroller != null)
            {
                // Yielding null makes it wait until the next frame, effectively checking each frame if the object is destroyed
                if (lawStamped)
                {
                    scroller.direction = Vector3.right;
                    scroller.scrollSpeed = 333;
                    yield return new WaitForSeconds(2);
                    Destroy(scroller);
                    lawStamped = false;
                }
                if (scroller.transform.position.y > 200) 
                {
                    Destroy(scroller);  // Remove it from the scene
                    MazeMaster.timerRunsOut();
                }
                
                yield return null;
            }

            Destroy(currentLaw);  // Remove current image from the screen
            currentIndex++;  // Move to the next image
        }
    }
}
