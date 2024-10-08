using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HT
{
    public class UI_Command : MonoBehaviour, IPointerClickHandler
    {
        private AnimalAction  command;
        private CommandSystem cs;

        public Image iconpart;

        public void SetCommand(AnimalAction cmd, CommandSystem cmds, Sprite icon)
        {
            command = cmd;
            cs = cmds;
            iconpart.sprite = icon;
        }

        public void OnPointerClick(PointerEventData ped) => cs.ExecuteCommandOn(command);


    }
}
