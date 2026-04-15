using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthPiece : MonoBehaviour
{
    /* Las piezas del laberinto que tienen una región en la que puedan aparecer los objetos a encontrar tienen asignado este Script.
     * Cada pieza tiene su propia geometría, este script permite establecer, para cada pieza, cómo es la región en la que el objeto puede aparecer.
     * El GameControl reunirá todos los GameObjects con este Script asignado, seleccionará algunos al azar y les solicitará una posición aleatoria
     * en la región donde los objetos pueden aparecer.
     */

    [SerializeField]
    private Transform area1position1;
    [SerializeField]
    private Transform area1position2;
    [SerializeField]
    private Transform area2position1;
    [SerializeField]
    private Transform area2position2;

    public Vector3 GetRandomPosition()
    {
        Vector3 pos = Vector3.zero;

        if (Random.value < 0.5f)
        {
            pos = Vector3.Lerp(area1position1.position,area1position2.position,Random.value);
        }
        else
        {
            pos = Vector3.Lerp(area2position1.position, area2position2.position, Random.value);
        }
        pos.y += 0.7f;
        return pos;
    }

}
