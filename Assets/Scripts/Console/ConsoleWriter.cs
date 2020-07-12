using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ConsoleWriter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI consoleText;
    [SerializeField] private string[] messages = null;

    private string actualPersistantFakeMessage, actualPersistantRealMessage, answerMessage;
    private string directory;
    private string user;

    private char[] weirdCharacters;
    [SerializeField] private Vector2 timeToSendMessage;

    public Coroutine writeCoroutine;


    private void Start()
    {
        actualPersistantFakeMessage = null;
        actualPersistantRealMessage = null;
        directory = Environment.CurrentDirectory;
        user = Environment.UserName;
        weirdCharacters = new char[]
            {'¥', '¢', '£', '¤', '§', 'µ', 'Æ', 'Þ', 'ð', 'þ', '#', '&', '¦', '¶', '×', 'ß', 'æ', 'ø'};

        actualPersistantFakeMessage = "Microzsoft Windovs [Version 10.0.18362.900]" +
                                      "\n" +
                                      "(c) 2019 Microzsoft Corporation. All rights reserved." +
                                      "\n" +
                                      "\n" +
                                      "\n" +
                                      ">: ";

        consoleText.text = actualPersistantFakeMessage;
    }

    [Button(ButtonStyle.Box, Name = "Write Message")]
    public void SendMessage(string message, bool isTimeToChoose)
    {
        StopAllCoroutines();
        writeCoroutine = StartCoroutine(WriteMessage(message, isTimeToChoose));
    }


    public void SendAnswer(string message)
    {
        StopAllCoroutines();
        writeCoroutine = StartCoroutine(WriteAnswer(message));
    }

    public IEnumerator WriteMessage(string message, bool isTimeToChoose)
    {
        char[] encodedMessageChars = message.ToCharArray();
        char[] originalChars = message.ToCharArray();
        for (int index = 0; index < encodedMessageChars.Length; index++)
        {
            if (encodedMessageChars[index] != ' ')
            {
                encodedMessageChars[index] = weirdCharacters[Random.Range(0, weirdCharacters.Length)];
            }

            consoleText.text = actualPersistantFakeMessage += encodedMessageChars[index];

            float wait_Time = Random.Range(0.001f, 0.04f);
            if (Random.value <= 0.3f)
            {
                wait_Time = wait_Time * 2;
            }

            yield return new WaitForSeconds(wait_Time);
        }

        actualPersistantRealMessage = actualPersistantFakeMessage;
        var actualCharArray = actualPersistantRealMessage.ToCharArray();

        for (int index = 0; index < encodedMessageChars.Length; index++)
        {
            actualCharArray[actualPersistantRealMessage.Length - encodedMessageChars.Length + index] =
                originalChars[index];

            consoleText.text = actualPersistantRealMessage = actualCharArray.ArrayToString();
            float wait_Time = Random.Range(0.001f, 0.04f);
            if (Random.value <= 0.3f)
            {
                wait_Time = wait_Time * 5;
            }

            yield return new WaitForSeconds(wait_Time);
        }

        yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));

        if (isTimeToChoose)
        {
            actualPersistantRealMessage += "\n" +
                                           user + ">: ";
            answerMessage = actualPersistantRealMessage;
        }
        else
        {
            actualPersistantRealMessage += "\n" +
                                           ">: ";
        }

        actualPersistantFakeMessage = actualPersistantRealMessage;

        yield return new WaitForSeconds(Random.Range(timeToSendMessage.x, timeToSendMessage.y));
    }


    public IEnumerator WriteAnswer(string message)
    {
        message = "You choose: " + message;
        char[] encodedMessageChars = message.ToCharArray();
        char[] originalChars = message.ToCharArray();
        for (int index = 0; index < encodedMessageChars.Length; index++)
        {
            consoleText.text = answerMessage += encodedMessageChars[index];

            float wait_Time = Random.Range(0.001f, 0.08f);
            if (Random.value <= 0.3f)
            {
                wait_Time = wait_Time * 2;
            }

            yield return new WaitForSeconds(wait_Time);
        }

        actualPersistantRealMessage = answerMessage;

        yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));

        actualPersistantRealMessage += "\n" +
                                       ">: ";

        actualPersistantFakeMessage = actualPersistantRealMessage;

        yield return new WaitForSeconds(Random.Range(timeToSendMessage.x, timeToSendMessage.y));
    }
}