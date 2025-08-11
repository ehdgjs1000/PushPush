using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{


    public void StartOnClick()
    {
        SoundManager.instance.BtnClickPlay();
        SceneManager.LoadScene("ChapterScene");
    }
    public void OptionOnClick()
    {
        SoundManager.instance.BtnClickPlay();

    }
    public void ExitOnClick()
    {
        Application.Quit();
    }

}
