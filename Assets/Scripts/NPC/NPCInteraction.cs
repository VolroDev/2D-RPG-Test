using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    public string npcName;

    [SerializeField] private QuestSystem questSystem;
    [SerializeField] private bool questPossible;
    [SerializeField] private bool questCompleted;  
    private int questCount;

    [SerializeField] private Image npcStatusImage;
    [SerializeField] Sprite questionMark;
    [SerializeField] Sprite exclamationMark;

    public enum QuestStatuses
    {
        noQuests, newQuestExists, questActive, questCompleted
    }

    [SerializeField] private QuestStatuses questStatus;

    public enum npcStatusEnum
    {
        friendly, neutral, unfriendly
    }

    public npcStatusEnum npcStatus;

    private void Start()
    {
        npcName = this.name;
        npcStatusImage.enabled = false; //off image
        questSystem = GameObject.Find("EventSystem").GetComponent<QuestSystem>();

        if (npcStatus == npcStatusEnum.friendly)
        {
            questStatus = QuestStatuses.newQuestExists;
            questPossible = true;
        }
        else 
        {
            questPossible = false;
        }
    }

    public void Update()
    {
        //interaction with friendly npc
        if (npcStatus == npcStatusEnum.friendly)
        {
            NPCImageStatus();
        }
        else if (npcStatus == npcStatusEnum.neutral) { }
        else if (npcStatus == npcStatusEnum.unfriendly) { }
        else { }

    }

    private void OnMouseDown()
    {
        if (GameObject.Find("Player").GetComponent<PlayerCollision>().EnterInCollision)
        {
            Debug.Log("clicked NPC");
            if (npcStatus == npcStatusEnum.friendly)
            {
                if (questStatus == QuestStatuses.newQuestExists)
                {
                    questSystem.OpenQuestWindow();
                    questSystem.NewQuest(this.gameObject, questCount++);
                }

                if (questStatus == QuestStatuses.questCompleted)
                {
                    questSystem.OpenQuestWindow();
                    questSystem.CompleteQuest(this.gameObject);
                }
            }
            else if (npcStatus == npcStatusEnum.neutral) { }
            else if (npcStatus == npcStatusEnum.unfriendly) { }
            else { }
        }

    }

    public void ChangeStatusNPC (npcStatusEnum status)
    {
        npcStatus = status;
    }

    public void ChangeQuestStatus(QuestStatuses status)
    {
        questStatus = status;
    }

    public void NPCImageStatus()
    {
      
        if (questStatus == QuestStatuses.newQuestExists)
        {
            npcStatusImage.sprite = questionMark;
            npcStatusImage.enabled = true;
        }
        else if (questStatus == QuestStatuses.questCompleted)
        {
            npcStatusImage.sprite = exclamationMark;
            npcStatusImage.enabled = true;
        }
        else 
        {
            npcStatusImage.sprite = null;
            npcStatusImage.enabled = false;
        }
    }

    public void EventBeforeDestruction()
    {
        questSystem.CheckQuest(this.gameObject);
    }
    
}
