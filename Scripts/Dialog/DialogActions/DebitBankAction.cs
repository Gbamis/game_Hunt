using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "DialogActionHolder", menuName = "Games/Hunt/Dialog/Actions/DialogActionHolder")]
    public class DebitBankAction : DialogAction
    {
        public GameEvent gameEvent;
        public float amount;
        private Action action;
        public Quest quest;

        public override void InjectFunc(Action m_action)
        {
            base.InjectFunc(action);
            action = m_action;
        }

        public override void OnExecuteDialogAction()
        {
            gameEvent.OnGet_BankSystem().Credit(amount);
            action.Invoke();
            gameEvent.OnGetQuestSystem().CurrentQuest = quest;
        }
    }
}
