using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{ 
    public static PlayerManager Instance;

    [Header("Player Spawn Point")]
    public Transform carSpawnPoint;

    [Header("Players")]
    [SerializeField]
    private GameObject carPrefab;
    public GameObject currentCar;

    public CarStates currentCarStates;

    private Car car;
    private void Awake()
    {
        AssignInstance();
    }
    
    private void AssignInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        if (carSpawnPoint != null)
        {
            currentCar = Instantiate(carPrefab, carSpawnPoint.position, Quaternion.identity);
        }
        car = currentCar.GetComponent<Car>();
        currentCarStates = CarStates.Idle;
    }
  
    public void SwitchPlayerStates()
    {
        switch (currentCarStates)
        {
            case CarStates.Idle:
                currentCarStates = CarStates.Running;
                break;

            case CarStates.Running:
                break;

            case CarStates.Jump:
                break;

            case CarStates.Fall:
                currentCarStates = CarStates.Idle;
                break;

            default:
                break;
        }
    }
    public void CarSpeed()
    {
        StartCoroutine(car.ChangeSpeed(false));
    }
}
public enum CarStates
{
    Idle,
    Running,
    Jump,
    Fall,
}
