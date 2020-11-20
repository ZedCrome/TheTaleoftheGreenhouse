using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed = 5;
    [SerializeField] private Vector2 rotationOffset;
    private PlayerRenderer playerRenderer;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponentInChildren<PlayerRenderer>();
    }
    
    
    void FixedUpdate()
    {
        Vector2 currentPosition = rb2d.position;
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInpunt = Input.GetAxis("Vertical");

        Vector2 inputVector = new Vector2(horizontalInput, verticalInpunt);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        
        Vector2 movement = inputVector * speed;
        Vector2 newPosition = currentPosition + movement * Time.fixedDeltaTime;
        
        playerRenderer.SetDirection(movement);
        
        rb2d.MovePosition(newPosition);
        
        
    }
}
