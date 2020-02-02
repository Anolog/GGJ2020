using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void OnPlayGameClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
