using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Player:")]
    [SerializeField] AudioClip playerShooting;
    [SerializeField] float playerShootingVolume = 1f;
    [SerializeField] AudioClip playerHurt;
    [SerializeField] float playerHurtVolume = 2f;
    [SerializeField] AudioClip playerDeath;
    [SerializeField] float playerDeathVolume = 2f;

    [Header("Health packs")]
    [SerializeField] AudioClip healthPack;
    [SerializeField] float healthPackVolume = 2f;

    [Header("Enemy killed")]
    [SerializeField] AudioClip enemyKilled;
    [SerializeField] float enemyKilledVolume = 1f;

    [Header("Power ups")]
    [SerializeField] AudioClip shieldUp;
    [SerializeField] AudioClip shieldDown;
    [SerializeField] AudioClip shieldLose;
    [SerializeField] float shieldUpVolume = 1f;
    [SerializeField] float shieldDownVolume = 1f;
    [SerializeField] float shieldLoseVolume = 1f;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayShootingSFX()
    {
        PlayClip(playerShooting, playerShootingVolume);
    }

    public void PlayHealthPackSFX()
    {
        PlayClip(healthPack, healthPackVolume);
    }

    public void PlayPlayerHurtSFX()
    {
        PlayClip(playerHurt, playerHurtVolume);
    }

    public void PlayPlayerDeathSFX()
    {
        PlayClip(playerDeath, playerDeathVolume);
    }

    public void PlayShieldUp()
    {
        PlayClip(shieldUp, shieldUpVolume);
    }

    public void PlayShieldDown()
    {
        PlayClip(shieldDown, shieldDownVolume);
    }

    public void PlayEnemyKilledSFX()
    {
        PlayClip(enemyKilled, enemyKilledVolume);
    }

    public void PlayShieldLose()
    {
        PlayClip(shieldLose, shieldLoseVolume);
    }

    void PlayClip(AudioClip audioClip, float volume)
    {
        if (audioClip != null)
        {
            audioSource.PlayOneShot(audioClip, volume);
        }
    }
}
