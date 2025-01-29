using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioClip lobbyMusic;
    public AudioClip gameMusic;

    private AudioSource audioSource;
    private static MusicManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.playOnAwake = false;
            SceneManager.sceneLoaded += OnSceneLoaded;
            PlayMusicForScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates when reloading the first scene
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    void PlayMusicForScene(string sceneName)
    {
        if (IsLobbyScene(sceneName) && audioSource.clip != lobbyMusic)
        {
            audioSource.clip = lobbyMusic;
            audioSource.Play();
        }
        else if (IsGameScene(sceneName) && audioSource.clip != gameMusic)
        {
            audioSource.clip = gameMusic;
            audioSource.Play();
        }
    }

    bool IsLobbyScene(string sceneName)
    {
        return sceneName == "LobbyScene" || sceneName == "ControlsScene" || sceneName == "CreditsScene";
    }

    bool IsGameScene(string sceneName)
    {
        return sceneName == "GameScene" || sceneName == "VictoryScene" || sceneName == "DefeatScene" || sceneName == "InsanityScene";
    }
}
