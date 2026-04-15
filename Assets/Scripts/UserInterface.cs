using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    /* Este Script se encarga de todo lo que tiene que ver con la muestra de información en pantalla,
     * El cambio entre las distintas pantallas, menú principal, juego, pantalla final y de pausa.
     * Mostrar los items encontrados, el temporizador, etc.
     * 
     */

    [Header("Screens")]

    [SerializeField]
    private GameObject menuScreen;
    [SerializeField]
    private GameObject gameScreen;
    [SerializeField]
    private GameObject endScreen;
    [SerializeField]
    private GameObject pauseScreen;
    [SerializeField]
    private GameObject menuCameraObject;

    [Header("Icons")]

    [SerializeField]
    private Image carIcon;
    [SerializeField]
    private Image cameraIcon;
    [SerializeField]
    private Image cannonIcon;
    [SerializeField]
    private Image balloonIcon;
    [SerializeField]
    private Image earthIcon;
    [SerializeField]
    private Image violinIcon;
    [SerializeField]
    private Image keyIcon;

    [Header("Objects")]

    [SerializeField]
    private Sprite[] car;
    [SerializeField]
    private Sprite[] camera;
    [SerializeField]
    private Sprite[] cannon;
    [SerializeField]
    private Sprite[] balloon;
    [SerializeField]
    private Sprite[] earth;
    [SerializeField]
    private Sprite[] violin;
    [SerializeField]
    private Sprite[] key;
    [SerializeField]
    private GameObject findMissingObjectsMessage;
    [SerializeField]
    private GameObject keyReleasedMessage;
    [SerializeField]
    private GameObject escapeMessage;
    [SerializeField]
    private GameObject objectsToFindPanel;
    [SerializeField]
    private GameObject keyPanel;

    [Header("Timer")]

    [SerializeField]
    private TMPro.TMP_Text timerText;
    [SerializeField]
    private TMPro.TMP_Text timerTextMesh;

    private GameControl gameControl;

    [Header("End Screen")]

    [SerializeField]
    private TMPro.TMP_Text endScreenMessage;
    [SerializeField]
    private string victoryMessage;
    [SerializeField]
    private string defeatMessage;
    [SerializeField]
    private string victoryMessageSpanish;
    [SerializeField]
    private string defeatMessageSpanish;

    void Awake()
    {
        gameControl = FindObjectOfType<GameControl>();
        
        ResetIcons();
        SetMenuScreen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMenuScreen()
    {
        ClearScreens();
        menuScreen.SetActive(true);
        menuCameraObject.SetActive(true);
    }

    public void SetGameScreen()
    {
        ClearScreens();
        gameScreen.SetActive(true);
    }

    public void SetPauseScreen()
    {
        ClearScreens();
        pauseScreen.SetActive(true);
    }

    public void SetEndScreen(bool s)
    {
        ClearScreens();
        endScreen.SetActive(true);
        menuCameraObject.SetActive(true);
        if (s)
        {
            endScreenMessage.text = victoryMessageSpanish;
        }
        else
        {
            endScreenMessage.text = defeatMessageSpanish;
        }
    }

    private void ClearScreens()
    {
        menuScreen.SetActive(false);
        gameScreen.SetActive(false);
        pauseScreen.SetActive(false);
        endScreen.SetActive(false);
        menuCameraObject.SetActive(false);
    }

    public void NewGame()
    {
        ResetIcons();
        SetGameScreen();
    }


    public void ItemFound(Collectable.Item item)
    {
        switch (item)
        {
            case Collectable.Item.CAR:
                carIcon.sprite = car[1];
                break;
            case Collectable.Item.CAMERA:
                cameraIcon.sprite = camera[1];
                break;
            case Collectable.Item.CANNON:
                cannonIcon.sprite = cannon[1];
                break;
            case Collectable.Item.BALLOON:
                balloonIcon.sprite = balloon[1];
                break;
            case Collectable.Item.EARTH:
                earthIcon.sprite = earth[1];
                break;
            case Collectable.Item.VIOLIN:
                violinIcon.sprite = violin[1];
                break;
            case Collectable.Item.KEY:
                keyIcon.sprite = key[1];
                escapeMessage.SetActive(true);
                keyReleasedMessage.SetActive(false);
                break;

        }

    }

    private void ResetIcons()
    {
        carIcon.sprite = car[0];
        cameraIcon.sprite = camera[0];
        cannonIcon.sprite = cannon[0];
        balloonIcon.sprite = balloon[0];
        earthIcon.sprite = earth[0];
        violinIcon.sprite = violin[0];
        keyIcon.sprite = key[0];
        keyReleasedMessage.SetActive(false);
        findMissingObjectsMessage.SetActive(false);
        objectsToFindPanel.SetActive(false);
        keyPanel.SetActive(false);
        escapeMessage.SetActive(false);
    }

    public void PlayButton()
    {
        gameControl.NewGame();

    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void WriteTimer(int m, int s)
    {
        if (s < 10)
        {
            timerText.text = m.ToString() + ":0" + s.ToString();
            timerTextMesh.text = m.ToString() + ":0" + s.ToString();
        }
        else
        {
            timerText.text = m.ToString() + ":" + s.ToString();
            timerTextMesh.text = m.ToString() + ":" + s.ToString();
        }

    }

    public void KeyReleased()
    {
        keyReleasedMessage.SetActive(false);
        findMissingObjectsMessage.SetActive(false);
        objectsToFindPanel.SetActive(false);
        keyPanel.SetActive(true);
        escapeMessage.SetActive(true);
    }

    public void ClickToStartButton()
    {
        gameControl.ClickToStartButton();
    }


}
