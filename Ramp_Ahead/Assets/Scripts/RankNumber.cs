using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankNumber : MonoBehaviour
{

    public int rankNumber;

    public void AssignRankNumber(int number)
    {
        Debug.Log(number);
        rankNumber = number;
    }

}
