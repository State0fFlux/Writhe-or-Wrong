using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MazeMaster : MonoBehaviour
{
    public static List<Room> rooms;  // List of all Room references
    public static Stamps[] items; // Array of 4 distinct items (GameObjects)
    public static List<int> availableRooms;  // Track rooms that can spawn items
    public static List<Stamps> spawnedItems = new List<Stamps>();  // List of spawned items
    public static int performance = 50; // worm's performance
    public static int sanity = 100; // host's sanity
    private const float respawnInterval = 45f; // Time interval for respawn
    private const int sanityPenalty = 25; // determines the amount of sanity taken off per miss
    public static float timer = 0f;

    public TextMeshProUGUI timerText;
    public Slider sanityMeter;
    public Slider performanceMeter;
    
    
    void Start()
    {
        rooms = new List<Room>();
        for (int i=1; i<=17; i++){
            string s = "Spawn Room ("+i+")";
            Room room = GameObject.Find(s).GetComponent<Room>();
            rooms.Add(room);
        }

        items = new Stamps[4];
        items[0] = GameObject.Find("Super Yes").GetComponent<Stamps>();
        items[1] = GameObject.Find("Yes").GetComponent<Stamps>();
        items[2] = GameObject.Find("No").GetComponent<Stamps>();
        items[3] = GameObject.Find("Super No").GetComponent<Stamps>();

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
            timerRunsOut();
        }
        UpdateUI();
    }

    public static void timerRunsOut(){
        sanity -= 25; // Reduce sanity
        Debug.Log("Sanity: " + sanity);
        LawScroller.lawStamped = true;
        RespawnItems();
        timer = 0f; // reset timer
    }

    public static void SpawnItems()
    {
        //Debug.Log("I've been called upon!");
        List<int> itemIndices = new List<int>{0, 1, 2, 3};
        availableRooms=new List<int>();
        for (int i=0;i<=16;i++){
            if (!rooms[i].GetComponent<BoxCollider2D>().bounds.Contains(GameObject.FindWithTag("Player").transform.position)){
                availableRooms.Add(i);
            }
            if (rooms[i].GetComponent<BoxCollider2D>().bounds.Contains(GameObject.FindWithTag("Player").transform.position)){
                Debug.Log("Disaster Prevented!");
            }
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
    public static void ShuffleList<T>(List<T> list)
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
    public static void RespawnItems()
    {
        // Destroy all currently spawned items
        foreach (Stamps item in spawnedItems)
        {
            Destroy(item.gameObject);  // Destroy the collected item
        }
        
        spawnedItems.Clear();  // Clear the list of spawned items
        
        // Spawn new items in random rooms
        SpawnItems();
    }

    public static void AddScore(int multiplier)
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
