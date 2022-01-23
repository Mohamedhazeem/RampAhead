using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public Joystick floatingJoystick;

    public delegate void MouseCallback();
    public event MouseCallback  OnMouseDown, OnMouseUp;
    public delegate void MouseDragCallback(float X);
    public event MouseDragCallback OnMouseDrag;


    [Header("Ortho Graphic Camera")]
    [SerializeField] private Camera orthographicCamera;

    private Vector3 MouseStartPosition;
    private Vector3 MouseCurrentPosition;

    private void Awake()
    {
        AssignInstance();
    }
    private void AssignInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }
    void Update()
    {
        Joystick();
    }
    private void HoldAndDrag()
    {
        if (GameManager.Instance.currentGameState == GameManager.GameState.Menu && UIManager.instance.currentMenuState == UIManager.MenuState.Start && PlayerManager.Instance.currentCarStates == CarStates.Idle && Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.SwitchGameStates();
            UIManager.instance.SwitchUiState();
        }

        if (GameManager.Instance.currentGameState == GameManager.GameState.GamePlay && PlayerManager.Instance.currentCarStates == CarStates.Idle && Input.GetMouseButtonDown(0))
        {
            PlayerManager.Instance.SwitchPlayerStates();
            OnMouseDown?.Invoke();
            MouseStartPosition = orthographicCamera.ScreenToWorldPoint(Input.mousePosition);
            MouseStartPosition.y = PlayerManager.Instance.currentCar.transform.position.y;

        }
        else if (Input.GetMouseButton(0) && GameManager.Instance.currentGameState == GameManager.GameState.GamePlay && PlayerManager.Instance.currentCarStates == CarStates.Running)
        {
            MouseCurrentPosition = orthographicCamera.ScreenToWorldPoint(Input.mousePosition);
            MouseCurrentPosition.y = PlayerManager.Instance.currentCar.transform.position.y;

            var difference = MouseCurrentPosition - MouseStartPosition;
            OnMouseDrag(difference.x);
           
        }
        else if (Input.GetMouseButtonUp(0) && GameManager.Instance.currentGameState == GameManager.GameState.GamePlay && PlayerManager.Instance.currentCarStates == CarStates.Running)
        {
            
            PlayerManager.Instance.SwitchPlayerStates();
            OnMouseUp?.Invoke();
        }
    }
    private void Joystick()
    {
        if (GameManager.Instance.currentGameState == GameManager.GameState.Menu && PlayerManager.Instance.currentCarStates == CarStates.Idle && Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.SwitchGameStates();
            UIManager.instance.SwitchUiState();

        }
        if (GameManager.Instance.currentGameState == GameManager.GameState.GamePlay && PlayerManager.Instance.currentCarStates == CarStates.Idle && Input.GetMouseButtonDown(0))
        {
            OnMouseDown?.Invoke();
            PlayerManager.Instance.currentCarStates = CarStates.Running;
            PlayerManager.Instance.SwitchPlayerStates();
        }

        if (Input.GetMouseButton(0) && GameManager.Instance.currentGameState == GameManager.GameState.GamePlay && PlayerManager.Instance.currentCarStates == CarStates.Running)
        {
            OnMouseDown?.Invoke();
            if (floatingJoystick.Horizontal > 0.1f || floatingJoystick.Horizontal < -0.1f)
            {
                OnMouseDrag?.Invoke(floatingJoystick.Horizontal);
            }
        }
        if (Input.GetMouseButtonUp(0) && GameManager.Instance.currentGameState == GameManager.GameState.GamePlay && PlayerManager.Instance.currentCarStates == CarStates.Running)
        {
            OnMouseUp?.Invoke();
            PlayerManager.Instance.SwitchPlayerStates();
        }
    }
}

