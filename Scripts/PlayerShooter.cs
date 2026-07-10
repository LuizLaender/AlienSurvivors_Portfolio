using System.Collections;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileSpread = 1f;
    [SerializeField] float fireRate = 0.1f;

    AudioPlayer audioPlayer;
    GameObject bulletInstance;
    Coroutine firingCoroutine;
    Animator animator;
    float projectileLifeTime = 5f;
    bool isFiring;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>().GetComponent<AudioPlayer>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Fire();
    }

    public float GetStartingFireRate() // used in PowerUpManager
    {
        return fireRate;
    }

    public void SetFireRate(float amount) // used in PowerUpManager
    {
        fireRate = amount;
    }

    public void SetIsFiring(bool value) // used in PauseMenu
    {
        isFiring = value;
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            audioPlayer.PlayShootingSFX();
            bulletInstance = Instantiate(projectilePrefab,
                                        transform.position,  
                                        Quaternion.Euler(0,0,270),
                                        GameObject.Find("Bullets").gameObject.transform);
            
            animator.Play("Shooting",0,0);

            if (bulletInstance != null)
            {
                Rigidbody2D bulletRigidBody = bulletInstance.GetComponent<Rigidbody2D>();

                if (bulletRigidBody != null)
                {
                    float randomAngle = Random.Range(-projectileSpread, projectileSpread);
                    Vector2 projectileDirection = Quaternion.Euler(0, 0, randomAngle) * transform.up;

                    bulletRigidBody.linearVelocity = projectileDirection * projectileSpeed;
                    Destroy(bulletInstance, projectileLifeTime);
                }
            }

            yield return new WaitForSeconds(fireRate);
        }
    }
}
