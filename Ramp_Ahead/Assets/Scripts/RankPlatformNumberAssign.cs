using UnityEngine;
using TMPro;
public class RankPlatformNumberAssign : MonoBehaviour
{
    public Color[] colors;
    private int lastRandomNumber = 0;
    private int loopLimit = 50;
    void Start()
    {
        NumberForRank();
    }
    private void NumberForRank()
    {
        var count = transform.childCount;
        for (int i = 0; i < count; ++i)
        {
            var text = transform.GetChild(i).GetChild(0).GetComponent<TextMeshPro>();
            var number = i;
           // Debug.Log("I" + i);
            ++number;
            //Debug.Log("Number" + number);
            transform.GetChild(i).GetComponent<RankNumber>().AssignRankNumber(number);
            text.text = "X" + number;
        }
        for (int j = 0; j < count; j++)
        {
            int loopCount = 0;
            while (loopCount < loopLimit)
            {
                var randomNumber = Random.Range(0,colors.Length);
                if(randomNumber != lastRandomNumber)
                {
                    transform.GetChild(j).GetComponent<Renderer>().material.color = colors[randomNumber];
                    lastRandomNumber = randomNumber;
                    break;
                }
                if (loopCount == loopLimit - 1)
                {
                    transform.GetChild(j).GetComponent<Renderer>().material.color = colors[randomNumber];
                    lastRandomNumber = randomNumber;
                }
                loopCount++;
            }
            
        }
    }

}
