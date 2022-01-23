using UnityEngine;
using System.Collections;
using DG.Tweening;
public class Car : MonoBehaviour
{
    [SerializeField] public Rigidbody playerRigidBody;

    [Header("CAR TRANSFORM")]
    [SerializeField] private Transform carTransform;
    [SerializeField] private float rotateDuration;

    [Header("Move Speed")]
    [SerializeField] private int moveSpeed;
    [SerializeField] private int speedPaticleEffectValue;
    private float currentSpeed;

    [Header("Clamp Value on X Axis On Platform")]
    [SerializeField] private float xMinimum,xMaximum;

    [Header("Clamp Value on X Axis On Platform")]
    [SerializeField] private float xMinimumOnRank, xMaximumOnRank;

    [Header("Move speed on X Axis")]
    [SerializeField] private float xMoveSpeed;

    [Header("GRAVITY FOR RAMP")]
    [SerializeField] private float gravityValueForRamp;
    [SerializeField] private float multipliyerForRamp;

    [Header("GRAVITY FOR MINI-RAMP")]
    [SerializeField] private float  gravityValueForMiniRamp;
    [SerializeField] private float multipliyerForMiniRamp;

    [Header("BOOST EFFECT SPAWN POINT")]
    public Transform boostSpawnPointLeft;
    public Transform boostSpawnPointRight;

    [Header("BOOL")]
    public bool isSpeedTriggered = false ,isGrounded = false , isObstacleTriggered = false;
    public bool isRamp = false ,isMiniRampTrigger = false, isMiniRamp = false, isSideMove = false;

    private SpeedDown speedDown;
    private Tyre tyre;
    private MiniRamp miniRamp;

    protected void Start()
    {
        InputManager.instance.OnMouseUp += Idle;
        InputManager.instance.OnMouseDown += StartSideMove;
        InputManager.instance.OnMouseDrag += PlayerSideMoves;
        playerRigidBody = GetComponent<Rigidbody>();
    }
  
