using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HT
{
    public class QuestSystem : MonoBehaviour
    {
        public GameEvent gameEvent;
        public Transform root;
        public List<Quest> uncompletedQuests = new List<Quest>();


        private Quest quest;
        public Quest CurrentQuest
        {
            set
            {
                if (quest != null) { quest.PauseQuest(); }
                quest = value;
                if (!uncompletedQuests.Contains(value))
                {
                    uncompletedQuests.Add(value);
                }
                quest.StartQuest(root);
                gameEvent.OnQuestTracked?.Invoke(quest);
            }
            get => quest;
        }

        private void Start()
        {
            gameEvent.OnGetQuestSystem += () => this;
        }

    }

}