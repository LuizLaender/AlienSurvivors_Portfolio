using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    Coroutine firingCoroutine;

    [SerializeField] GameObject bulletPrefab;
    float bulletVelocity = 8.1f;
    float damage = 1f;
    bool isFiring;

    //fire rate:
    float interval = 1.5f;
    float variance = 0.4f;
    float minimum = 1f;

    void Start()
    {
        isFiring = true;
    }

    void Update()
    {
        Fire();
    }

    public void SetBulletSpeed(float index)
    {
        bulletVelocity = index;
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
            float randomFireRate = Random.Range(interval - variance,
                                                interval + variance);

            yield return new WaitForSeconds(Mathf.Clamp(randomFireRate,
                                                        minimum,
                                                        float.MaxValue));

            var projectileInstance = Instantiate(bulletPrefab,
                                        transform.position, 
                                        Quaternion.Euler(0, 0, transform.eulerAngles.z),
                                        GameObject.Find("Bullets").gameObject.transform);

            if (projectileInstance != null)
            {
                Rigidbody2D bulletRigidBody = projectileInstance.GetComponent<Rigidbody2D>();
                projectileInstance.GetComponent<EnemyProjectiles>().SetProjectileDamage(damage);

                if(bulletRigidBody != null)
                {
                    projectileInstance.transform.rotation = Quaternion.identity;
                    bulletRigidBody.linearVelocity = Vector2.left * bulletVelocity;

                    Destroy(projectileInstance, 10);
                }
            }
        }
    }
}
