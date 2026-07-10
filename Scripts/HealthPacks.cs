using UnityEngine;

public class HealthPacks : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0.1f;
    [SerializeField] int healAmount = 1;

    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>().GetComponent<AudioPlayer>();
    }
    
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && GameObject.Find("Player").GetComponent<PlayerHealth>().GetPlayerHealth() < 4)
        {
            FindObjectOfType<PlayerMovement>().GetComponent<PlayerHealth>().Heal(healAmount);
            audioPlayer.PlayHealthPackSFX();
            Destroy(gameObject);
        }
    }
}
