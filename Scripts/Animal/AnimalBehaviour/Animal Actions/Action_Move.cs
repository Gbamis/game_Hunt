using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "Action_Move", menuName = "Games/Hunt/AI/AnimalActions/Move")]
    public class Action_Move : AnimalAction
    {
        private bool active;
        public LayerMask mask;

        public override void Awake() => base.Awake();

        public override void Execute(AnimalController animalController)
        {
            base.Execute(animalController);

            active = true;
            Routine.Instance.moveIcon.gameObject.SetActive(true);
            Routine.Instance.Run(OnUpdate());

            IEnumerator OnUpdate()
            {
                while (active)
                {
                    OnMouseMove(animalController);
                    yield return null;
                }
            }
        }

        private void OnMouseMove(AnimalController animalController)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 200, mask))
            {
                if (hit.collider != null)
                {
                    Routine.Instance.moveIcon.transform.position = hit.point;
                    if (Input.GetMouseButton(0))
                    {
                        active = false;
                        Routine.Instance.moveIcon.Dropped();
                        animalController.Execute__Move_Command(Routine.Instance.moveIcon.transform);
                    }
                }
                else { Routine.Instance.moveIcon.Dropped(); }
            }
        }
    }

}