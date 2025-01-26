using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class MazeMaster : MonoBehaviour
{
    public List<Room> rooms;  // List of all Room references
    public Stamps[] items; // Array of 4 distinct items (GameObjects)
    private List<int> availableRooms;  // Track rooms that can spawn items
    private List<Stamps> spawnedItems = new List<Stamps>();  // List of spawned items
    public int playerScore = 0;
    
    
    void Start()
    {
        SpawnItems();
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
        playerScore += multiplier;
        Debug.Log("Score: " + playerScore);  // Output the current score for testing
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
