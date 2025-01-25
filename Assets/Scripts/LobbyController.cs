using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene management

public class LobbyController : MonoBehaviour
{
    // This method will be called when the "Play Game" button is clicked
    public void StartGame()
    {
        // Load the "Game" scene
        SceneManager.LoadScene("GameScene");
    }
}
