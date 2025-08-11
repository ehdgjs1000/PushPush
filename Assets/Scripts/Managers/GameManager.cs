using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI ballMoveCountText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject losePanel;
    [SerializeField] Image[] starImages;
    public bool isEndGame = false;
    public int ballMoveCount = 0;

    private void Awake()
    {
        instance = this;
        isEndGame = false;
        ballMoveCount = 0 ;
    }
    private void Start()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        levelText.text = SceneManager.GetSceneAt(1).name;
        ballMoveCountText.text = $"{ballMoveCount.ToString()}/{LevelManager.instance.HighestMoveCount()+1}";
    }
    public void BallMoveCountUpdate(int amount)
    {
        ballMoveCount += amount;
        UpdateUI();
        if (ballMoveCount > LevelManager.instance.HighestMoveCount())
        {
            LoseGame();
        }
    }
    private void LoseGame()
    {
        SoundManager.instance.GameResultSFXPlay(1);
        losePanel.SetActive(true);
    }
    public void GameOver()
    {
        SoundManager.instance.GameResultSFXPlay(0);
        //���� �ε� ���� Level��
        isEndGame = true;
        Scene scene = SceneManager.GetSceneAt(1);
        string sceneName = scene.name;

        //���¿� ���� �� �ֱ� 
        PlayerPrefs.SetInt(sceneName, LevelManager.instance.ReturnClearStar(ballMoveCount));
        //����� �ִ� é�ͺ��� ���� é���� ���/����é�� ��Ʈ���� ����ó��
        if(ReturnLevelNum() >= PlayerPrefs.GetInt("highestChapter"))
        {
            PlayerPrefs.SetInt("highestChapter", ReturnLevelNum());
        }
        gameOverPanel.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            if(i < LevelManager.instance.ReturnClearStar(ballMoveCount))
            {
                starImages[i].color = Color.yellow;
            }
        }
    }
    public void SettingOnClick()
    {
        settingPanel.SetActive(true);
    }
    public void RetryOnClick()
    {
        SceneManager.LoadScene("InGameCommon");
        SceneManager.LoadScene(SceneManager.GetSceneAt(1).name,LoadSceneMode.Additive);
    }
    public void ExitOnClick()
    {
        SceneManager.LoadScene("ChapterScene");
    }
    public void NextOnClick()
    {
        SceneManager.LoadScene("InGameCommon");
        SceneManager.LoadScene("Level"+ (ReturnLevelNum()+1), LoadSceneMode.Additive);
    }
    public int ReturnLevelNum()
    {
        string input = SceneManager.GetSceneAt(1).name;
        string numbersOnly = Regex.Replace(input, "[^0-9]", ""); //���ڸ� ����
        int nextNum = int.Parse(numbersOnly);
        return nextNum;
    }
}
