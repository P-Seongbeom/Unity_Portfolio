using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest Data", menuName = "Scriptable Object/Quest Data", order = int.MaxValue)]
public class QuestData : ScriptableObject
{
    [SerializeField]
    private string questName;
    public string QuestName { get { return questName; } }

    [SerializeField]
    private int[] npcId;
    public int[] NpcId { get { return npcId; } }

    public QuestData(string name, int[] npc)
    {
        questName = name;
        npcId = npc;
    }
}
