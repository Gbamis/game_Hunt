using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace HT
{
    public enum MouseMode { COMMAND, UI }
    public class CommandSystem : MonoBehaviour
    {
        private bool opened;
        public GameEvent gameEvent;
        public MouseMode mouseMode;
        public Transform player;

        public List<AnimalAction> commands;
        private List<AnimalAction> runtime_commands = new List<AnimalAction>();
        public List<ICommandable> selectedEntities;

        public bool iscOmmandMode;

        private void Start()
        {
            mouseMode = MouseMode.UI;

            gameEvent.OnGet_CommandSystem += () => this;
        }

        public void AddToSelection(ICommandable selectable, Vector3 pos)
        {
            selectedEntities ??= new List<ICommandable>();

            if (!selectedEntities.Contains(selectable)) { selectedEntities.Add(selectable); }

            if (!opened)
            {
                gameEvent.OnActivateCommandCanvas?.Invoke(true);
                opened = true;
            }
            FilterCommands();
        }

        public void RemoveSelected(ICommandable selectable)
        {
            if (selectedEntities.Contains(selectable)) { selectedEntities.Remove(selectable); }
            if (selectedEntities.Count == 0)
            {
                gameEvent.OnActivateCommandCanvas(false);
                opened = false;
            }
            else { FilterCommands(); }
        }

        private void FilterCommands()
        {
            FILTER filter = FILTER.SINGLE;
            if (selectedEntities.Count == 2) { filter = FILTER.DOUBLE; }
            else if (selectedEntities.Count > 2) { filter = FILTER.MULTIPLE; }

            var actions = from cmd in commands
                          where cmd.actionFilter.Contains(filter)
                          select cmd;
            List<AnimalAction> cmd_actions = actions.ToList();
            gameEvent.OnLastCommandableSelected(cmd_actions);
        }

        public void RemoveAllSelected()
        {
            if (selectedEntities == null) { return; }
            foreach (ICommandable ss in selectedEntities) { ss.OnDeSelect(); }
            gameEvent.OnActivateCommandCanvas(false);
            opened = false;
            selectedEntities.Clear();
            iscOmmandMode = false;

            foreach (AnimalAction ac in runtime_commands) { Destroy(ac); }
        }


        public void ExecuteCommandOn(AnimalAction command)
        {
            if (selectedEntities == null) { return; }

            foreach (ICommandable ss in selectedEntities)
            {
                AnimalAction cmd = Instantiate(command);
                //runtime_commands.Add(cmd);
                ss.OnExecuteCommandable(cmd);
                Destroy(cmd);
            }
            RemoveAllSelected();
        }
    }

}