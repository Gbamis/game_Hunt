using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "Action_Idle", menuName = "Games/Hunt/AI/AnimalActions/Idle")]
    public class Action_Idle : AnimalAction
    {
        private int randIdle;
        public override void Awake()
        {
            base.Awake();
            randIdle = 0;
        }

        public override void Execute(AnimalController animalController)
        {
            base.Execute(animalController);
            switch (randIdle)
            {
                case 0:
                    animalController.Execute_Idle_Action(2);
                    break;
                case 1:
                    animalController.Execute__Roam_Action();
                    break;
            }
            randIdle = Random.Range(0, 2);

        }

    }
}
