using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestSystem : MonoBehaviour

{

    [SerializeField] List<Quest> activeQuests;
    [SerializeField] List<Quest> closedQuests;
    [SerializeField] Quest quest;

    [SerializeField] private Text QuestText;
    [SerializeField] private Text QuestGiverName;
    
    public bool DecisionIsMade;
    
    [SerializeField] Button RefusalButton;

    private void Update()
    { 

        foreach (var activeQuest in activeQuests)
        {
            if (activeQuest.completed)
            {

                activeQuest.giver.GetComponent<NPCInteraction>().ChangeQuestStatus(NPCInteraction.QuestStatuses.questCompleted);

            }
        }

    }

    public void NewQuest(GameObject questGiver, int questCount)
    {
        ShowRefusalButton();
        quest.CreateNewQuest(questGiver, questCount);
        QuestText.text = quest.questText;
        QuestGiverName.text = questGiver.name;
        questGiver.GetComponent<NPCInteraction>().ChangeQuestStatus(NPCInteraction.QuestStatuses.questActive);
    }
    public void ConfirmQuest()
    {
        GameObject playerCollisoin = GameObject.Find("Player").GetComponent<PlayerCollision>().collisionObject;     
        Quest confirmQuest = FindQuestsByNPC(playerCollisoin);

        if (confirmQuest == null)
        {
            AddQuest(playerCollisoin);
        }
        else if (!confirmQuest.completed) 
        {
            AddQuest(playerCollisoin);
        }
        else
        {
            confirmQuest.GetRewards();
            GameObject.Find("Player").GetComponent<PlayerInventory>().AddGetRewardsAfterQuest(confirmQuest.reward);
            confirmQuest.giver.GetComponent<NPCInteraction>().ChangeQuestStatus(NPCInteraction.QuestStatuses.noQuests);
            
            closedQuests.Add(confirmQuest);
            activeQuests.Remove(confirmQuest);

            DecisionIsMade = true;
            CloseQuestWindow();
        }
    }

    private void AddQuest(GameObject questGiver) 
    {
        activeQuests.Add(quest);

        if (Quest.QuestType.kill == quest.questType)
        {
            questGiver.GetComponent<NPCInteraction>().ChangeQuestStatus(NPCInteraction.QuestStatuses.questActive);
            var questGoal = GameObject.Find((string)quest.goalName).GetComponent<NPCInteraction>();
            questGoal.npcName = (string)quest.goalName;
            questGoal.ChangeStatusNPC(NPCInteraction.npcStatusEnum.unfriendly);
        }

        //questGiver.GetComponent<NPCInteraction>.

        CloseQuestWindow();
    }

    public void RefusalQuest()
    {
        CloseQuestWindow();
    }

    public void CompleteQuest(GameObject questGiver)
    {
        CloseRefusalButton();
        quest = FindQuestsByNPC(questGiver);
        QuestText.text = quest.questText;
        QuestGiverName.text = questGiver.name;
    }

    public void OpenQuestWindow()
    {
        PauseControl.PauseGame();
        GameObject.Find("CanvasQuestGive").GetComponent<Canvas>().enabled = true;
    }

    public void CloseQuestWindow()
    {
        PauseControl.ResumeGame();
        GameObject.Find("CanvasQuestGive").GetComponent<Canvas>().enabled = false;
    }

    public void ShowRefusalButton()
    {
        RefusalButton.gameObject.SetActive(true);
    }

    public void CloseRefusalButton()
    {
        RefusalButton.gameObject.SetActive(true);
    } 

    public void CheckQuest(GameObject gameCheckObject)
    {

        foreach (var activeQuest in activeQuests)
        {
            if ((activeQuest.goal == gameCheckObject)
                && (activeQuest.questType == Quest.QuestType.kill))
            {
                activeQuest.ChangeQuestText();
                activeQuest.completed = true;
                activeQuest.active = false;
            }
        }

    }

    public Quest FindQuestsByNPC(GameObject NPC)
    {

        foreach (var activeQuest in activeQuests)
        {
            if ((activeQuest.completed) && (activeQuest.giver == NPC))
            {
                quest = activeQuest;
                return activeQuest;
            }
        }

        return null;
    }
}
