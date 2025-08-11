using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource[] audioPool;

    public float soundsVolume = 1.0f;
    [SerializeField] private AudioClip btnClickClip;
    [SerializeField] private AudioClip errorClip;
    [SerializeField] private AudioClip winClip;
    [SerializeField] private AudioClip loseClip;
    [SerializeField] private AudioClip bgmClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        audioPool = new AudioSource[100];
    }

    public void ChangeVolume(float volume) //type = 1 Bgm / type = 0 SFX
    {
        soundsVolume = volume;
    }

    private IEnumerator DestroyAudio(GameObject audioGO, float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        Destroy(audioGO);
    }
    public void GameResultSFXPlay(int _type)
    {
        AudioClip soundClip;
        if (_type == 0) soundClip = winClip;
        else soundClip = loseClip;
        for (int i = 0; i < audioPool.Length; i++)
        {
            if (audioPool[i] == null || !audioPool[i].isPlaying)
            {
                GameObject go = new GameObject { name = soundClip.name };
                audioPool[i] = go.AddComponent<AudioSource>();
                audioPool[i].transform.position = Camera.main.transform.position;
                audioPool[i].spatialBlend = 0.0f;
                audioPool[i].PlayOneShot(soundClip, soundsVolume);


                StartCoroutine(DestroyAudio(go, soundClip.length * 3.5f));
                return;
            }
        }
        return;
    }
    public void CloseClipPlay()
    {
        for (int i = 0; i < audioPool.Length; i++)
        {
            if (audioPool[i] == null || !audioPool[i].isPlaying)
            {
                GameObject go = new GameObject { name = errorClip.name };
                audioPool[i] = go.AddComponent<AudioSource>();
                audioPool[i].transform.position = Camera.main.transform.position;
                audioPool[i].spatialBlend = 0.0f;
                audioPool[i].PlayOneShot(errorClip, soundsVolume);


                StartCoroutine(DestroyAudio(go, errorClip.length * 3.5f));
                return;
            }
        }
        return;
    }
    public void BtnClickPlay()
    {
        for (int i = 0; i < audioPool.Length; i++)
        {
            if (audioPool[i] == null || !audioPool[i].isPlaying)
            {
                GameObject go = new GameObject { name = btnClickClip.name };
                audioPool[i] = go.AddComponent<AudioSource>();
                audioPool[i].transform.position = Camera.main.transform.position;
                audioPool[i].spatialBlend = 0.0f;
                audioPool[i].PlayOneShot(btnClickClip, soundsVolume);


                StartCoroutine(DestroyAudio(go, btnClickClip.length * 3.5f));
                return;
            }
        }
        return;
    }
    public void PlaySound(AudioClip _clip)
    {
        for (int i = 0; i < audioPool.Length; i++)
        {
            if (audioPool[i] == null || !audioPool[i].isPlaying)
            {
                GameObject go = new GameObject { name = _clip.name };
                audioPool[i] = go.AddComponent<AudioSource>();
                audioPool[i].transform.position = Camera.main.transform.position;
                audioPool[i].spatialBlend = 0.0f;
                audioPool[i].PlayOneShot(_clip, soundsVolume);


                StartCoroutine(DestroyAudio(go, _clip.length * 3.5f));
                return;
            }
        }
        return;
    }
    public void PlayBGM()
    {
        for (int i = 0; i < audioPool.Length; i++)
        {
            if (audioPool[i] == null || !audioPool[i].isPlaying)
            {
                GameObject go = new GameObject { name = bgmClip.name };
                audioPool[i] = go.AddComponent<AudioSource>();
                audioPool[i].transform.position = Camera.main.transform.position;
                audioPool[i].spatialBlend = 0.0f;

                audioPool[i].clip = bgmClip;
                audioPool[i].volume = soundsVolume;
                audioPool[i].Play();
                //audioPool[i].PlayOneShot(bgmClip, soundsVolume);
                audioPool[i].loop = true;

                return;
            }
        }
        return;
    }
    public void PlayBGM(AudioClip _clip)
    {
        for (int i = 0; i < audioPool.Length; i++)
        {
            if (audioPool[i] == null || !audioPool[i].isPlaying)
            {
                GameObject go = new GameObject { name = _clip.name };
                audioPool[i] = go.AddComponent<AudioSource>();
                audioPool[i].transform.position = Camera.main.transform.position;
                audioPool[i].spatialBlend = 0.0f;

                audioPool[i].clip = _clip;
                audioPool[i].volume = soundsVolume;
                audioPool[i].Play();
                //audioPool[i].PlayOneShot(bgmClip, soundsVolume);
                audioPool[i].loop = true;

                return;
            }
        }
        return;
    }

}