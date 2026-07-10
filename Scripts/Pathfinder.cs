using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Rigidbody2D rb;
    float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10);
    }

    void Update()
    {
        Move();
    }

    public void SetMoveSpeed(float index)
    {
        speed = index;
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(-speed,0);
    }
}
