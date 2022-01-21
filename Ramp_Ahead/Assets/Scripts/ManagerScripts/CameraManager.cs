using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [Header("Main CineMachine Camera")]
    public CinemachineVirtualCamera mainCinemachineCamera;
    [Header("Ramp CineMachine Camera")]
    public CinemachineVirtualCamera[] rampVirtualCamera;
    [Header("Ramp Cinemachine Priority")]
    public int priority;

    [Header("Speed particle System")]
    public ParticleSystem speedParticleSystem;


    public Camera primaryCamera;
    public Transform target;
    private Vector3 offset;
    public float distance;

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
    private void Start()
    {
        //offset = primaryCamera.transform.position - target.transform.position;

        AssignCarToCamera(PlayerManager.Instance.currentCar.transform);

    }
    public void SwitchCamera()
    {
        var camera = Random.Range(0, rampVirtualCamera.Length);
        rampVirtualCamera[camera].Priority = priority;
    }
    public void SpeedParticleEffect(bool isPlay)
    {
        if (isPlay)
        {
            speedParticleSystem.Play();
        }
        else
        {
            speedParticleSystem.Stop();
        }
        
    }
    private void AssignCarToCamera(Transform car)
    {
        mainCinemachineCamera.Follow = PlayerManager.Instance.currentCar.transform;
        mainCinemachineCamera.LookAt = PlayerManager.Instance.currentCar.transform;

        for (int i = 0; i < rampVirtualCamera.Length; i++)
        {
            rampVirtualCamera[i].Follow = car;
            rampVirtualCamera[i].LookAt = car;
        }
    }
    public void FollowPlayer()
    {

        if (target != null)
        {
            Vector3 pos = target.position;
            pos.x = 0;
            pos.y += offset.y;
            pos.z += offset.z;
            primaryCamera.transform.position = pos;

        }
    }
}
