using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//サウンド管理用クラス
public class SoundManager
{

    public const string SePath = "Sounds/SE/";
    public const string BgmPath = "Sounds/BGM/";

    BGMPlayer CurBGMPlayer = null;
    BGMPlayer FadeOutBGMPlayer = null;

    //オーディオ情報クラス
    private class AudioClipInfo
    {
        public string resourceName;
        public string name;
        public AudioClip clip;

        public SortedList<int, SEInfo> stockList = new SortedList<int, SEInfo>();
        public List<SEInfo> playingList = new List<SEInfo>();
        public int maxSENum = 10;
        public float initVolume = 1.0f;
        public float attenuate = 0.0f;

        //コンストラクタ
        public AudioClipInfo(string resourceName, string name, int maxSENum, float initVolume)
        {
            this.resourceName = resourceName;
            this.name = name;

            this.maxSENum = maxSENum;
            this.initVolume = initVolume;
            attenuate = CalcAttenuateRate();

            // create stock list
            for (int i = 0; i < maxSENum; i++)
            {
                SEInfo seInfo = new SEInfo(i, 0.0f, initVolume * Mathf.Pow(attenuate, i));
                stockList.Add(seInfo.index, seInfo);
            }
        }

        float CalcAttenuateRate()
        {
            float n = maxSENum;
            return NewtonMethod.run(
                delegate (float p)
                {
                    return (1.0f - Mathf.Pow(p, n)) / (1.0f - p) - 1.0f / initVolume;
                },
                delegate (float p)
                {
                    float ip = 1.0f - p;
                    float t0 = -n * Mathf.Pow(p, n - 1.0f) / ip;
                    float t1 = (1.0f - Mathf.Pow(p, n)) / ip / ip;
                    return t0 + t1;
                },
                0.9f, 100
            );
        }
    }
    class SEInfo
    {
        public int index;
        public float curTime;
        public float volume;

        public SEInfo(int i, float v1, float v2)
        {
            index = i;
            curTime = v1;
            volume = v2;
        }
    }

    GameObject soundPlayer;
    AudioSource audioSource;
    Dictionary<string, AudioClipInfo> audioClips = new Dictionary<string, AudioClipInfo>();


    //コンストラクタ
    public SoundManager()
    {
        //ここにゲーム全体で使う音源を登録

        //SE
        audioClips.Add("fire", new AudioClipInfo(SePath + "fire", "fire", 1, 1.0f));
        audioClips.Add("tap", new AudioClipInfo(SePath + "tap", "tap", 5, 1.0f));
        audioClips.Add("tap2", new AudioClipInfo(SePath + "tap2", "tap2", 1, 1.0f));
        audioClips.Add("pic", new AudioClipInfo(SePath + "pic", "pic", 1, 1.0f));
        audioClips.Add("pic2", new AudioClipInfo(SePath + "pic2", "pic2", 1, 1.0f));
        audioClips.Add("happy", new AudioClipInfo(SePath + "happy", "happy", 1, 1.0f));

        audioClips.Add("fried", new AudioClipInfo(SePath + "Fried", "fried", 1, 1.0f));
        audioClips.Add("tenkasu", new AudioClipInfo(SePath + "tenkasu", "tenkasu", 1, 1.0f));

        //BGM
        audioClips.Add("bgm", new AudioClipInfo(BgmPath + "ほのぼのBGM「MusicMaterial」", "bgm", 1, 0.5f));
    }

    //SEを再生
    public bool PlaySE(string seName)
    {
        //指定のSEが登録されていない場合
        if (!audioClips.ContainsKey(seName)) return false;

        AudioClipInfo info = audioClips[seName];


        //ロード
        if (info.clip == null)
            info.clip = (AudioClip)Resources.Load(info.resourceName);

        if(soundPlayer == null)
        {
            soundPlayer = new GameObject("SoundPlayer");
            audioSource = soundPlayer.AddComponent<AudioSource>();
        }

        float len = info.clip.length;
        if (info.stockList.Count > 0)
        {
            SEInfo seInfo = info.stockList.Values[0];
            seInfo.curTime = len;
            info.playingList.Add(seInfo);

            // remove from stock
            info.stockList.Remove(seInfo.index);

            // Play SE
            audioSource.PlayOneShot(info.clip, seInfo.volume);

            return true;
        }
        return false;
    }

    public void Update()
    {
        // playing SE update
        foreach (AudioClipInfo info in audioClips.Values)
        {
            List<SEInfo> newList = new List<SEInfo>();

            foreach (SEInfo seInfo in info.playingList)
            {
                seInfo.curTime = seInfo.curTime - Time.deltaTime;
                if (seInfo.curTime > 0.0f)
                    newList.Add(seInfo);
                else
                    info.stockList.Add(seInfo.index, seInfo);
            }
            info.playingList = newList;
        }

        //BGM
        CurBGMPlayer.UpdateBGM();
        FadeOutBGMPlayer.UpdateBGM();
    }

    //BGMを再生
    public void PlayBGM(string bgmName, float fadeTime)
    {
        if (CurBGMPlayer != null && CurBGMPlayer.FadeOutFlg == false)
            CurBGMPlayer.PlayBGM();
        if (FadeOutBGMPlayer != null && FadeOutBGMPlayer.FadeOutFlg == false)
            FadeOutBGMPlayer.PlayBGM();

        // play new BGM
        if (audioClips.ContainsKey(bgmName) == false)
        {
            // null BGM
            CurBGMPlayer = new BGMPlayer();
        }
        else
        {
            CurBGMPlayer = new BGMPlayer(audioClips[bgmName].resourceName);
            CurBGMPlayer.PlayBGM(fadeTime);
        }
    }

    public void PauseBGM()
    {
        if (CurBGMPlayer != null) CurBGMPlayer.PauseBGM();
        if (FadeOutBGMPlayer != null) FadeOutBGMPlayer.PauseBGM();
    }

    public void StopBGM(float fadeTime)
    {
        if (CurBGMPlayer != null) CurBGMPlayer.StopBGM(fadeTime);
        if (FadeOutBGMPlayer != null) FadeOutBGMPlayer.StopBGM(fadeTime);
    }
}
