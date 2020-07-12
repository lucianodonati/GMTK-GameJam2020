using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    [SerializeField, HideInInspector]
    private AudioSource audioSource = null;

    #region Stages

    private enum Stages
    {
        Pre,
        IntroGame,
        Late,
        Console,
        LENGHT,
        INVALID
    }

    [ShowInInspector, ReadOnly]
    private Stages currentStage = Stages.INVALID;

    [ShowInInspector, ReadOnly]
    private float stageTime;

    [ShowInInspector, ReadOnly]
    private bool gameStarted = false;

    [SerializeField]
    private float[] timers = new float [(int) Stages.LENGHT];

    #endregion

    #region BSOD

    [SerializeField]
    private GameObject BSOD = null;

    [SerializeField]
    private int filesToCreate = 10;

    [SerializeField]
    private float timeToQuit = 8;

    [SerializeField]
    private AudioClip BSOD_Clip = null;

    #endregion

    private FileCreator fileCreator = new FileCreator();


    private void OnValidate()
    {
        if (null == audioSource)
            audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (Application.isPlaying && !Application.isEditor)
            CreateKeyCombinationFiles(filesToCreate);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            QuitAndAcknowledge();
        }

        if (currentStage != Stages.Console)
        {
            if (Input.GetKeyDown(KeyCode.Q) && Input.GetKeyDown(KeyCode.Z) &&
                Input.GetKeyDown(KeyCode.P) && Input.GetKeyDown(KeyCode.M))
            {
                currentStage = Stages.Console;
            }
        }

        if (gameStarted)
        {
            timers[(int) currentStage] += Time.deltaTime;

            if (timers[(int) currentStage] >= stageTime)
            {
                //Helper();
            }
        }
    }

    private void StartGame()
    {
        currentStage = 0;
        stageTime = 10f;
        gameStarted = true;
    }

    private void StartMediumStage()
    {
    }

    private void StartLateStage()
    {
    }

    private void StartEndStage()
    {
    }

    [Button]
    private void QuitAndAcknowledge()
    {
        if (Application.isPlaying && !Application.isEditor)
            StartCoroutine(BSODRoutine());
    }

    IEnumerator BSODRoutine()
    {
        BSOD.SetActive(true);
        audioSource.PlayOneShot(BSOD_Clip, 1);
        //PlayerPrefs.SetInt("HasCrashed", 1);
        CreateKeyCombinationFiles(filesToCreate);

        yield return new WaitForSeconds(timeToQuit);

        Application.Quit();
    }

    private void CreateKeyCombinationFiles(int howMany)
    {
        for (int i = 0; i < howMany; i++)
            fileCreator.CreateTxtFile($"KEY_CODE{i + 1}", "Input Key Combination: Q+Z+M+P");
    }
}