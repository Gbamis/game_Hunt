using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    [System.Serializable]
    public class PlayerConfig
    {
        [HideInInspector] public Animator anim;
        [HideInInspector] public CharacterController controller;
        [HideInInspector] public PlayerInput player_input;
        public Transform cam;
    }

    public class Player : MonoBehaviour
    {
        public GameEvent gameEvent;

        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private List<Ability> abilities;
        private bool canUpdateAbilities;

        private void Awake()
        {
            playerConfig.player_input = new PlayerInput();

            playerConfig.anim = GetComponent<Animator>();
            playerConfig.controller = GetComponent<CharacterController>();

            canUpdateAbilities = true;
            gameEvent.OnBlockPlayerInput += (ctx) =>
            {
                canUpdateAbilities = !ctx;
                if (ctx == true)
                {
                    foreach (Ability ab in abilities) { ab.OnReset(); }
                }
            };

        }

        private void OnEnable() => playerConfig.player_input.Gameplay.Enable();
        private void OnDisable() => playerConfig.player_input.Gameplay.Disable();

        private void Start()
        {
            foreach (Ability ab in abilities) { ab.OnInit(playerConfig); }
        }
        private void Update()
        {
            if (canUpdateAbilities)
            {
                foreach (Ability ab in abilities) { ab.OnUpdate(); }
            }

        }


    }

}