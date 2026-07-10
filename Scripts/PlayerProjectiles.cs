using UnityEngine;

public class PlayerProjectiles : MonoBehaviour
{
    [SerializeField] float projectileDamage = 1;
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
