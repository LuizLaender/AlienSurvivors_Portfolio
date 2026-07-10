using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float deceleration;
    [SerializeField] float screenPaddingLeft;
    [SerializeField] float screenPaddingRight;
    [SerializeField] float screenPaddingBottom;
    [SerializeField] float screenPaddingTop;

    Vector2 rawInput;
    Vector2 minBounds;
    Vector2 maxBounds;
    Vector2 playerPosition;
    Vector2 currentVelocity;

    PlayerShooter playerShooter;

    bool isPaused;

    void Awake()
    {
        playerShooter = GetComponent<PlayerShooter>();
    }

    void Start()
    {
        InitBounds();
    }

    void Update()
    {
        rawInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Debug.Log("raw: " +rawInput);

        Move();

        isPaused = FindObjectOfType<LevelManager>().GetComponent<PauseMenu>().GetIsPaused();
        playerPosition = transform.position;

        if (playerShooter != null && !isPaused)
        {
            playerShooter.SetIsFiring(Input.GetButton("Fire1"));
        }
    }

    public Vector2 GetPlayerPosition()
    {
        return playerPosition;
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Move()
    {
        Vector2 targetVelocity = rawInput.normalized * moveSpeed;
        currentVelocity = Vector2.Lerp(currentVelocity, targetVelocity, deceleration);

        Vector2 delta = currentVelocity * Time.deltaTime;
        Vector2 newPos = new Vector2();

        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + screenPaddingLeft, maxBounds.x - screenPaddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + screenPaddingBottom, maxBounds.y - screenPaddingTop);

        transform.position = newPos;
    }
}
