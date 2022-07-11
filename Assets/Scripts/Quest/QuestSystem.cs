using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestSystem : MonoBehaviour

{
    [SerializeField] List<Quest> activeQuests;
    //[SerializeField] List<Quest> closedQuests;

    [SerializeField] private Text QuestText;
    [SerializeField] private Text QuestGiverName;
    [SerializeField] private GameObject QuestNPC;
    
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
    public void NewQuest(Quest Quest)
    {
        ShowRefusalButton();
        ShowQuest(Quest);
    }
    public void ConfirmQuest()
    {
        GameObject playerCollisoin = GameObject.Find("Player").GetComponent<PlayerCollision>().collisionObject;     
        Quest confirmQuest = FindQuestsByNPC(playerCollisoin);  

        if (confirmQuest == null) //Якщо квест ще не існує
        {
            Quest npcQuest = playerCollisoin.GetComponent<NPCInteraction>().GetQuest();
            AddQuest(playerCollisoin, npcQuest);
        }
        else if (!confirmQuest.completed) //Якщо квест ще не виконаний
        {
            AddQuest(playerCollisoin, confirmQuest);
        }
        else // Якщ квест існує та виконаний
        {
            confirmQuest.GetRewards();
            GameObject.Find("Player").GetComponent<PlayerInventory>().AddGetRewardsAfterQuest(confirmQuest.reward);
            confirmQuest.giver.GetComponent<NPCInteraction>().ChangeQuestStatus(NPCInteraction.QuestStatuses.noQuests);
            
            //closedQuests.Add(confirmQuest);
            activeQuests.Remove(confirmQuest);
            Destroy(GameObject.Find(confirmQuest.name));

            CloseQuestWindow();
        }

    }
    private void AddQuest(GameObject questGiver, Quest quest) 
    {

        if (Quest.QuestType.kill == quest.questType)
        {
            //dynamic questGoal = null;

            questGiver.GetComponent<NPCInteraction>().ChangeQuestStatus(NPCInteraction.QuestStatuses.questActive);

            if (quest.goal == null) 
            {
                GameObject questGoal = Instantiate(QuestNPC, new Vector3(1.0f, -0.5f, 0f), Quaternion.Euler(0f, 0f, 0f));
                questGoal.name = "" + quest.goalName;
                quest.goal = questGoal;
                questGoal.GetComponent<NPCInteraction>().InitializeNewNPC(questGoal.name);
                questGoal.GetComponent<NPCInteraction>().ChangeStatusNPC(NPCInteraction.npcStatusEnum.unfriendly);
                
                quest.goal = questGoal;

            }
            else //Якщо НПС існує 
            {
                //questGoal = GameObject.Find((string)quest.goalName).GetComponent<NPCInteraction>();
            }

        }

        questGiver.GetComponent<NPCInteraction>().ChangeQuestStatus(NPCInteraction.QuestStatuses.questActive);

        activeQuests.Add(quest);

        CloseQuestWindow();
    }
    public void RefusalQuest()
    {
        CloseQuestWindow();
    }
    public void CompleteQuest(Quest Quest)
    {
        CloseRefusalButton();
        ShowQuest(Quest);
    }
    private void ShowQuest(Quest Quest) 
    {
        QuestText.text = Quest.questText;
        QuestGiverName.text = Quest.giver.name;
    }
    public void CheckQuest(GameObject gameCheckObject)
    {

        foreach (var activeQuest in activeQuests)
        {
            if ((activeQuest.goal == gameCheckObject) && (activeQuest.questType == Quest.QuestType.kill))
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
                return activeQuest;
            }
        }

        return null;
    }

    //Візуалізація
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
        RefusalButton.gameObject.SetActive(false);
    }

}
