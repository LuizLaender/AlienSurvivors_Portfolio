using UnityEngine;

public class PowerUp_FireRate : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && gameObject.tag == "Power_FireRate")
        {
            FindObjectOfType<PowerUpManager>().GetComponent<PowerUpManager>().PowerUp("FireRate");
            Destroy(gameObject);
        }
    }
}

