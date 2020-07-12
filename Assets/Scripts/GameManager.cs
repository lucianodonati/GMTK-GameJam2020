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
        End,
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
            CreateKeyCombinationFiles(5);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            QuitAndAcknowledge();
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
        //audioSource.PlayOneShot(BSOD_Clip, 1);
        //PlayerPrefs.SetInt("HasCrashed", 1);
        CreateKeyCombinationFiles(5);

        yield return new WaitForSeconds(1);

        Application.Quit();
    }

    private void CreateKeyCombinationFiles(int howMany)
    {
        for (int i = 0; i < howMany; i++)
            fileCreator.CreateTxtFile($"key{i + 1}", "K+T+M+G :noitanibmoC yeK");
    }
}