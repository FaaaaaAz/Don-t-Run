using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    /* El Script GameControl se encarga del control durante todo el juego, iniciar un nuevo juego,
     * colocar al personaje, distribuir las piezas en el laberinto, iniciar el timer, etc.
     * 
     */

    [SerializeField]
    private GameObject[] objectsToFindPrefabs;

    private GameObject[] objectsToFindInstances;

    [SerializeField]
    private Transform keySpawnPoint;

    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject scenePlayer;
    private GameObject playerInstance;
    private string appearPositionsTag = "AppearPosition";
    private Vector3 scenePlayerStartPosition;
    private Quaternion scenePlayerStartRotation;
    private bool hasScenePlayer;
    [SerializeField]
    private bool keyFound;
    private UserInterface userInterface;

    public enum GameState { MAINMENU,PLAYING,PAUSED,ENDSCREEN};
    private GameState gameState;
    [SerializeField]
    private bool gamePaused;

    private AudioManager audioManager;

    private Timer timer;
    private Gate gate;

    public AudioSource backgrounMusic;

    private IEnumerator Start()
    {
        userInterface = FindObjectOfType<UserInterface>();
        audioManager = FindObjectOfType<AudioManager>();
        timer = FindObjectOfType<Timer>();
        gate = FindObjectOfType<Gate>();

        if (scenePlayer == null)
        {
            scenePlayer = GameObject.Find("PlayerVariant");
        }

        if (scenePlayer == null)
        {
            try
            {
                scenePlayer = GameObject.FindGameObjectWithTag("Player");
            }
            catch (UnityException)
            {
                scenePlayer = null;
            }
        }

        if (scenePlayer != null)
        {
            hasScenePlayer = true;
            scenePlayerStartPosition = scenePlayer.transform.position;
            scenePlayerStartRotation = scenePlayer.transform.rotation;
            playerInstance = scenePlayer;
        }

        if (backgrounMusic != null && !backgrounMusic.isPlaying)
        {
            backgrounMusic.Play();
        }

        yield return null;
        SetMainMenu();

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            NewGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && (gameState == GameState.PLAYING || gameState == GameState.PAUSED))
        {
            SetPauseState(!gamePaused);
        }
    }

    public void NewGame()
    {

        gamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gate.NewGame();
        userInterface.NewGame();
        gameState = GameState.PLAYING;
        keyFound = false;
        PlacePlayer();
        PlaceObjectsToFind();
        timer.StartTimer();
    }

    public void SetMainMenu()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameState = GameState.MAINMENU;
        userInterface.SetMenuScreen();
        RemoveCurrentPlayer();
        timer.StopTimer();
    }

    public void SetEndScreen(bool victory)
    {
        
        RemoveCurrentPlayer();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameState = GameState.ENDSCREEN;
        userInterface.SetEndScreen(victory);
        timer.StopTimer();
    }

    public void GameWon()
    {
        SetEndScreen(true);
    }

    public void GameLost()
    {
        SetEndScreen(false);
    }

    public void SetPauseState(bool pause)
    {
        gamePaused = pause;
        
        if (gamePaused)
        {
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            userInterface.SetPauseScreen();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            userInterface.SetGameScreen();
        }

    }

    private void PlacePlayer()
    {
        if (hasScenePlayer)
        {
            playerInstance = scenePlayer;
            playerInstance.SetActive(true);
            playerInstance.transform.SetPositionAndRotation(scenePlayerStartPosition, scenePlayerStartRotation);
            EnsureScenePlayerCameraIsActive();

            Rigidbody playerRigidbody = playerInstance.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                playerRigidbody.linearVelocity = Vector3.zero;
                playerRigidbody.angularVelocity = Vector3.zero;
            }
            return;
        }

        if (playerInstance != null)
        {
            Destroy(playerInstance);
        }

        GameObject p = GameObject.FindGameObjectWithTag(appearPositionsTag);
        if (p == null)
        {
            return;
        }

        playerInstance = Instantiate(playerPrefab,p.transform.position,playerPrefab.transform.rotation);
    }

    private void EnsureScenePlayerCameraIsActive()
    {
        if (scenePlayer == null)
        {
            return;
        }

        Camera playerCamera = scenePlayer.GetComponentInChildren<Camera>(true);
        if (playerCamera == null)
        {
            return;
        }

        playerCamera.gameObject.SetActive(true);
        playerCamera.tag = "MainCamera";

        AudioListener[] listeners = FindObjectsOfType<AudioListener>(true);
        foreach (AudioListener listener in listeners)
        {
            bool isPlayerListener = playerCamera.GetComponent<AudioListener>() == listener;
            listener.enabled = isPlayerListener;
        }
    }

    private void RemoveCurrentPlayer()
    {
        if (hasScenePlayer)
        {
            if (scenePlayer != null)
            {
                scenePlayer.SetActive(false);
            }
            return;
        }

        if (playerInstance != null)
        {
            Destroy(playerInstance);
            playerInstance = null;
        }
    }


    private void PlaceObjectsToFind()
    {
        if (objectsToFindInstances != null)
        {
            foreach(GameObject g in objectsToFindInstances)
            {
                Destroy(g);
            }
        }
        LabyrinthPiece[] labyrinthPieces = FindObjectsOfType<LabyrinthPiece>();
        if (labyrinthPieces.Length == 0 || objectsToFindPrefabs == null || objectsToFindPrefabs.Length == 0)
        {
            objectsToFindInstances = null;
            return;
        }

        int keyIndex = objectsToFindPrefabs.Length - 1;
        Vector3 keyPosition;
        if (keySpawnPoint != null)
        {
            keyPosition = keySpawnPoint.position;
        }
        else
        {
            keyPosition = labyrinthPieces[Random.Range(0, labyrinthPieces.Length)].GetRandomPosition();
        }

        GameObject keyInstance = Instantiate(objectsToFindPrefabs[keyIndex], keyPosition, objectsToFindPrefabs[keyIndex].transform.rotation);
        keyInstance.transform.position += 0.5f * Vector3.up;

        objectsToFindInstances = new GameObject[1] { keyInstance };
    }

    public void ItemFound(Collectable.Item item)
    {
        audioManager.ItemCollected();
        userInterface.ItemFound(item);
        if (item == Collectable.Item.KEY)
        {
            keyFound = true;
            userInterface.KeyReleased();
        }
    }

    public bool PlayerHasKey()
    {
        return keyFound;
    }

    public bool IsGamePaused()
    {
        return gamePaused;
    }
    
    public void TimeIsUp()
    {
        //timer reached 0
        GameLost();
    }

    public void ClickToStartButton()
    {
        NewGame();
    }



}
