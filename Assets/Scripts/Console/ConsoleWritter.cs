using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ConsoleWritter : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI consoleText;
    [SerializeField] 
    private string[] messages = null;
    
    private string actualPersistantFakeMessage,actualPersistantRealMessage;
    private string directory;
    
    private char[] weirdCharacters;
    [SerializeField]
    private Vector2 timeToSendMessage;

    

    private void Start()
    {
        directory = Environment.CurrentDirectory;
        weirdCharacters = new char[] {'¥','¢','£','¤','§','µ','Æ','Þ','ð','þ','#','&','¦','¶','×','ß','æ','ø'};

        actualPersistantFakeMessage = "Microzsoft Windovs [Version 10.0.18362.900]" +
                        "\n" +
                        "(c) 2019 Microzsoft Corporation. All rights reserved." +
                        "\n" +
                        "\n" +
                        "\n" +
                        directory + ">: ";

        consoleText.text = actualPersistantFakeMessage;
    }

    [Button(ButtonStyle.Box, Name = "Write Message")]
    private void SendMessage()
    {
        StopAllCoroutines();
        StartCoroutine(WriteEncodedMessage(0));
    }

    IEnumerator WriteEncodedMessage(int i)
    {
        char[] encodedMessageChars = messages[i].ToCharArray();
        char[] originalChars = messages[i].ToCharArray();
        for (int index = 0; index < encodedMessageChars.Length; index++)
        {
            if ( encodedMessageChars[index] != ' ')
            {
                encodedMessageChars[index] = weirdCharacters[Random.Range(0, weirdCharacters.Length)];
            }
            consoleText.text = actualPersistantFakeMessage += encodedMessageChars[index]; 
            
            float wait_Time = Random.Range(0.0005f, 0.08f);
            if (Random.value <= 0.3f)
            {
                wait_Time = wait_Time * 2;
            }
            yield return new WaitForSeconds(wait_Time);
        }

        actualPersistantRealMessage = actualPersistantFakeMessage;
        var actualCharArray = actualPersistantRealMessage.ToCharArray();
        
        for(int index = 0; index < encodedMessageChars.Length; index++)
        {
            actualCharArray[actualPersistantRealMessage.Length - encodedMessageChars.Length + index] = originalChars[index];
            
            consoleText.text = actualPersistantRealMessage = actualCharArray.ArrayToString();
            float wait_Time = Random.Range(0.0005f, 0.08f);
            if (Random.value <= 0.3f)
            {
                wait_Time = wait_Time * 5;
            }
            yield return new WaitForSeconds(wait_Time);
        }
        
        yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        
        actualPersistantRealMessage +="\n" +
                                       directory +">: ";
        actualPersistantFakeMessage = actualPersistantRealMessage;
        
        yield return new WaitForSeconds(Random.Range(timeToSendMessage.x, timeToSendMessage.y));
        i++;
        if (i < messages.Length)
        {
            StartCoroutine(WriteEncodedMessage(i));
        }
        else
        {
            StopCoroutine((WriteEncodedMessage(i)));
        }
    }
}