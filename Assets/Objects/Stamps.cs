using UnityEngine;

public class Stamps : MonoBehaviour
{
    public int multiplier; // The multiplier associated with the stamp type (negative for veto, positive for expedite)
    public int index; //0=super yes, 1=yes, 2=no, 3=super no.
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
        Destroy(this.gameObject);
        // Add to player's score (using a method in the MazeManager or PlayerManager)
        MazeMaster.AddScore(multiplier);

        // Notify the MazeManager to respawn the items
        MazeMaster.RespawnItems();
    }

}
