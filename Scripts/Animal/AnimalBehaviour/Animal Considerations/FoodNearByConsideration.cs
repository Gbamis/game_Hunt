using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    [CreateAssetMenu(fileName = "food_near_Consideration", menuName = "Games/Hunt/AI/AnimalConsideration/FoodNear")]
    public class FoodNearByConsideration : AnimalConsiderations
    {
        public float searchRadius;
        public LayerMask foodLayer;


        public override float CalculateScore(AnimalStat animalStat) => FoundFood(animalStat) ? 1 : 0;

        private bool FoundFood(AnimalStat animalStat)
        {
            Collider[] colliders = new Collider[1];
            Physics.OverlapSphereNonAlloc(animalStat.animaTransform.position, searchRadius, colliders, foodLayer);
            if (colliders[0] != null)
            {
                animalStat.place_of_action = colliders[0].transform;
                return true;
            }
            return false;
        }
    }

}