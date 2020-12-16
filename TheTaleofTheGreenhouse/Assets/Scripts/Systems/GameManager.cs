using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{

    public static GameManager instance;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    
    
    public enum GameState 
    {

        Menu,
        GameNight,
        GameLoop,
        Options,
        PauseGame,
        ShopMenu,
        GameOver,
        Victory
    }
    
    [Header("GameState")]
    [Space(20)]
    public GameState currentGameState;
    public GameState previousGameState;
    [Space(10)]
    [Header("Options")]
    public bool debugActive;

    void Awake() 
    {

        DontDestroyOnLoad(this.gameObject);

        if( instance == null ) {

            instance = this;
        } else {

            Destroy( this.gameObject );
        }
    }
    
    
    void Update() 
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D))
        {
            debugActive = !debugActive;
        }

        if (debugActive)
        {
            Debugger.instance.Log("Debug", " Active");
        }
        
        switch( currentGameState ) 
        {

            case GameState.Menu:

                break;

            case GameState.GameNight:

                break;

            case GameState.GameLoop:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    previousGameState = GameState.GameLoop;
                    ChangeGameState(GameState.PauseGame);
                }
                
                break;
            
            case GameState.Options:
                if (currentGameState == GameState.Options)
                {
                    ChangeGameState(GameState.Options);
                }
                
                break;

            case GameState.PauseGame:
                
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    previousGameState = GameState.PauseGame;
                    ChangeGameState(GameState.GameLoop);
                }

                break;

            case GameState.ShopMenu:

                break;

            case GameState.GameOver:

                break;

            case GameState.Victory:

                break;

            default:
                break;
        }
    }

    
    public event Action<GameState> onChangeGameSate;
    public void ChangeGameState( GameState newGameState ) 
    {

        if( currentGameState == newGameState ) 
        {
            return;
        }

        if( newGameState == GameState.Menu ) 
        {
            mainMenu.SetActive(true);
            pauseMenu.SetActive(false);
            optionsMenu.SetActive(false);
            previousGameState = GameState.Menu;
        }

        if ( newGameState == GameState.GameNight ) 
        {

        }
        
        if( newGameState == GameState.GameLoop ) 
        {
            mainMenu.SetActive(false);
            pauseMenu.SetActive(false);
            optionsMenu.SetActive(false);
        }

        if ( newGameState == GameState.Options )
        {
            mainMenu.SetActive(false);
            pauseMenu.SetActive(false);
            optionsMenu.SetActive(true);

        }

        if ( newGameState == GameState.PauseGame ) 
        {
            pauseMenu.SetActive(true);
            optionsMenu.SetActive(false);
            previousGameState = GameState.PauseGame;
        }

        if (newGameState == GameState.ShopMenu)
        {
            previousGameState = GameState.ShopMenu;
        }

        if ( newGameState == GameState.GameOver ) 
        {
            
        }

        if (newGameState == GameState.Victory)
        {
            SceneManager.LoadScene("OutroCinematic");
        }

        currentGameState = newGameState;
        onChangeGameSate?.Invoke(newGameState);
    }

    public string GetActiveScene() 
    {

        return SceneManager.GetActiveScene().name;
    }
}
