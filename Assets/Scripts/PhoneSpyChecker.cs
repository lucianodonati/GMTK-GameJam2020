using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhoneSpyChecker : MonoBehaviour
{
    [SerializeField]
    private string correctPasscode;

    [SerializeField]
    private UnityEvent onCorrectPasscode = null;

    [SerializeField]
    private UnityEvent onIncorrectPasscode = null;

    public void CheckPasscode(string passcode)
    {
        if (correctPasscode.Equals(passcode))
            onCorrectPasscode.Invoke();
        else
            onIncorrectPasscode.Invoke();
    }
}