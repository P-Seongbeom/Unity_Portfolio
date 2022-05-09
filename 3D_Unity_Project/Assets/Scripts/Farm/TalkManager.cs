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

    // Update is called once per frame
    void GenerateData(int index)
    {
        talkData.Add(TalkData[index].TalkId, TalkData[index].TalkDialogue);

        //talkData.Add(TalkData[10 + index].TalkId, TalkData[10 + index].TalkDialogue);
    }

    public string GetDialogue(int id, int dialogeIndex)
    {
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
