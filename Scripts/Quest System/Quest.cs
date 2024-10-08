using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public enum Q_STATE
    {
        UNINITIALIZED, STARTED, RUNNING, PAUSED, COMPLETED
    }

    [CreateAssetMenu(fileName = "Quest", menuName = "Games/Hunt/Quest/quest")]
    public class Quest : ScriptableObject
    {
        public string description;
        public Q_STATE quest_state;
        public QuestProcessor questProcessor;
        public List<Quest> childQuests;

        public void StartQuest(Transform root)
        {
            QuestProcessor quest = Instantiate(questProcessor,root);
            quest.gameObject.SetActive(true);
            quest_state = Q_STATE.STARTED;
        }

        public void PauseQuest()
        {
            quest_state = Q_STATE.PAUSED;
        }
    }
}
