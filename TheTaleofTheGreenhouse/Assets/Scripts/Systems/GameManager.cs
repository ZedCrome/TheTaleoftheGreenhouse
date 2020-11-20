using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{

    public static GameManager instance;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;
    
    public enum GameState 
    {

        Menu,
        LoadLevel,
        LevelLoaded,
        GameLoop,
        PauseGame,
        GameOver,
        Victory
    }
    
    [Header("GameState")]
    [Space(20)]
    public GameState currentGameState;
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

        switch( currentGameState ) 
        {

            case GameState.Menu:

                break;

            case GameState.LoadLevel:

                break;

            case GameState.LevelLoaded:

                break;

            case GameState.GameLoop:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ChangeGameState(GameState.PauseGame);
                }
                
                break;

            case GameState.PauseGame:
                
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ChangeGameState(GameState.GameLoop);
                }

                break;

            case GameState.GameOver:

                break;

            case GameState.Victory:

                break;

            default:
                break;
        }
        
    }

    public void ChangeGameState( GameState newGameState ) 
    {

        if( currentGameState == newGameState ) 
        {
            return;
        }

        if( newGameState == GameState.Menu ) 
        {
            
        }

        if ( newGameState == GameState.LoadLevel ) 
        {

        }

        if ( newGameState == GameState.LevelLoaded ) 
        {
            
        }

        if( newGameState == GameState.GameLoop ) 
        {
            mainMenu.SetActive(false);
            pauseMenu.SetActive(false);
        }

        if ( newGameState == GameState.PauseGame ) 
        {
            pauseMenu.SetActive(true);
        }

        if( newGameState == GameState.GameOver ) 
        {
            
        }

        if (newGameState == GameState.Victory) 
        {

        }

        currentGameState = newGameState;
    }

    public string GetActiveScene() 
    {

        return SceneManager.GetActiveScene().name;
    }
}
