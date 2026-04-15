using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    /* En las puertas hay un empty GameObject con un Collider que tiene asignado este Script.
     * Se encarga de avisar al GameControl que el personaje salió del laberinto.
     */

    private string playerTag = "Player";
    private GameControl gameControl;

    private void Start()
    {
        gameControl = FindObjectOfType<GameControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag==playerTag)
        {
            gameControl.GameWon();
        }
    }

}
