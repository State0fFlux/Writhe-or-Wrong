using UnityEngine;

public class Stamps : MonoBehaviour
{
    public int multiplier; // The multiplier associated with the stamp type (negative for veto, positive for expedite)
    public int index; //0=yes, 1=no, 2=super yes, 3=super no.
    //private MazeMaster mazeMaster;

    void Start()
    {
        //mazeMaster = FindFirstObjectByType<MazeMaster>();  // Find the MazeMaster to access respawning and scoring
    }

    // Trigger when the player collides with the item
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectItem();  // Call the collect item function
        }
        LawScroller.lawStamped = true;
    }

    // Handle item collection
    void CollectItem()
    {
        // making it invisible (will be destroyed by MazeMaster in like 2 seconds)
        this.gameObject.SetActive(false);
        //this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        //this.gameObject.GetComponent<Renderer>().enabled = false;
        // Add to player's score (using a method in the MazeManager or PlayerManager)
        MazeMaster.AddScore(multiplier);

        // Notify the MazeManager to respawn the items
        //MazeMaster.RespawnItems();
    }

}
