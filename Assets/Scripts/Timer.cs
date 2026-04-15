using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    
    [SerializeField]
    private int gameMinutes;
    [SerializeField]
    private int gameSeconds;
    private int m, s;
    private int gameTime;

    private GameControl gameControl;
    private UserInterface userInterface;
    private float timerCount;
    private bool timerRunning=false;
    void Start()
    {
        gameControl = FindObjectOfType<GameControl>();
        userInterface = FindObjectOfType<UserInterface>();
    }

    public void StartTimer()
    {
        timerRunning = true;
        timerCount = 0f;
        m = gameMinutes;
        s = gameSeconds;
        gameTime = 0;
        userInterface.WriteTimer(m, s);
       
    }

    public void StopTimer()
    {
        timerRunning = false;
        

    }

    void FixedUpdate()
    {
        if (!gameControl.IsGamePaused())
        {
            if (timerRunning)
            {
                timerCount += Time.fixedDeltaTime;
                if (timerCount >= 1f)
                {
                    timerCount = 0f;
                    UpdateTimer();
                }
            }

        }
    }

    private void UpdateTimer()
    {
    
        s--;
        gameTime++;
        if (s < 0)
        {
            if (m == 0)
            {
                StopTimer();
                gameControl.TimeIsUp();
                return;
            }
            else
            {          
                m--;
                s = 59;
            }
        }
        userInterface.WriteTimer(m, s);
        

   }



}
