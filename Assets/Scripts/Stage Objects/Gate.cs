using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

    private GameControl gameControl;
    private string playerTag="Player";
    [SerializeField]
    private GameObject gateLeft;
    [SerializeField]
    private GameObject gateRight;

    
    [SerializeField]
    private float anglesToOpen;
    [SerializeField]
    private int steps;

    private float rotationStep;
    private bool rotating;
    private bool opened;
    private bool opening;
    private bool playerInRange;

    void Start()
    {
        gameControl = FindObjectOfType<GameControl>();
       
        
        NewGame();
    }

    public void NewGame()
    {
        opened = false;
        opening = false;
        gateRight.transform.localEulerAngles = Vector3.zero;
        gateLeft.transform.localEulerAngles = Vector3.zero;
    }


    private void Update()
    {
        rotationStep = anglesToOpen / steps;

        if (playerInRange && !opened && gameControl.PlayerHasKey() && Input.GetKeyDown(KeyCode.F))
        {
            OpenGate();
        }


    }


    void FixedUpdate()
    {

        if (opening)
        {
            
            
                if (gateRight.transform.localEulerAngles.z < anglesToOpen)
                {
                    gateLeft.transform.Rotate(0, 0, -rotationStep);
                    gateRight.transform.Rotate(0, 0, rotationStep);
                }
                else
                {
                    opening = false;
                    
                    gateRight.transform.localEulerAngles = new Vector3(0,0,anglesToOpen);
                    gateLeft.transform.localEulerAngles = new Vector3(0, 0, -anglesToOpen);
                }
         
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

    private void OpenGate()
    {
        opening = true;
        gateLeft.GetComponent<AudioSource>().Play();
        gateRight.GetComponent<AudioSource>().Play();
        opened = true;
        

    }



}