    private void Update()
    {
        if (!isRamp && !isMiniRampTrigger && !isSideMove && PlayerManager.Instance.currentCarStates == CarStates.Running)
        {
            Move();
        }
        if (isRamp)
        {
            var position = transform.position;
            position.x = Mathf.Clamp(position.x, xMinimumOnRank, xMaximumOnRank);
            transform.position = position;
            Fall();
        }
        else if (!isRamp)
        {            
            var position = transform.position;
            position.x = Mathf.Clamp(position.x, xMinimum, xMaximum);
            transform.position = position;
        }
        else if (isMiniRampTrigger)
        {
            playerRigidBody.velocity = transform.forward * moveSpeed;
            Fall();
        }
    }
    private void StartSideMove()
    {
        isSideMove = true;
    }
    private void Idle()
    {
        isSideMove = false;
    }
    private void Move()
    {        
        if(moveSpeed > 0 && !isMiniRamp)
        {
            playerRigidBody.velocity = Vector3.forward * moveSpeed;
        }

        if (isMiniRamp )
        {           
            playerRigidBody.velocity = transform.forward * moveSpeed;
        }
    }
    private void PlayerSideMoves(float x)
    {
        if (!isRamp && isSideMove == true && !isMiniRamp && !isMiniRampTrigger)
        {
            var position = transform.position;
            if (moveSpeed > 0)
            {
                if (position.x > xMinimum && x < 0)
                {

                    playerRigidBody.velocity = new Vector3(Vector3.right.x * x * xMoveSpeed, 0, moveSpeed);
                }
                else if (position.x < xMaximum && x > 0)
                {
                    playerRigidBody.velocity = new Vector3(Vector3.right.x * x * xMoveSpeed, 0, moveSpeed);
                }
                else
                {
                    playerRigidBody.velocity = Vector3.forward * moveSpeed;
                }

            }
            else
            {
                playerRigidBody.velocity = Vector3.zero;
            }

            position.x = Mathf.Clamp(position.x, xMinimum, xMaximum);

            transform.position = position;
        }
    }
    
   
    private void Jump()
    {
        CameraManager.instance.SwitchCamera();
        CameraManager.instance.SpeedParticleEffect(false);      
    }
    private void Fall()
    {
        if (isRamp)
        {
            playerRigidBody.AddForce(Vector3.down * gravityValueForRamp * multipliyerForRamp);
        }
        else if (isMiniRampTrigger)
        {
            playerRigidBody.AddForce(Vector3.down * gravityValueForMiniRamp * multipliyerForMiniRamp);
        }
        

    }      
    private void Flip()
    {

        switch (miniRamp.flip)
        {
            case MiniRamp.Flip.None:
                break;
            case MiniRamp.Flip.HorizontalRight:
                carTransform.DORotate(Vector3.up * 360, 1f, RotateMode.LocalAxisAdd)
                            .OnComplete(delegate()
                            {

                                playerRigidBody.DORotate(Vector3.zero, 0.5f);
                                carTransform.DORotate(Vector3.zero, 0.5f);
                            });
                break;
            case MiniRamp.Flip.HorizontalLeft:
                carTransform.DORotate(Vector3.down * 360, 1f, RotateMode.LocalAxisAdd)
                            .OnComplete(delegate ()
                            {
                                playerRigidBody.DORotate(Vector3.zero, 0.5f);
                                carTransform.DORotate(Vector3.zero, 0.5f);                               
                            });
                break;
            case MiniRamp.Flip.VerticalUp:
                carTransform.DORotate(Vector3.right * 360, 0.3f, RotateMode.LocalAxisAdd)
                            .OnComplete(delegate ()
                            {
                                playerRigidBody.DORotate(Vector3.zero, 0.3f);
                                carTransform.DORotate(Vector3.zero,0.3f);
                            });
                break;
            case MiniRamp.Flip.VerticalDown:
                carTransform.DORotate(Vector3.left * 360, 0.3f, RotateMode.LocalAxisAdd)
                            .OnComplete(delegate ()
                            {
                                playerRigidBody.DORotate(Vector3.zero, 0.3f);
                                carTransform.DORotate(Vector3.zero, 0.3f);
                            });
                break;
            default:
                break;
        }
    }   
    private void InitialRotation()
    {
        transform.DORotate(Vector3.zero, 0.5f);
        carTransform.DORotate(Vector3.zero, 0.5f);
    }
    private void OilRotate()
    {
        carTransform.DORotate(Vector3.up * 360, rotateDuration, RotateMode.FastBeyond360);
    }
    public IEnumerator ChangeSpeed(bool hasJumped)
    {
        if (!hasJumped)
        {
            while (true)
            {
                if (currentSpeed < moveSpeed)
                {
                    currentSpeed++;
                    UIManager.instance.speed.text = ((int)currentSpeed).ToString();
                }
                else if (currentSpeed > moveSpeed)
                {
                    currentSpeed--;
                    if (currentSpeed < 0)
                    {
                        currentSpeed = 0;
                    }
                    UIManager.instance.speed.text = ((int)currentSpeed).ToString();
                }
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            while (true)
            {
                if (currentSpeed > 0)
                {
                    currentSpeed--;
                    UIManager.instance.speed.text = ((int)currentSpeed).ToString();
                }
                else if (currentSpeed < 0)
                {
                    currentSpeed = 0;
                    UIManager.instance.speed.text = ((int)currentSpeed).ToString();
                }
                yield return new WaitForSeconds(0.05f);
            }
        }

    }
    public void CheckSpeed()
    {
        if (moveSpeed <= 0)
        {
            GameManager.Instance.currentGameState = GameManager.GameState.Lose;
            playerRigidBody.velocity = Vector3.zero;
        }
    }
    private void CheckSpeedEffect()
    {
        if (moveSpeed >= speedPaticleEffectValue)
        {
            CameraManager.instance.SpeedParticleEffect(true);
        }
        else
        {
            CameraManager.instance.SpeedParticleEffect(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Speed") && !isSpeedTriggered && PlayerManager.Instance.currentCarStates == CarStates.Running)
        {
            moveSpeed = other.GetComponent<SpeedPost>().CarSpeed(moveSpeed);
            CheckSpeedEffect();
            other.gameObject.SetActive(false);
            isSpeedTriggered = true;
            Invoke("Triggered", 0.5f);
        }

        if (other.CompareTag("Ramp"))
        {
            isRamp = true;         
            StopAllCoroutines();
            Jump();
            PlayerManager.Instance.currentCarStates = CarStates.Jump;
        }

        if (other.CompareTag("MiniRamp"))
        {            
            isMiniRampTrigger = true;
            isMiniRamp = false;
            PlayerManager.Instance.currentCarStates = CarStates.Jump;
        }

        if (other.CompareTag("Tyre") && !isObstacleTriggered)
        {
            tyre = other.GetComponent<Tyre>();
            tyre.DisableTyre();
            moveSpeed -= tyre.speed;
            CheckSpeed();
            CheckSpeedEffect();
            isObstacleTriggered = true;
            Invoke("ObstacleTriggered", 1f);
        }
        if(other.CompareTag("Oil") && !isObstacleTriggered)
        {
            speedDown = other.GetComponent<SpeedDown>();
            moveSpeed -= speedDown.speed;
            OilRotate();
            CheckSpeed();
            CheckSpeedEffect();
            isObstacleTriggered = true;
            Invoke("ObstacleTriggered", 1f);
        }        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ramp"))
        {
            isRamp = true;
        }
        if (other.CompareTag("MiniRamp"))
        {
            playerRigidBody.constraints = RigidbodyConstraints.None;
            Flip();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground") && !isGrounded)
        {
            isRamp = true;
            playerRigidBody.constraints = RigidbodyConstraints.None;

            StartCoroutine(ResetRigidBody(3f));
            isGrounded = true;
            GameManager.Instance.currentGameState = GameManager.GameState.Win;
            PlayerManager.Instance.currentCarStates = CarStates.Fall;
            PlayerManager.Instance.SwitchPlayerStates();            
        }
        
        else if (collision.collider.CompareTag("Road"))
        {
            isRamp = isMiniRampTrigger = false;
            InitialRotation();
            if(GameManager.Instance.currentGameState == GameManager.GameState.GamePlay)
            {
                PlayerManager.Instance.currentCarStates = CarStates.Running;
            }
            StartCoroutine(PlayerRigidbodyConstraints(0f));   
        }
        else if (collision.collider.CompareTag("Ramp"))
        {
            isRamp = true;
            playerRigidBody.constraints = RigidbodyConstraints.None;
        }
        else if (collision.collider.CompareTag("MiniRamp"))
        {

            isMiniRamp = true;
            playerRigidBody.constraints = RigidbodyConstraints.None;
            miniRamp = collision.collider.GetComponent<MiniRamp>();

            if(!miniRamp.isMiniRampSpeedAdded)
            {
                moveSpeed += miniRamp.speed;
                miniRamp.isMiniRampSpeedAdded = true;
            }

        }
        else if (collision.collider.CompareTag("Cone"))
        {
            Debug.Log("W");
            speedDown = collision.collider.GetComponent<SpeedDown>();
            moveSpeed -= speedDown.speed;
            CheckSpeed();
            CheckSpeedEffect();
            StartCoroutine(speedDown.DisableGameObject(0.5f));
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            PlayerManager.Instance.rankMaterial = collision.collider.GetComponent<Renderer>().material;
        }
    }
    
    IEnumerator ResetRigidBody(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        PlayerManager.Instance.RankPlatformBlink();
        playerRigidBody.velocity = Vector3.zero;
        playerRigidBody.angularVelocity = Vector3.zero;
        //playerRigidBody.isKinematic = true;
        isGrounded = false;
       
    }
    IEnumerator PlayerRigidbodyConstraints(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        playerRigidBody.constraints = RigidbodyConstraints.FreezeRotation;

    }
    private void Triggered()
    {
        isSpeedTriggered = false;
    }
    private void ObstacleTriggered()
    {
        isObstacleTriggered = false;
    }
   
}
