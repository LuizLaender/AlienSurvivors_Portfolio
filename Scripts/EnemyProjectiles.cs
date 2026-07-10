using UnityEngine;

public class EnemyProjectiles : MonoBehaviour
{
    float projectileDamage;
    float projectileVelocity;

    public void SetProjectileDamage(float index)
    {
        projectileDamage = index;
    }

    public float GetProjectileDamage()
    {
        return projectileDamage;
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
