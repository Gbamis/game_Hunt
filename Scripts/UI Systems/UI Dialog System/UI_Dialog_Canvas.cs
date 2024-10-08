using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HT
{
    [Serializable]
    public struct Callout
    {
        public Text id;
        public Text msg;
        public RectTransform rectTransform;
        public void Enable(bool value) => rectTransform.gameObject.SetActive(value);
        public void Print(string m_id, string m_msg)
        {
            id.text = m_id; msg.text = m_msg;
        }
    }

    public class UI_Dialog_Canvas : UICanvas
    {
        private List<DialogButton> dialogButtons = new List<DialogButton>();
        [SerializeField] private Callout playerCallout;
        [SerializeField] private Callout npcCallout;

        [SerializeField] private Transform buttonList;
        [SerializeField] private DialogButton dialogButton;


        public override void OnInit(GameEvent ge)
        {
            base.OnInit(ge);
            gameEvent = ge;

            DisableDialog();
        }

        public override void OnDisplay() => base.OnDisplay();

        public override void OnHide()
        {
            base.OnHide();

        }

        public void AddResponseBtn(Dialog m_dialog, Response m_response, Action completed)
        {
            DialogButton button = Instantiate(dialogButton, buttonList);
            button.Create(m_dialog, m_response, completed);
            button.gameObject.SetActive(true);
            dialogButtons.Add(button);
        }

        public void ClearResponseBtn()
        {
            foreach (DialogButton bt in dialogButtons) { Destroy(bt.gameObject); }
            dialogButtons.Clear();
        }


        public void DisableDialog()
        {
            playerCallout.Enable(false);
            npcCallout.Enable(false);
        }

        public void OutputMsg(char id, string msg, string npc)
        {
            if (id == 'p')
            {
                playerCallout.Print("Player", msg);
                npcCallout.Enable(false);
                playerCallout.Enable(true);
            }
            else if (id == 'n')
            {
                npcCallout.Print(npc, msg);
                npcCallout.Enable(true);
                playerCallout.Enable(false);
            }
        }
    }
}
