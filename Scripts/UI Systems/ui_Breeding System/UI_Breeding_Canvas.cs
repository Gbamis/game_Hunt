using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HT
{
    public class UI_Breeding_Canvas : UICanvas
    {
        public BreedingSystem breedingSystem;
        public List<AnimalGene> animalGenes;
        public Button autoRandomBtn;
        public Button continueBtn;

        public Image maleIcon;
        public Image femaleIcon;


        public override void OnInit(GameEvent ge)
        {
            base.OnInit(ge);
            gameEvent = ge;

            autoRandomBtn.onClick.AddListener(() => GenerateRandom());
            continueBtn.onClick.AddListener(() => OnHide());

        }

        public override void OnDisplay()
        {
            base.OnDisplay();
        }

        public override void OnHide()
        {
            base.OnHide();
            gameObject.SetActive(false);
            gameEvent.OnGetMouseSystem().SetCam(false);
            gameEvent.OnShowContextMenu(Input.mousePosition);
        }

        private void GenerateRandom()
        {
            StartCoroutine(Change());

            IEnumerator Change()
            {
                Random.InitState(System.DateTime.Now.Millisecond);
                int male_rand = Random.Range(0, animalGenes.Count);

                yield return new WaitForSeconds(.1f);

                Random.InitState(System.DateTime.Now.Millisecond);
                int female_rand = Random.Range(0, animalGenes.Count);


                maleIcon.color = animalGenes[male_rand].skinColor;
                femaleIcon.color = animalGenes[female_rand].skinColor;

                breedingSystem.SetStartData(animalGenes[male_rand], animalGenes[female_rand]);
            }





        }
    }

}