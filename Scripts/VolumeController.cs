using UnityEngine;

public class VolumeController : MonoBehaviour
{
    [SerializeField] GameObject isPlaying;
    [SerializeField] GameObject isMute;
    [SerializeField] AudioSource audioSource;
    float startingVolume;

    void Start()
    {
        startingVolume = audioSource.volume;
    }

    public void IsPlaying()
    {
        isPlaying.SetActive(false);
        isMute.SetActive(true);
        audioSource.volume = 0;
    }

    public void IsMute()
    {
        isPlaying.SetActive(true);
        isMute.SetActive(false);
        audioSource.volume = startingVolume;
    }
}
