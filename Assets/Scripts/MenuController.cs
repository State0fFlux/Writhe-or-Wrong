using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene management

public class MenuController : MonoBehaviour
{
    // This method will be called when the "Start Game" button is clicked
    public void StartGame()
    {
        // Load the "Game" scene
        SceneManager.LoadScene("GameScene");
    }

        // This method will be called when the "Controls" button is clicked
    public void ViewControls()
    {
        // Load the "Game" scene
        SceneManager.LoadScene("ControlsScene");
    }

        // This method will be called when the "Credits" button is clicked
    public void ViewCredits()
    {
        // Load the "Game" scene
        SceneManager.LoadScene("CreditsScene");
    }

        public void ViewLobby()
    {
        // Load the "Game" scene
        SceneManager.LoadScene("LobbyScene");
    }
}
