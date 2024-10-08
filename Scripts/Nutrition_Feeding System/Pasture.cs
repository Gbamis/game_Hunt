using System;
using UnityEngine;

namespace HT
{
    public class Pasture : MonoBehaviour
    {
        [SerializeField] private Grass grass_prefab;
        [SerializeField] private Transform items;
        [SerializeField] private Transform spawnRoot;
        //public Transform[] slots;
        [SerializeField] private LayerMask mask;
        [SerializeField] private float hieghtCheck;


        private void Start() => CreatePasture();

        public void CreatePasture()
        {
            for (int i = 0; i < spawnRoot.childCount; i++)
            {
                Vector3 sp = Point(spawnRoot.GetChild(i).position);
                Grass grass = Instantiate(grass_prefab, sp, Quaternion.identity, items);
                grass.gameObject.SetActive(true);
            }
            spawnRoot.gameObject.SetActive(false);
        }

        public Vector3 Point(Vector3 origin)
        {
            Vector3 pos = Vector3.zero;
            origin.y += hieghtCheck;

            if (Physics.Raycast(origin, Vector3.down, out RaycastHit info, 200, mask))
            {
                if (info.collider != null)
                {
                    pos = info.point;
                }
            }
            return pos;
        }
    }

}