using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectByLanguage : MonoBehaviour
{

    public bool[] enableStateByLanguage;
    

    public void SetLanguage(int language)
    {
        if (enableStateByLanguage == null || enableStateByLanguage.Length == 0)
        {
            return;
        }

        int index = Mathf.Clamp(language, 0, enableStateByLanguage.Length - 1);
        gameObject.SetActive(enableStateByLanguage[index]);
    }

}
