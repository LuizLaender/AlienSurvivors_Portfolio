using UnityEngine;

public class PowerUp_Shield : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && gameObject.tag == "Power_Shield")
        {
            FindObjectOfType<PowerUpManager>().GetComponent<PowerUpManager>().PowerUp("Shield");
            Destroy(gameObject);
        }
    }
}

