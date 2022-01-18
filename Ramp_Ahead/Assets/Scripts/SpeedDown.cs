using UnityEngine;
using System.Collections;
public class SpeedDown : MonoBehaviour
{
    public int speed;
    public IEnumerator DisableGameObject(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }

    public IEnumerator DisableChildren(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
