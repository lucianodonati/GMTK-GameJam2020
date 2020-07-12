using UnityEngine;

public class ProgressConsole : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt(name, 0) == 0)
        {
            PlayerPrefs.SetInt(name, 1);
            FindObjectOfType<GameManager>().ProgressConsole();
        }
    }
}