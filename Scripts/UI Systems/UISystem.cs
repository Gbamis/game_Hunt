using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public class UISystem : MonoBehaviour
    {
        private bool isMenuOpen;
        public GameEvent gameEvent;
        public UI_Menu_Canvas menu_canvas;
        public UI_Command_Canvas command_canvas;
        public UI_Dialog_Canvas dialog_canvas;
        public UI_Gameplay_Canvas gameplay_canvas;
        public UI_Breeding_Canvas breeding_Canvas;
        public UI_Bank_Canvas bank_Canvas;
        public UI_Quest_Canvas quest_Canvas;


        private void Start()
        {
            InitializeCanvas();

            //menuRect = menu_canvas.GetComponent<RectTransform>();

            gameEvent.OnGet_UISystem += () => this;
            gameEvent.OnShowContextMenu += (ctx) =>
            {
               menu_canvas.gameObject.SetActive(true);
            };
        }

        private void InitializeCanvas()
        {
            menu_canvas.OnInit(gameEvent);
            command_canvas.OnInit(gameEvent);
            dialog_canvas.OnInit(gameEvent);
            gameplay_canvas.OnInit(gameEvent);
            breeding_Canvas.OnInit(gameEvent);
            bank_Canvas.OnInit(gameEvent);
            quest_Canvas.OnInit(gameEvent);

            menu_canvas.gameObject.SetActive(isMenuOpen);
        }

        private void OnDestroy()
        {
            gameEvent.OnGet_UISystem -= () => this;
            gameEvent.OnActivateCommandCanvas -= (ctx) => command_canvas.gameObject.SetActive(ctx);
        }

         public void CloseMenuItems()
        {
            Debug.Log("omo");
        }
    }
}
