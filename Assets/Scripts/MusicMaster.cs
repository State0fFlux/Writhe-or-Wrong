using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicMaster : MonoBehaviour
{
    private static MusicMaster instance;

    public AudioClip lobbyMusic;  // Music for Lobby, Controls, and Credits
    public AudioClip gameMusic;   // Music for the Game
    private AudioSource audioSource;

    void Awake()
    {
        // Singleton pattern to ensure only one MusicManager instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy this object when loading new scenes
            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;  // Ensure the music loops

            // Subscribe to the sceneLoaded event to update music when the scene changes
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Update music whenever a scene is loaded
        UpdateMusic();
    }

    void UpdateMusic()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "LobbyScene" || sceneName == "ControlsScene" || sceneName == "CreditsScene")
        {
            // Ensure the lobby music plays when entering a menu scene
            if (audioSource.resource != lobbyMusic)
            {
                audioSource.resource = lobbyMusic;
                audioSource.Play();
                Debug.Log("Playing Lobby Music");
            }
        }
        else if (sceneName == "StartGame" || sceneName == "PrologueScene")
        {
            // Change to game music for the gameplay scenes
            if (audioSource.resource != gameMusic)
            {
                audioSource.resource = gameMusic;
                audioSource.Play();
                Debug.Log("Playing Game Music");
            }
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from the scene loaded event when the MusicManager is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
