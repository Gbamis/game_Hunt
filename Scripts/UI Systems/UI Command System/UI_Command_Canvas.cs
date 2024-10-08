using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace HT
{
    public class UI_Command_Canvas : UICanvas
    {
        [SerializeField] private RectTransform centerRect;
        [SerializeField] private RectTransform commmandListView;
        [SerializeField] private UI_Command cmdPrefab;
        public Vector2 offset;
        public override void OnInit(GameEvent ge)
        {
            base.OnInit(ge);
            gameEvent = ge;
            centerRect.gameObject.SetActive(false);

            gameEvent.OnActivateCommandCanvas += (ctx) => centerRect.gameObject.SetActive(ctx);
            gameEvent.OnLastCommandableSelected += (cmd) => SetItemsByFilter(cmd);
        }

        public override void OnDisplay()
        {
            base.OnDisplay();

        }

        public override void OnHide()
        {
            base.OnHide();
        }

        public void ClearCommands()
        {
            if (commmandListView.childCount > 0)
            {
                foreach (Transform child in commmandListView) { Destroy(child.gameObject); }
            }
        }

        private void PositionCommandOnItem(Vector3 worldPos)
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(worldPos);
            pos += offset;
            centerRect.position = pos;
        }

        public void SetItemsByFilter(List<AnimalAction> commands)
        {
            ClearCommands();
            int i = 0;
            foreach (AnimalAction cmd in commands)
            {
                UI_Command ui = Instantiate(cmdPrefab, commmandListView);
                ui.GetComponent<RectTransform>().localScale = Vector3.one;
                ui.gameObject.SetActive(true);
                ui.SetCommand(cmd, gameEvent.OnGet_CommandSystem(), cmd.icon);
                i++;

            }
        }

        public void SetItems()
        {

            ClearCommands();

            List<AnimalAction> commands = gameEvent.OnGet_CommandSystem().commands;
            int i = 0;
            foreach (AnimalAction cmd in commands)
            {
                UI_Command ui = Instantiate(cmdPrefab, commmandListView);
                ui.GetComponent<RectTransform>().localScale = Vector3.one;
                ui.gameObject.SetActive(true);
                ui.SetCommand(cmd, gameEvent.OnGet_CommandSystem(), cmd.icon);
                i++;

            }
        }

    }
}
