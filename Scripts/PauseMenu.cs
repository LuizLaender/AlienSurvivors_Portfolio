using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] bool isPaused;
    [SerializeField] GameObject pauseMenu;
    AudioSource audioSource;
    float startingVolume;

    void Awake()
    {
        audioSource = FindObjectOfType<AudioPlayer>().GetComponent<AudioSource>();
    }

    void Start()
    {
        pauseMenu.SetActive(false);
        startingVolume = audioSource.volume;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel")) // "Cancel" usually corresponds to the Escape key
        {
            Debug.Log("Esc pressed");
            if (isPaused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    public bool GetIsPaused()
    {
        return isPaused;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        if (audioSource != null)
        {
            audioSource.volume = startingVolume / 3;
        }
        FindObjectOfType<PlayerMovement>().GetComponent<PlayerShooter>().SetIsFiring(false);
    }

    public void Unpause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        if (audioSource != null)
        {
            audioSource.volume = startingVolume;
        }
    }
}
