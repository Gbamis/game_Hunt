using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public class UI_Quest_Canvas : UICanvas
    {
        public Transform list_view;
        public UI_QuestInfo quest_prefab;

        public override void OnInit(GameEvent ge)
        {
            base.OnInit(ge);
            gameEvent = ge;

            gameEvent.OnQuestTracked += (ctx) => CreateQuestItem(ctx);
        }

        public override void OnDisplay()
        {
            base.OnDisplay();

        }

        public override void OnHide()
        {
            base.OnHide();
        }

        private void CreateQuestItem(Quest quest)
        {
            UI_QuestInfo info = Instantiate(quest_prefab, list_view);
            info.Describe(quest.description);
            info.gameObject.SetActive(true);
        }
    }

}