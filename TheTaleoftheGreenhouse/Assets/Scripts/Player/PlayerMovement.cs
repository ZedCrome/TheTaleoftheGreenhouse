using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private PlayerRenderer playerRenderer;
    public Vector2 movement;
    private Rigidbody2D rb2d;
    private Vector2 newPosition;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponentInChildren<PlayerRenderer>();
    }
    
    
    void Update()
    {
        if (GameManager.instance.currentGameState != GameManager.GameState.GameLoop)
        {
            return;
        }
        
        
        Vector2 currentPosition = rb2d.position;
        
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInpunt = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(horizontalInput, verticalInpunt * 0.5f).normalized;
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        
        movement = inputVector * speed;
        newPosition = currentPosition + (movement / 225);
        
        playerRenderer.SetDirection(movement);
        
        
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(newPosition);
    }
}
