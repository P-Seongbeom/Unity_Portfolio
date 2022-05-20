using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [System.Serializable]
    public struct BgmType
    {
        public string Name;
        public AudioClip Audio;
    }

    public BgmType[] BgmList;

    private AudioSource _bgm;
    private string _currentBgm;

    private void Start()
    {
        _bgm = gameObject.AddComponent<AudioSource>();
        _bgm.loop = true;
        _bgm.volume = 0.8f;
        if(BgmList.Length > 0)
        {
            PlayBGM(BgmList[0].Name);
        }
    }

    public void PlayBGM(string name)
    {
        if(_currentBgm == name)
        {
            return;
        }

        for(int i = 0; i < BgmList.Length; ++i)
        {
            if(BgmList[i].Name == name)
            {
                _bgm.clip = BgmList[i].Audio;
                _bgm.Play();
                _currentBgm = name;
            }
        }
    }
}
