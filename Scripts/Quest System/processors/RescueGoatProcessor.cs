using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HT
{
    public class RescueGoatProcessor : QuestProcessor
    {
        public Animal goat_for_rescue;
        public Transform rescue_locotaion;


        private void Start(){
            Debug.Log("quest -> rescue stuff has started in world");
        }

        public void SetRescueLocation(){
            
        }
    }

}