using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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

    public ParticleSystem trailEffect;
    public Material rankMaterial;

    private ParticleSystem trailEffectLeft, trailEffectRight;


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

        trailEffectLeft = Instantiate(trailEffect, car.boostSpawnPointLeft.position, Quaternion.identity);
        trailEffectLeft.transform.SetParent(car.boostSpawnPointLeft);

        trailEffectRight = Instantiate(trailEffect, car.boostSpawnPointRight.position, Quaternion.identity);
        trailEffectRight.transform.SetParent(car.boostSpawnPointRight);

        currentCarStates = CarStates.Idle;
    }
  
    public void SwitchPlayerStates()
    {
        switch (currentCarStates)
        {
            case CarStates.Idle:
                currentCarStates = CarStates.Running;
                //goto case CarStates.Running;
                break;
            case CarStates.Running:
                Debug.Log("C");
                StopAllCoroutines();
                SmokeTrail(true);
                CarSpeed(false);
                break;

            case CarStates.Jump:
                break;

            case CarStates.Fall:
                StopAllCoroutines();
                SmokeTrail(false);
                CarSpeed(true);
                currentCarStates = CarStates.Idle;

                break;

            default:
                break;
        }
    }
    public void CarSpeed(bool isJumped)
    {
        StartCoroutine(car.ChangeSpeed(isJumped));
    }
    public void SmokeTrail(bool isSmokeTrail)
    {
        if (isSmokeTrail)
        {
            trailEffectLeft.Play();
            trailEffectRight.Play();
        }
        else
        {
            trailEffectLeft.Stop();
            trailEffectRight.Stop();
        }
    }
    public void RankPlatformBlink()
    {
        rankMaterial.DOColor(Color.white, 0.5f).SetLoops(-1,LoopType.Yoyo);
    }
}
public enum CarStates
{
    Idle,
    Running,
    Jump,
    Fall,
}
