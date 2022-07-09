using System;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public string identifier;
    public string questName;
    public GameObject giver;
    //public string Location;
    //public string Prerequisite;
    //public string Faction;
    public string questText;
    public bool active;
    public bool completed;
    public object goal;
    public object goalName;

    public enum QuestType
    {
        kill
    }

    public QuestType questType;

    public enum RewardType
    {
        gold
    }

    public int reward;

    public void CreateNewQuest(GameObject QuestGiver, int QuestNumber)
    {
        identifier = QuestGiver.name + QuestNumber;
        giver = QuestGiver;
        active = true;
        completed = false;

        int unumLenght = Enum.GetValues(typeof(QuestType)).Length;
        QuestType randomQuestType = (QuestType)UnityEngine.Random.Range(0, unumLenght);

        unumLenght = Enum.GetValues(typeof(RewardType)).Length;
        RewardType randomRewardType = (RewardType)UnityEngine.Random.Range(0, unumLenght);

        if (randomQuestType == QuestType.kill)
        {
            ////якщо потр≥бно буде ≥з ≥снуючих Їлемент≥в обрати
            //GameObject[] enemys = GameObject.FindGameObjectsWithTag("enemy");
            //GameObject randomEnemy = enemys[UnityEngine.Random.Range(0, enemys.Length)];
            //goal = randomEnemy;
            //goalName = randomEnemy.name;

            goalName = "Emperor Mavr";

            questName = "Kill " + goalName;
            questText = "Kill " + goalName + ". Ok?";

        }
        //else if (randomQuestType == QuestType.courier)
        //{

        //    questIdentifier = QuestGiver.name + QuestNumber;
        //    questName = "Random quest name";
        //    questText = "Random quest text";

        //}

        if (randomRewardType == RewardType.gold) 
        {
            //reward = 100;
        }
    

    }

    public void GetRewards()
    {

        reward = 100;

    }

    public void ChangeQuestText() 
    {
        questText = "Wow! TY my bro for";

        if (questType == QuestType.kill)
        {

            questText = questText + "Kill " + goalName;

        }
    }

}
