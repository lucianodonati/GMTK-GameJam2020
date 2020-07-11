using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum Stages
    {
        Intro,
        Medium,
        Late,
        End,
        LENGHT,
        INVALID
    }

    private Stages currentStage;

    private float stageTime;
    [SerializeField]private float[] timers;


    private bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        timers = new float [(int) Stages.LENGHT];
        currentStage = Stages.INVALID;
    }

    // Update is called once per frame
    void Update()
    {
        //This will be replaced by the real way to Start the game.
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartGame();
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
        currentStage = Stages.Intro;
        stageTime = 10f;
        gameStarted = true;
    }

    private void StartMediumStage()
    {
        currentStage = Stages.Medium;
        stageTime = 20f;
    }

    private void StartLateStage()
    {
        currentStage = Stages.Late;
        stageTime = 0f;
    }

    private void StartEndStage()
    {
        currentStage = Stages.End;
        stageTime = 0f;
    }
}