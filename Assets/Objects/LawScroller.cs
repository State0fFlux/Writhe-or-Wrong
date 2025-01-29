using UnityEngine;
using System.Collections;

public class LawScroller : MonoBehaviour
{
    public GameObject[] laws;  // Array of PNG GameObjects (Sprites)
    public float scrollSpeed = 30f;
    private int currentIndex = 0;
    public static bool lawStamped = false;
    //public float timePerImage = 20f;

    private void Start()
    {
        StartCoroutine(ScrollLaws());
    }

    private IEnumerator ScrollLaws()
    {
        while (true)
        {
            GameObject currentLaw = Instantiate(laws[currentIndex], new Vector3(130, -140, 0), Quaternion.identity); // Start offscreen
            currentLaw.transform.localScale = new Vector3(7, 7, 0);
            currentLaw.transform.SetParent(transform); // Ensure the image is a child of the manager

            // Start scrolling this image
            Laws scroller = currentLaw.GetComponent<Laws>();
            scroller.direction = Vector3.up;
            scroller.scrollSpeed = this.scrollSpeed;  // Set scrolling speed as desired
            
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

            Destroy(currentLaw.gameObject);  // Remove current image from the screen
            currentIndex++;  // Move to the next image
        }
    }
}
