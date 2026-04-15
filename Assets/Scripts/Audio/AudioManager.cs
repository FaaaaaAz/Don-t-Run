using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /* Este script reproduce un sonido cuando se encuentra un item.
     */

    [SerializeField]
    private GameObject collectSoundPrefab;

    public void ItemCollected()
    {
        Instantiate(collectSoundPrefab);
    }

}
