using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    /* Todos los items que el personaje puede recoger tienen asignado este script.
     * Se encarga de determinar el tipo de objeto, avisar al control cuando el personaje
     * los recoge y se autodestruye.
     */

    public enum Item {CAR,CAMERA,CANNON,BALLOON,EARTH,VIOLIN,KEY};
    
    [SerializeField]
    private Item item;

    private string playerTag="Player";
    private GameControl gameControl;
    private bool playerInRange;

    private void Start()
    {
        gameControl = FindObjectOfType<GameControl>();
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Collect();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.tag == playerTag)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == playerTag)
        {
            playerInRange = false;
        }
    }

    private void Collect()
    {
        gameControl.ItemFound(item);
        Destroy(gameObject);
    }



}
