using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    public static ChapterManager instance;
    public int highestLevel;
    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        highestLevel = PlayerPrefs.GetInt("highestChapter");
    }
    public int ReturnHighestLevel()
    {
        return highestLevel;
    }
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
