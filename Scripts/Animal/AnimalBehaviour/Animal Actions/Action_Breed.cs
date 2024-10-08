
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "Action_Breed", menuName = "Games/Hunt/AI/AnimalActions/Breeding/Mate")]
    public class Action_Breed : AnimalAction
    {
        public LayerMask mateMask;
        private bool active;
        

        public override void Awake() => base.Awake();

        public override void Execute(AnimalController animalController)
        {
            base.Execute(animalController);
            animalController.gameEvent.OnGet_CommandSystem().iscOmmandMode = true;

            active = true;
            Routine.Instance.Run(OnUpdate());

            IEnumerator OnUpdate()
            {
                while (active && !Input.GetKey(KeyCode.Escape))
                {
                    if (Input.GetMouseButton(0))
                    {
                        OnMouseMove(animalController);
                    }

                    yield return null;
                }
            }
        }

        private void OnMouseMove(AnimalController animalController)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 200, mateMask))
            {
                if (hit.collider != null)
                {
                    IGoat male = animalController.GetComponent<IGoat>();
                    IGoat female = hit.collider.GetComponent<IGoat>();

                    if (female.isOnHeat())
                    {
                        //animalController.Execute__Mate_Command(hit.collider.transform);

                        if (female.gender == Gender.FEMALE)
                        {
                            hit.collider.GetComponent<AnimalController>().
                            Execute_Reproduce_Action(male, female,animalController.gene.reproductionRate);
                        }
                    }
                    active = false;
                }
            }
        }
    }

}