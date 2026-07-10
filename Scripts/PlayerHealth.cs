using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    LevelManager levelManager;
    AudioPlayer audioPlayer;
    CameraShake cameraShake;
    float playerHealth = 4;
    float playerShields;
    float playerHealthAndShield;
    int shieldSpriteIndex = 1;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>().GetComponent<AudioPlayer>();
        levelManager = FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    public void Heal(int healAmount)
    {
        playerHealth += healAmount;
        FindObjectOfType<HealthAndScoreUi>().SetUiHP(playerHealth);
    }

    public void SetPlayerShields(int index)
    {
        playerShields = index;
    }

    public void KillPlayer()
    {
        audioPlayer.PlayPlayerDeathSFX();
        levelManager.LoadGameOver();
        Destroy(gameObject);
    }

    public float GetPlayerHealth()
    {
        return playerHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyProjectiles projectile = other.GetComponent<EnemyProjectiles>();

        if (projectile != null && other.tag == "Projectiles")
        {
            PlayerTakeDamage(projectile.GetProjectileDamage());
            ShakeCamera();
            projectile.DestroyProjectile();

            PowerUpManager powerUpManager = FindObjectOfType<PowerUpManager>();
            powerUpManager.shieldSpriteIndex += shieldSpriteIndex;

            if(powerUpManager.shieldSpriteIndex >= 3)
            {
                powerUpManager.playerShield.SetActive(false);
            }
            else
            {
                powerUpManager.shieldSpriteRenderer.sprite = powerUpManager.shieldSprites[powerUpManager.shieldSpriteIndex];
            }
        }
    }

    void PlayerTakeDamage(float index)
    {
        if(playerShields > 0)
        {
            switch(playerShields)
            {
                case 3:
                    audioPlayer.PlayShieldDown();
                    break;

                case 2:
                    audioPlayer.PlayShieldDown();
                    break;

                case 1:
                    audioPlayer.PlayShieldLose();
                    break;

                default:
                    Debug.Log("shield error, check PlayerTakeDamage() in PlayerHealth.cs");
                    break;
            }
            playerShields--;
            return;
        }

        playerHealth -= index;

        if (playerHealth <= 0)
        {
            KillPlayer();
        }
        else
        {
            audioPlayer.PlayPlayerHurtSFX();
        }

        FindObjectOfType<HealthAndScoreUi>().SetUiHP(playerHealth);        
    }
    
    void ShakeCamera()
    {
        if (cameraShake != null)
        {
            cameraShake.Play();
        }
    }
}
