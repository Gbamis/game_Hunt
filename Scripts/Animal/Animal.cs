using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HT
{
    public enum Gender { MALE, FEMALE }

    public class Animal : MonoBehaviour, IGoat, IHoverable, IHealth
    {
        private GameObject virusIndiccator;
        private bool isSick;
        public Gene gene { set; get; }
        public Gender gender { set; get; }

        public GameEvent gameEvent;
        public AnimalBehaviour animalBehaviour;
        public AnimalController animalController;
        public AnimalPhysical animalPhysical;
        public ParticleSystem spawnFX;
        public string Genenome;
        public AnimalStat animalStat;


        public void Birth(Gene m_gene, Gender m_gender)
        {
            gene = m_gene;
            gender = m_gender;

            animalStat = new AnimalStat(transform);
            animalBehaviour.Init(animalStat);
            animalController.Init(animalStat, gene);
            animalPhysical.SetPhysicalCharcateristics(gene.color);

            gameEvent.OnGet_BreedingSystem().SetParentofGoat(transform, m_gender);

            Genenome = m_gene.StringRepresentation();
            spawnFX.gameObject.SetActive(true);
        }

        public void Vaccinate() { animalStat.ResetStamina(); isSick = false; }
        public bool isOnHeat() => animalStat.GetHeatLevel() > 0.4F ? true : false;
        public void TriggerSickness(float rate)
        {
            isSick = true;
            virusIndiccator ??= Instantiate(gameEvent.virusPrefab, transform.position, Quaternion.identity, transform);
            virusIndiccator.SetActive(true);

            StartCoroutine(KillHealth());

            IEnumerator KillHealth()
            {
                while (isSick && animalStat.GetStamina() > 0)
                {
                    virusIndiccator.transform.GetChild(0).transform.LookAt(Camera.main.transform);
                    animalStat.ReduceStamina(gene.tirednessRate * 20 * gene.diseaseRate);

                    yield return null;
                }
                virusIndiccator.SetActive(false);
                if (animalStat.GetStamina() == 0)
                {
                    GetComponent<Collider>().enabled = false;
                    animalBehaviour.Die();
                    animalController.Die();
                    gameEvent.OnGet_BreedingSystem().RemoveParentofGoat(transform);
                }
            }
        }

        
        public void OnPointerEnter(PointerEventData ped) => OnHighlight();
        public void OnPointerExit(PointerEventData ped) => OnDeHightlight();
        public void OnHighlight()
        {
            GoatInfo goatInfo = new GoatInfo()
            {
                gendder = (gender == Gender.MALE) ? "M" : "f",
                stamina = animalStat.GetStamina(),
                oestrus = animalStat.GetHeatLevel()
            };

            gameEvent.OnShowGoatInfo(goatInfo, transform.position);
        }
        public void OnDeHightlight() => gameEvent.OnHideGoatInfo();


    }

}