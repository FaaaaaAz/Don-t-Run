using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    /* Este Script consiste en un Timer que se inicializa en un tiempo definido por el programador,
     * indicando los minutos y segundos en el inspector. Le envía la información a la interfaz gráfica para
     * que actualice el valor en pantalla.
     * El timer se inicia y se detiene con la orden del GameControl, constantemente lee el estado de pausa
     * para detener la cuenta cuando el juego está pausado.
     * Cuando el contador llega a 0, se le informa al GameControl.
     */

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
    // Start is called before the first frame update
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
