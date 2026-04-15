using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthPiece : MonoBehaviour
{

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
