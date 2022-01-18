using UnityEngine;
using TMPro;
public class SpeedPost : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;
    public enum SpeedStates
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }
    public SpeedStates speedStates;

    public int speed;

    private void Start()
    {
        textMeshPro = GetComponentInChildren<TextMeshPro>();
        AssignTextMeshText();
    }
    public void AssignTextMeshText()
    {
        switch (speedStates)
        {
            case SpeedStates.Add:
                textMeshPro.text = "+" + speed;
                break;
            case SpeedStates.Subtract:
                textMeshPro.text = "-" + speed;
                break;
            case SpeedStates.Multiply:
                textMeshPro.text = "x" + speed;
                break;
            case SpeedStates.Divide:
                textMeshPro.text = "%" + speed;
                break;
            default:
                break;
        }
    }
    public int CarSpeed(int currentSpeed)
    {
        switch (speedStates)
        {
            case SpeedStates.Add:
                return currentSpeed += speed;
            case SpeedStates.Subtract:
                return currentSpeed -= speed;
            case SpeedStates.Multiply:
                return currentSpeed *= speed;
            case SpeedStates.Divide:
                return currentSpeed /= speed;
            default:
                return 0;
        }
    }
}