using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;
using GameDevTV.Utils;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        // level of the character
        [Range(1,99)]
        [ShowNonSerializedField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject particleEffectForLevelUp = null;

        // only the player should have this toggled on
        [SerializeField] bool shouldUseModifiers = false;

        public event Action resetPlayerHealth;
        LazyValue<int> currentLevelTracker = null;
        public int CurrentLevelTracker
        {
            get
            {
                return currentLevelTracker.value;
            }
        }

        Experiences exp;
        private void Awake()
        {
            exp = GetComponent<Experiences>();
            currentLevelTracker = new LazyValue<int>(CalculateLevel);
        }

        private void OnEnable()
        {
            if (exp != null)
            {
                // only the player's BaseStats.cs should enter here

                // saves the cost of having to update the level on every frame
                // by subscribing to this method
                exp.onExperienceGained += UpdateLevel;
            }
        }

        private void OnDisable()
        {
            if (exp != null)
            {
                // only the player's BaseStats.cs should enter here

                // UpdateLevel() is unsubscribing from this event
                exp.onExperienceGained -= UpdateLevel;
            }
        }

        private void Start()
        {
            // if (this.transform == this.GetComponent<Transform>())
            // {
            //     print("SAME DAMN THING!!");
            // }
            // get the current level of the player
            // print("started basestats");
            currentLevelTracker.ForceInit();
        }

        public int GetStats(StatEnum stat)
        {
            // damage you can give at your current level + (additive modifier * an additional percentage)
            return (GetBaseStat(stat) + GetAdditiveModifier(stat)) * (1 + (int)(GetPercentageModifiers(stat) / 100));
        }

        private int GetBaseStat(StatEnum stat)
        {
            return progression.GetStats(stat, characterClass, CalculateLevel());
        }

        private int GetAdditiveModifier(StatEnum stat)
        {
            // only the player should get modifier bonuses
            if (!shouldUseModifiers) { return 0; }

            int sum = 0;
            IModifierProvider[] components = GetComponents<IModifierProvider>();

            // get all interface components
            foreach (IModifierProvider provider in components)
            {

                foreach (int modifier in provider.GetAdditiveModifier(stat))
                {
                    sum += modifier;
                }
            }

            return sum;
        }

        // from progression
        private float GetPercentageModifiers(StatEnum stat)
        {
            // only the player should get modifier bonuses
            if (!shouldUseModifiers) { return 0; }

            float sum = 0;
            IModifierProvider[] components = GetComponents<IModifierProvider>();

            // get all interface components
            foreach (IModifierProvider provider in components)
            {
                foreach (float modifier in provider.GetPercentageModifiers(stat))
                {
                    sum += modifier;
                }
            }

            return sum;
        }

        private void UpdateLevel()
        {
            // calculate new level
            int newLevel = CalculateLevel();
            if (newLevel > currentLevelTracker.value)
            {
                currentLevelTracker.value = newLevel;
                print("levelled up!");

                // spawns particle effects signalling that the player has leveled up
                LevelUpEffect();

                // calling this delegate increases health back to maximum when leveled up
                resetPlayerHealth();
            }
        }

        private void LevelUpEffect()
        {
            if (particleEffectForLevelUp != null)
            {
                Instantiate(particleEffectForLevelUp, this.transform);
            }
        }

        // private void Update() {
        //     if (gameObject.tag == "Player")
        //     {
        //         print("Player is at level " + GetCurrentLevel());
        //     }
        // }

        // Calculates the level based off of the exp
        public int CalculateLevel()
        {
            // get current Exp
            // Experiences experience = GetComponent<Experiences>();

            // all enemies will return their own starting level on this method call
            if (exp == null) { return startingLevel; }

            int currentExp = exp.ExperiencePoints;

            // get current num possible levels from progression
            int currentNumPossibleLevels =
            progression.GetNumPossibleLevels(characterClass, StatEnum.ExperienceNeededToLevelUp);

            // levels 1 through max 3 (for now)
            for (int level = 1; level <= currentNumPossibleLevels; ++level)
            {
                // get the num experience points needed at each index
                int numEXPToLevelUp = progression.GetStats(StatEnum.ExperienceNeededToLevelUp, characterClass, level);

                if (currentExp < numEXPToLevelUp) { return level; }
            }
 
            // if we reach here, then we have maxed out the levels array in progression,
            // and therefore need to return 
            return currentNumPossibleLevels + 1;
        }
    }
}
