using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "VaccineAction", menuName = "Games/Hunt/Nutrition/Actions/Vaccine")]
    public class VaccineAction : NutriAction
    {
        public GameEvent gameEvent;
        public LayerMask layerMask;
        public Texture2D cursorIcon;

        public override void Init() => Reset();
        public override void Buy(int count)
        {
            spawnCount = count;
            float total = cost * spawnCount;
            if (gameEvent.OnGet_BankSystem().Debit(total))
            {
                Cursor.SetCursor(cursorIcon,Vector2.zero,CursorMode.Auto);
                
                Routine.Instance.Run(Move());
            }

        }

        public override void Reset() { base.Reset(); spawnCount = 0; }

        private IEnumerator Move()
        {
            while (spawnCount > 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit, 200, layerMask))
                    {
                        if (hit.collider != null)
                        {
                            IHealth health = hit.collider.GetComponent<IHealth>();
                            health.Vaccinate();
                            spawnCount--;
                        }
                    }   
                    
                }

                yield return null;
            }
            Cursor.SetCursor(gameEvent.defCursorIcon,Vector2.zero,CursorMode.Auto);
        }
    }

}