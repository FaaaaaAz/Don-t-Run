using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{

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
