using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public enum MenuState
    {
        BeforeStart,
        Started
    }
    internal MenuState currentMenuState;

    public TMP_Text speed; 

    //[Header("Hold And Drag GameObject")]
    //[SerializeField]private GameObject holdAndDragText;

    //[Header("Restart and Next Button GameObject")]
    //[SerializeField] private GameObject restartButton;
    //[SerializeField] private GameObject nextButton;

    private void Awake()
    {
        AssignInstance();
    }
    private void Start()
    {
        currentMenuState = MenuState.BeforeStart;
        //holdAndDragText.SetActive(true);
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

    public void SwitchUiState()
    {
        switch (currentMenuState)
        {
            case MenuState.BeforeStart:
                currentMenuState = MenuState.Started;
                //holdAndDragText.SetActive(false);
                break;

            case MenuState.Started:
                currentMenuState = MenuState.BeforeStart;
                //holdAndDragText.SetActive(true);
                break;

            default:
                break;
        }
    }

    public void NextSceneButtonSetActive()
    {
        //nextButton.SetActive(true);
    }
    public void RestartSceneButtonSetActive()
    {
        //restartButton.SetActive(true); 
    }
    
}
