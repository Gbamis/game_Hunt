using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace HT
{
    public class BreedingSystem : MonoBehaviour
    {
        public GameEvent gameEvent;
        // public Animal animalPrefab;
        public Animal male_goat;
        public Animal female_goat;
        public Transform maleCattleHolder;
        public Transform femaleCattleHolder;

        private void Start()
        {
            gameEvent.OnGet_BreedingSystem += () => this;
        }

        public void SetStartData(AnimalGene male, AnimalGene female)
        {

            male_goat.Birth(new Gene()
            {
                color = male.skinColor,
                maxGrowthSize = male.maxGrowthSize,
                tirednessRate = male.tirednessRate,
                diseaseRate = male.diseaseRate,
                reproductionRate = male.reproductionRate,
                red = male.red,
                green = male.green,
                blue = male.blue
            }, Gender.MALE);

            female_goat.Birth(new Gene()
            {
                color = female.skinColor,
                maxGrowthSize = female.maxGrowthSize,
                tirednessRate = female.tirednessRate,
                diseaseRate = female.diseaseRate,
                reproductionRate = female.reproductionRate,
                red = female.red,
                green = female.green,
                blue = female.blue
            }, Gender.FEMALE);
        }

        public void BreedGoat(IGoat male, IGoat female, Transform trans)
        {
            Gender gender = Random.Range(0, 10) % 2 == 0 ? Gender.MALE : Gender.FEMALE;
            Animal offspring = gender==Gender.MALE? Instantiate(male_goat) : Instantiate(female_goat);
            offspring.Birth(CreateGeneFromParents(male.gene, female.gene, gender), gender);

            Vector2 rand = Random.insideUnitCircle * 4;
            Vector3 pos = trans.position;
            pos.x += rand.x;
            pos.z += rand.y;

            offspring.gameObject.transform.position = pos;
        }

        private Gene CreateGeneFromParents(Gene father, Gene mother, Gender gender)
        {
            Gene gene = new()
            {
                color = MixSkinColor(father.color, mother.color),
                tirednessRate = MixTraits(father.tirednessRate, mother.tirednessRate),
                reproductionRate = gender == Gender.FEMALE ? mother.reproductionRate : MixTraits(father.reproductionRate, mother.reproductionRate),
                diseaseRate = MixTraits(father.diseaseRate, mother.diseaseRate),
                red = MixTraits(father.red, mother.red),
                green = MixTraits(father.green, mother.green),
                blue = MixTraits(father.blue, mother.blue)
            };
            return gene;
        }

        private Color MixSkinColor(Color father, Color mother)
        {
            Color color;
            color.r = (father.r + mother.r) / 2;
            color.g = (father.g + mother.g) / 2;
            color.b = (father.b + mother.b) / 2;
            color.a = 1;
            return color;
        }
        private float MixTraits(float male, float female) => (male + female) / 2;

        public void SetParentofGoat(Transform obj, Gender gender)
        {
            if (gender == Gender.MALE) { obj.SetParent(maleCattleHolder); }
            else if (gender == Gender.FEMALE) { obj.SetParent(femaleCattleHolder); }
            gameEvent.OnBreedStatChanged?.Invoke(maleCattleHolder.childCount, femaleCattleHolder.childCount);
        }
        public void RemoveParentofGoat(Transform obj)
        {
            obj.SetParent(null);
            gameEvent.OnBreedStatChanged?.Invoke(maleCattleHolder.childCount, femaleCattleHolder.childCount);
        }
    }

}