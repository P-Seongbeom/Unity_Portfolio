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

    [SerializeField]
    private string questDescription;
    public string QuestDescription { get { return questDescription; } }

    [SerializeField]
    private int rewardGold;
    public int RewardGold { get { return rewardGold; } }

    [SerializeField]
    private string[] rewardCard;
    public string[] RewardCard { get { return rewardCard; } }
}
