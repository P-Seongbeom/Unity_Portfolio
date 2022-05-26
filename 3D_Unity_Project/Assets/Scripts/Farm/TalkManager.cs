using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    public static TalkManager Instance;
    public TalkData[] TalkData;
    Dictionary<int, string[]> talkList;

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

        talkList = new Dictionary<int, string[]>();

        for(int i = 0; i < TalkData.Length; ++i)
        {
            GenerateData(i);
        }
    }

    void GenerateData(int index)
    {
        talkList.Add(TalkData[index].TalkId, TalkData[index].TalkDialogue);
    }

    public string GetDialogue(int id, int dialogeIndex)
    {
        if(!talkList.ContainsKey(id))
        {
            if(!talkList.ContainsKey(id - (id % 10)))
            {
                return GetDialogue(id - (id % 100), dialogeIndex);
            }
            else
            { 
                return GetDialogue(id - (id % 10), dialogeIndex);
            }
        }

        if(dialogeIndex == talkList[id].Length)
        {
            return null;
        }
        else
        {
            return talkList[id][dialogeIndex];
        }
    }
}
