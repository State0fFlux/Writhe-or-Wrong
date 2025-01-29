using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MazeMaster : MonoBehaviour
{
    public List<Room> rooms;  // List of all Room references
    public Stamps[] items; // Array of 4 distinct items (GameObjects)
    private List<int> availableRooms;  // Track rooms that can spawn items
    private List<Stamps> spawnedItems = new List<Stamps>();  // List of spawned items
    public int performance = 50; // worm's performance
    public int sanity = 100; // host's sanity
    private const float respawnInterval = 45f; // Time interval for respawn
    private const int sanityPenalty = 25; // determines the amount of sanity taken off per miss
    private float timer = 0f;

    public TextMeshProUGUI timerText;
    public Slider sanityMeter;
    public Slider performanceMeter;
    
    
    void Start()
    {
        SpawnItems();
    }

    // Update is called once per frame
    void Update()
    {
        if (sanity <= 0) {
            SceneManager.LoadScene("InsanityEndScene");
        } else if (performance >= 100) {
            SceneManager.LoadScene("VictoryEndScene");
        } else if (performance <= 0) {
            SceneManager.LoadScene("DefeatEndScene");
        }

        timer += Time.deltaTime; // Increment timer


        // Check if the timer exceeds the respawn interval
        if (timer >= respawnInterval) {
            sanity -= 25; // Reduce sanity
            Debug.Log("Sanity: " + sanity);
            LawScroller.lawStamped = true;
            RespawnItems();
            timer = 0f; // reset timer
        }
        UpdateUI();
    }

    void SpawnItems()
    {
        List<int> itemIndices = new List<int>{0, 1, 2, 3};
        availableRooms=new List<int>();
        for (int i=0;i<=16;i++){
            availableRooms.Add(i);
        }
        // Randomize the order of rooms
        ShuffleList(availableRooms);
        // Randomize the order of items
        ShuffleList(itemIndices);

        // Spawn items in random rooms
        for (int i = 0; i < 4; i++)
        {
            int roomIndex = availableRooms[i];  // Select a random room
            Stamps item = items[itemIndices[i]];  // Select a random item
            
            // Instantiate the item in the selected room's position
            Stamps spawnedItem = Instantiate(item, rooms[roomIndex].transform.position, Quaternion.identity);
            spawnedItems.Add(spawnedItem);
            // Mark the room as occupied
            rooms[roomIndex].isOccupied = true;
        }
    }

    // Helper function to shuffle lists randomly
    void ShuffleList<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    // Respawn items in new random rooms
    public void RespawnItems()
    {
        // Destroy all currently spawned items
        foreach (Stamps item in spawnedItems)
        {
            Debug.Log("Destroyed "+item.index);
            Destroy(item.gameObject);  // Destroy the collected item
        }
        
        spawnedItems.Clear();  // Clear the list of spawned items
        
        // Spawn new items in random rooms
        SpawnItems();
    }

    public void AddScore(int multiplier)
    {
        performance += 10 * multiplier;
        Debug.Log("Score: " + performance);  // Output the current score for testing
        timer = 0f;
    }

        void UpdateUI()
    {
        // Update sanity and performance meters
        sanityMeter.value = sanity;
        performanceMeter.value = performance;
        float timeLeft = respawnInterval - timer;
        if (timeLeft <= 10f - 1) { // single digits
            timerText.text = "00:0" + Mathf.Ceil(respawnInterval - timer).ToString();
        } else {
            timerText.text = "00:" + Mathf.Ceil(respawnInterval - timer).ToString();
        }
    }
}
