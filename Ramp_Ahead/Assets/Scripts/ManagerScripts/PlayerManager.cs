using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{ 
    public static PlayerManager Instance;

    public delegate void ClimaxIdleAnimationCallback();
    public event ClimaxIdleAnimationCallback OnClimaxIdleAnimation;

    public delegate void ClimaxWinAnimationCallback();
    public event ClimaxWinAnimationCallback OnClimaxWinAnimation;

    [Header("Player Spawn Point")]
    public Transform playerSpawnPoint;

    [Header("Players")]
    [SerializeField]
    private GameObject playerPrefab;
    public GameObject currentPlayer;
    public Material playerMaterial;

    public PlayerStates currentPlayerStates;

   
    public int Count;
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
        if (playerSpawnPoint != null)
        {
            currentPlayer = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        }

        currentPlayerStates = PlayerStates.Idle;
    }
  
    public void SwitchPlayerStates()
    {
        switch (currentPlayerStates)
        {
            case PlayerStates.Idle:
                currentPlayerStates = PlayerStates.Running;
                break;

            case PlayerStates.Running:
                    currentPlayerStates = PlayerStates.Idle;                
                break;

            case PlayerStates.Attack:

                break;

            case PlayerStates.Win:
                OnClimaxWinAnimation?.Invoke();
                break;

            case PlayerStates.Die:
                break;

            case PlayerStates.ClimaxIdle:
                StartCoroutine(ClimaxIdleToRun());
                OnClimaxIdleAnimation.Invoke();

                break;

            default:
                break;
        }
    }
    IEnumerator ClimaxIdleToRun()
    {
        yield return new WaitForSeconds(1f);
        currentPlayerStates = PlayerStates.Attack;
    }
    
}
public enum PlayerStates
{
    Idle,
    Running,
    Attack,
    ClimaxIdle,
    Win,
    Die
}
