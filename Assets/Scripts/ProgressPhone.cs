using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressPhone : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt(name,0) == 0)
        {
            PlayerPrefs.SetInt(name, 1);
            FindObjectOfType<GameManager>().ProgressPhoneSpy();
        }
    }
}
