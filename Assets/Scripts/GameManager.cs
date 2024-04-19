using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IDataPersistence
{
    public static bool isPlay;
    public static bool isStartGame;
    public CinemachineVirtualCamera vcam;
    public Vector3 gameplayOffset;
    public Vector3 nonGameplayOffset;

    public float movementDuration = 1.5f;

    public Quaternion initialRotation;
    public Quaternion targetRotation;
    private bool isDone = false;
    private bool isRetry = false;

    public void LoadData(GameData data)
    {
        isRetry = data.isRetry;
    }

    public void SaveData(ref GameData data)
    {

    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        var transporter = vcam.GetCinemachineComponent<CinemachineTransposer>();
        transporter.m_FollowOffset = nonGameplayOffset;
        vcam.GetComponent<Transform>().rotation = initialRotation;
        isPlay = false;
        isStartGame = false;
    }

    private void Update()
    {
        if (isPlay && !isDone)
        {
            StartCoroutine(MoveRotateCamera(gameplayOffset, targetRotation, movementDuration));
        }

        if(isRetry)
        {
            isPlay = true;
            isRetry = false;
        }

    }

    IEnumerator MoveRotateCamera(Vector3 targetPosition, Quaternion targetRotation, float duration)
    {
        isDone = true;
        var transporter = vcam.GetCinemachineComponent<CinemachineTransposer>();
        float timer = 0f;
        Vector3 startPosition = nonGameplayOffset;
        Quaternion startRotation = initialRotation;

        while (timer < duration)
        {
            float t = timer / duration;
            transporter.m_FollowOffset = Vector3.Lerp(startPosition, targetPosition, t);
            vcam.GetComponent<Transform>().rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            timer += Time.deltaTime;
            yield return null;
        }
    }

}
