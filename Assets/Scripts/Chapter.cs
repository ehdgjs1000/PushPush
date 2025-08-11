using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class Chapter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameLevelText;
    [SerializeField] Image[] starImages;
    [SerializeField] Sprite[] starSprites;
    [SerializeField] Image clearBG;
    [SerializeField] Image isClearCheckImage;

    Button startBtn;
    Image mainImage;
    int gameLevel;
    float updateInterval = 0.2f;
    float timer = 0f;

    private void Awake()
    {
        mainImage = GetComponent<Image>();
        startBtn = GetComponent<Button>();
        gameLevel = transform.GetSiblingIndex() + 1;
    }
    private void Start()
    {
        UpdateGameLevelText();
        UpdateUI();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= updateInterval)
        {
            UpdateUI();
        }
    }
    private void UpdateUI()
    {
        if(gameLevel <= ChapterManager.instance.ReturnHighestLevel())
        {
            //¸ðµç UI ¶ç¿ï ¼ö ÀÖÀ½
            mainImage.color = Color.yellow;
            startBtn.enabled = true;
            clearBG.gameObject.SetActive(false);
            isClearCheckImage.gameObject.SetActive(true);
            foreach (Image starImage in starImages)
            {
                starImage.gameObject.SetActive(true);
            }
            UpdateStar();

        }
        else if (gameLevel == ChapterManager.instance.ReturnHighestLevel()+1)
        {
            //check»©°í ´Ù ¶ç¿ò
            mainImage.color = Color.red;
            startBtn.enabled = true;
            clearBG.gameObject.SetActive(false);
            isClearCheckImage.gameObject.SetActive(false);
            foreach (Image starImage in starImages)
            {
                starImage.gameObject.SetActive(true);
            }
            UpdateStar();
        }
        else if (gameLevel > ChapterManager.instance.ReturnHighestLevel() + 1)
        {
            //¸ðµÎ ¸ø ¶ç¿ò
            startBtn.enabled = false;
            clearBG.gameObject.SetActive(true);
            isClearCheckImage.gameObject.SetActive(false);
            foreach (Image starImage in starImages)
            {
                starImage.gameObject.SetActive(false);
            }
        }
    }
    private void UpdateGameLevelText()
    {
        gameLevelText.text = gameLevel.ToString();
    }
    private void UpdateStar()
    {
        string text = "Level" + gameLevel.ToString();
        int clearstar = PlayerPrefs.GetInt(text);

        for (int i = 0; i < 3; i++)
        {
            if(i < clearstar) //Clear
            {
                starImages[i].sprite = starSprites[0];
            }
            else //no clear
            {
                starImages[i].sprite = starSprites[1];
            }

        }
    }

    public void StartOnClick()
    {
        StartCoroutine(GameStart());    
    }
    IEnumerator GameStart()
    {
        SoundManager.instance.BtnClickPlay();
        yield return new WaitForSeconds(0.15f);
        string text = "Level" + gameLevel;
        SceneManager.LoadScene("InGameCommon");
        SceneManager.LoadScene(text, LoadSceneMode.Additive);
    }
}
