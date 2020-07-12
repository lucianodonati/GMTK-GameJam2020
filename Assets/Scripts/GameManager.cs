using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    [SerializeField, HideInInspector]
    private AudioSource audioSource = null;

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

    [SerializeField]
    private GameObject[] moveMe = null;

    [SerializeField]
    private int partsToPhoneSpy = 3;

    [SerializeField]
    private int partsToConsole = 3;

    [SerializeField]
    private GameObject console = null;

    private FileCreator fileCreator = new FileCreator();

    private void OnValidate()
    {
        if (null == audioSource)
            audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (var trans in moveMe)
                trans.transform.position += Vector3.right * .001f;
        }
    }

    public void ProgressPhoneSpy()
    {
        var progress = PlayerPrefs.GetInt("SpyProgress", 0) + 1;
        PlayerPrefs.SetInt("SpyProgress", progress);
        ProgressToPhoneSpy(progress);
    }

    public void ProgressConsole()
    {
        var progress = PlayerPrefs.GetInt("ConsoleProgress", 0) + 1;
        PlayerPrefs.SetInt("ConsoleProgress", progress);
        ProgressToConsole(progress);
    }

    private void ProgressToPhoneSpy(int currentProgress)
    {
        if (currentProgress >= partsToPhoneSpy)
        {
            QuitAndAcknowledge();
        }
    }

    private void ProgressToConsole(int currentProgress)
    {
        if (currentProgress >= partsToConsole)
        {
            StartCoroutine(EnableConsoleRoutine());
        }
    }

    private void QuitAndAcknowledge()
    {
        if (PlayerPrefs.GetInt("HasCrashed", 0) == 0)
        {
            if (Application.isPlaying && !Application.isEditor)
                StartCoroutine(BSODRoutine());
        }
    }

    IEnumerator EnableConsoleRoutine()
    {
        yield return new WaitForSeconds(16);
        console.SetActive(true);
    }

    IEnumerator BSODRoutine()
    {
        yield return new WaitForSeconds(15);
        BSOD.SetActive(true);
        audioSource.PlayOneShot(BSOD_Clip, 1);
        PlayerPrefs.SetInt("HasCrashed", 1);
        CreateKeyCombinationFiles(filesToCreate);

        yield return new WaitForSeconds(timeToQuit);

        Application.Quit();
    }

    private void CreateKeyCombinationFiles(int howMany)
    {
        for (int i = 0; i < howMany; i++)
            fileCreator.CreateTxtFile($"KEY_CODE{i + 1}", "Password: mateo71516");
    }
}