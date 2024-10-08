using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = " GameEvents", menuName = "Games/Hunt/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        public Texture2D defCursorIcon;
        public GameObject virusPrefab;

        //Systems
        public Func<MouseSystem> OnGetMouseSystem;
        public Func<CommandSystem> OnGet_CommandSystem;
        public Func<UISystem> OnGet_UISystem;
        public Func<BankSystem> OnGet_BankSystem;
        public Func<BreedingSystem> OnGet_BreedingSystem;
        public Func<Environmental_DayNightCycle> OnGetDayNightCycle;
        public Func<QuestSystem> OnGetQuestSystem;

        //command

        public Action<bool> OnActivateCommandCanvas;
        public Action<List<AnimalAction>> OnLastCommandableSelected;

        //ui

        public Action<Vector2> OnShowContextMenu;
        public Action<GoatInfo, Vector3> OnShowGoatInfo;
        public Action OnHideGoatInfo;


        //Breeding system

        public Action<int, int> OnBreedStatChanged;


        //Quest System

        public Action<Quest> OnQuestTracked;

        public Action<bool> OnBlockPlayerInput;

    }

}