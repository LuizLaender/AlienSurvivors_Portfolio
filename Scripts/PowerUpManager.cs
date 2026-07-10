using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour
{
    [Header("FireRate")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireRateDuration = 5f;
    [SerializeField] float fireRate = 0.05f;

    [Header("Shield: ")]
    [SerializeField] public GameObject playerShield;
    [SerializeField] public List<Sprite> shieldSprites;
    [SerializeField] public int shieldAmount = 3;
    [HideInInspector] public SpriteRenderer shieldSpriteRenderer;
    [HideInInspector] public int shieldSpriteIndex = 0;

    SpriteRenderer bulletSprite;
    PlayerShooter playerShooter;
    PlayerHealth playerHealth;
    AudioPlayer audioPlayer;
    float fireRateAtStart;

    void Awake()
    {
        shieldSpriteRenderer = playerShield.GetComponent<SpriteRenderer>();
        playerShooter = FindObjectOfType<PlayerMovement>().GetComponent<PlayerShooter>();
        bulletSprite = bulletPrefab.GetComponent<SpriteRenderer>();
        playerHealth = FindObjectOfType<PlayerHealth>().GetComponent<PlayerHealth>();
        audioPlayer = FindObjectOfType<AudioPlayer>().GetComponent<AudioPlayer>();
    }

    void Start()
    {
        bulletSprite.color = Color.white;
        fireRateAtStart = playerShooter.GetStartingFireRate();
        playerShield.SetActive(false);
    }

    public void PowerUp(string value)
    {
        switch (value)
        {
            case "FireRate":
                StartCoroutine(UpFireRate());
                break;

            case "Shield":
                StartCoroutine(Shield());
                break;
        }
    }

    IEnumerator UpFireRate()
    {
        playerShooter.SetFireRate(fireRate);

        bulletSprite.color = Color.yellow;

        yield return new WaitForSeconds(fireRateDuration);

        playerShooter.SetFireRate(fireRateAtStart);
        bulletSprite.color = Color.white;
    }

    IEnumerator Shield()
    {
        shieldSpriteIndex = 0;

        playerShield.SetActive(true);

        audioPlayer.PlayShieldUp();

        playerHealth.SetPlayerShields(shieldAmount);

        shieldSpriteRenderer.sprite = shieldSprites[shieldSpriteIndex];

        yield return new WaitForSeconds(0);
    }
}
