using System.Collections;
using UnityEngine;

public class Tyre : SpeedDown
{
    public float disbleTime;
    public void DisableTyre()
    {
        StartCoroutine(DisableChildren(disbleTime));
    }
   
}
