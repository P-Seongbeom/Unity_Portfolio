using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Talk Data", menuName = "Scriptable Object/Talk Data", order = int.MaxValue)]
public class TalkData : ScriptableObject
{
    [SerializeField]
    private string objectName;
    public string ObjectName { get { return objectName; } }

    [SerializeField]
    private int talkId;
    public int TalkId { get { return talkId; } }

    [SerializeField]
    private string[] talkDialogue;
    public string[] TalkDialogue { get { return talkDialogue; } }


}
