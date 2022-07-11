using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    public string npcName;
    public string guidNPC;

    [SerializeField] private QuestSystem questSystem;
    [SerializeField] private Quest quest;
    [SerializeField] private bool questPossible;
    [SerializeField] private bool questCompleted;  

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
        if (npcName == "") 
        {
            InitializeNewNPC(name);
        }

        npcStatusImage.enabled = false; //off image
        questSystem = GameObject.Find("EventSystem").GetComponent<QuestSystem>();

        if (npcStatus == npcStatusEnum.friendly)
        {
            questStatus = QuestStatuses.newQuestExists;
            questPossible = true;
            
            GameObject gameObject = new GameObject("Quest");
            quest = gameObject.AddComponent<Quest>();
            quest.name = npcName + " quest";
            quest.CreateNewQuest(this.gameObject);
        }
        else 
        {
            questPossible = false;
        }
    }

    public void InitializeNewNPC(string initializeName) 
    {
        npcName = initializeName;
        guidNPC = CreateNPCGUID();
        name = npcName + guidNPC;
    }

    private string CreateNPCGUID() 
    {
        if (guidNPC == "")
        {
            return "" + System.Guid.NewGuid();
        }
        else 
        {
            return guidNPC;
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
                    questSystem.NewQuest(quest);
                }

                if (questStatus == QuestStatuses.questCompleted)
                {
                    questSystem.OpenQuestWindow();
                    questSystem.CompleteQuest(quest);
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

        if (this.quest == null) 
        {
            questStatus = QuestStatuses.noQuests;
            npcStatusImage.sprite = null;
            npcStatusImage.enabled = false;
        }
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
    
    public Quest GetQuest() 
    {
        return quest;
    }

}
