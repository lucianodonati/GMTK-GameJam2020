using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Remoting.Channels;
using System.Threading;
using Sirenix.OdinInspector;
using UnityEngine;

public class ConsoleScript : MonoBehaviour
{
    [SerializeField] private float score;

    [SerializeField] private ConsoleWriter writer;

    [Serializable]
    struct Response
    {
        public string responseText;
        public char option;
        public float value;

        public Response(string text, char option, float value)
        {
            this.responseText = text;
            this.option = option;
            this.value = value;
        }
    }

    [Serializable]
    struct Question
    {
        public string questionText;
        public Response[] responses;
        public float delay;

        public Question(string text, Response[] responses, float delay)
        {
            this.questionText = text;
            this.delay = delay;
            this.responses = responses;
        }
    }

    [SerializeField] private Question[] scriptQuestions;

    private void Start()
    {
        StartCoroutine(WriteScript());
    }

    IEnumerator WriteScript()
    {
        for (int index = 0; index < scriptQuestions.Length; index++)
        {
            if (scriptQuestions[index].responses.Length == 0)
            {
                writer.SendMessage(scriptQuestions[index].questionText, false);
                yield return writer.writeCoroutine;
            }
            else
            {
                writer.SendMessage(scriptQuestions[index].questionText, true);
                yield return writer.writeCoroutine;


                writer.SendMessage("Choose an option:", false);
                yield return writer.writeCoroutine;
                foreach (var response in scriptQuestions[index].responses)
                {
                    writer.SendMessage(response.responseText + " [" + response.option + ']', false);
                    yield return writer.writeCoroutine;
                    yield return new WaitForSeconds(scriptQuestions[index].delay);
                }


                int chose = -1;
                do
                {
                    if (Input.anyKeyDown)
                    {
                        if (Input.GetKeyDown(KeyCode.Alpha1))
                            chose = 1;
                        else if (Input.GetKeyDown(KeyCode.Alpha2))
                            chose = 2;
                        else if (Input.GetKeyDown(KeyCode.Alpha3))
                            chose = 3;
                    }

                    yield return null;
                } while (chose == -1);

                writer.SendAnswer(scriptQuestions[index].responses[chose - 1].responseText);
                score += scriptQuestions[index].responses[chose - 1].value;
                yield return writer.writeCoroutine;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}

