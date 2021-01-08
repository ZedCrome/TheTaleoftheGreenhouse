using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed = 5;
    private PlayerRenderer playerRenderer;
    public Vector2 movement;
    private Rigidbody2D rb2d;

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
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInpunt = Input.GetAxis("Vertical");

        Vector2 inputVector = new Vector2(horizontalInput, verticalInpunt);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        
         movement = inputVector * speed;
        Vector2 newPosition = currentPosition + movement * Time.deltaTime;
        
        playerRenderer.SetDirection(movement);
        
        rb2d.MovePosition(newPosition);
        
        
    }
}
