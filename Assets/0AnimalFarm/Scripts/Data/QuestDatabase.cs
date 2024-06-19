using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

[CreateAssetMenu(fileName = "QuestDatabase", menuName = "AnimalFarmstay/QuestDatabase")]
public class QuestDatabase : DescriptionDatabase
{
   
    public QuestType questType;
    [EnumToggleButtons]
    public QuestAction action = QuestAction.COLLECT;
    [HideIf("action", QuestAction.COLLECT)]
    public string idItem;
    public Sprite rewardImg;
    public int total;
    public int coinReward; 
    
}

//COLLECT,เก็บเหรียญ
//SERVICE,เสริฟอาหาร
//WATCH,ชมสวน,ชมธรรมชาติ
//CLEANUP,ทำความสะอาด
//GROW,ปลูกผัก