using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "Grass_Action", menuName = "Games/Hunt/Nutrition/Actions/Grass")]
    public class GrassAction : NutriAction
    {
        public GameEvent gameEvent;
        public GameObject grassPrefab;
        private GameObject clone;
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
                Spawn();
                Routine.Instance.Run(Move());
            }

        }

        public override void Reset() { base.Reset(); spawnCount = 0; }

        private void Spawn()
        {
            if (spawnCount > 0)
            {
                clone = Instantiate(grassPrefab);
                clone.SetActive(true);
            }
        }


        private IEnumerator Move()
        {
            while (spawnCount > 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 200, layerMask))
                {
                    if (hit.collider != null)
                    {
                        clone.transform.position = hit.point;

                        if (Input.GetMouseButtonDown(0))
                        {
                            Spawn();
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