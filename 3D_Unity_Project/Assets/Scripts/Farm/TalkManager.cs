using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    public static TalkManager Instance;
    public TalkData[] TalkData;
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        talkData = new Dictionary<int, string[]>();
        for(int i = 0; i < TalkData.Length; ++i)
        {
            GenerateData(i);
        }
    }

    void GenerateData(int index)
    {
        talkData.Add(TalkData[index].TalkId, TalkData[index].TalkDialogue);
    }

    public string GetDialogue(int id, int dialogeIndex)
    {
        if(!talkData.ContainsKey(id))
        {
            if(!talkData.ContainsKey(id - id % 10))
            {
                //퀘스트 없을 때 기본 대사
                return GetDialogue(id - (id % 100), dialogeIndex);
            }
            else
            { 
                //퀘스트 처음 대사
                return GetDialogue(id - (id % 10), dialogeIndex);
            }
        }

        if(dialogeIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][dialogeIndex];
        }
    }
}
