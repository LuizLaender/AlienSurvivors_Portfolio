using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] ParticleSystem deathParticles;
    ScoreKeeper scoreKeeper;
    GameObject scoreUI;
    AudioPlayer audioPlayer;
    float enemyHealth;
    float rewardPoints;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>().GetComponent<ScoreKeeper>();
        scoreUI = GameObject.Find("Score");
        audioPlayer = FindObjectOfType<AudioPlayer>().GetComponent<AudioPlayer>();
    }

    public void KillEnemy()
    {
        scoreKeeper.SetScore(rewardPoints);
        audioPlayer.PlayEnemyKilledSFX();
        PlayEnemyKilledParticleFX();
        scoreUI.GetComponent<Animator>().Play("Score_AddPoints",0,0);
        Destroy(gameObject);
    }

    public void SetStats(float hp, float rw)
    {
        rewardPoints = rw;
        enemyHealth = hp;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerProjectiles projectile = other.GetComponent<PlayerProjectiles>();

        if (projectile != null && other.tag == "Projectiles")
        {
            TakeDamage(projectile.GetProjectileDamage());
            projectile.DestroyProjectile();
        }
    }

    void TakeDamage(float index)
    {
        enemyHealth -= index;

        // enemy hurt SFX
        StartCoroutine(HurtAnimation());

        if (enemyHealth <= 0)
        {
            KillEnemy();
        }
    }

    void PlayEnemyKilledParticleFX()
    {
        if (deathParticles != null)
        {
            ParticleSystem instance = Instantiate(deathParticles,
                                                    transform.position,
                                                    Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    IEnumerator HurtAnimation()
    {
        SpriteRenderer sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.08f);
        sprite.color = Color.white;
    }
}
