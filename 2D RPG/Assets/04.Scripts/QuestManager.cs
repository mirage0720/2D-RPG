using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public GameObject[] questObject;

    Dictionary<int, QuestData> qusetList;

    void Awake()
    {
        qusetList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    private void GenerateData()
    {
        qusetList.Add(10, new QuestData("마을 사람들과 대화하기."
                                            , new int[] {1000, 2000}));
        qusetList.Add(20, new QuestData("루도의 동전 찾아주기."
                                            , new int[] {5000, 2000}));
        qusetList.Add(30, new QuestData("퀘스트 올 클리어!"
                                            , new int[] {0}));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        //Next Talk Target
        if(id == qusetList[questId].npcId[questActionIndex])
            questActionIndex++;

        //Control Quest Object
        ControlObject();


        //Talk Complete & Next Quest
        if(questActionIndex == qusetList[questId].npcId.Length)
            NextQuest();

        //Quest Name
        return qusetList[questId].questName;
        
    }

    public string CheckQuest()
    {
        //Quest Name
        return qusetList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    void ControlObject()
    {
        switch(questId){
            case 10 :
                if(questActionIndex ==2)
                    questObject[0].SetActive(true);
                break;
            case 20 :
                if(questActionIndex ==1)
                    questObject[0].SetActive(false);
                break;
        }
    }
}
