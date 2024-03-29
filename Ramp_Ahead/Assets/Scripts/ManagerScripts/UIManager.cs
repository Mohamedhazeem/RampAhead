using System;
using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public enum MenuState
    {
        Start,
        GamePlay
    }
    internal MenuState currentMenuState;

    [Header("Speed Text")]
    public TMP_Text speed;

    private void Awake()
    {
        AssignInstance();
    }
    private void Start()
    {
        currentMenuState = MenuState.Start;
        speed.gameObject.SetActive(false);
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
            case MenuState.Start:
                currentMenuState = MenuState.GamePlay;
                speed.gameObject.SetActive(true);
                //holdAndDragText.SetActive(false);
                break;

            case MenuState.GamePlay:
                StopAllCoroutines();
                currentMenuState = MenuState.Start;
                
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
