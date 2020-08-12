using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using RPG.Saving;
using NaughtyAttributes;
using RPG.Stats;
using RPG.Core;
using GameDevTV.Utils;
using UnityEngine.Events;
using RPG.UI.DamageHUD;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        private LazyValue<int> health = null;
        public int show_current_health
        {
            get { return health.value; }
        }
        private int startingHealth;
        public int show_max_health
        {
            get { return startingHealth; }
        }
        public bool isDead {get; private set;} = false;
        BaseStats  baseStats;

        [SerializeField] DamageTextSpawner damageTextSpawner;
        
        // might make non-static and just create reference
        // in HealthBar.cs instead later
        public static event Action onHealthChange;

        private void Awake()
        {
            baseStats = GetComponent<BaseStats>();
            
            // pass in a function
            health = new LazyValue<int>(GetInitialHealth);
        }

        // used to use Awake() instead of Start()
        private void Start()
        {
            // if before Start(), nothing has accessed (or tried to) health,
            // then we force an initialization, so that other scripts that
            // access health.value down the line will get a correct health value
            health.ForceInit();

            startingHealth = health.value;
        }

        public int GetInitialHealth()
        {
            return baseStats.GetStats(StatEnum.Health);
        }

        private void OnEnable()
        {
            // let ResetHealthToMax() subscribe to the BaseStats action event
            baseStats.resetPlayerHealth += ResetHealthToMax;
        }

        private void OnDisable()
        {
            // let ResetHealthToMax() unsubscribe to the BaseStats action event
            baseStats.resetPlayerHealth -= ResetHealthToMax;
        }

        public void ResetHealthToMax()
        {
            health.value = startingHealth;
        }

        public float GetHealthPercentage()
        {
            return (((float)health.value / (float)startingHealth) * 100);
        }

        public float GetHealthDecimalValue()
        {
            return ((float)health.value / (float)startingHealth);
        }

        // instigator is the one who is causing the damage
        public void TakeDamage(int damage, GameObject instigator)
        {
            if (gameObject.tag != "Player")
            {
                print(gameObject.name + " is getting damaged" + damage + " by the " + instigator.name);
            }
            
            health.value = Mathf.Max(health.value - damage, 0);

            // Syncs health bar with the character's health
            onHealthChange();

            if (health.value <= 0)
            {
                Die();
                AwardEXP(instigator);
            }
            else
            {
                print("damaging " + this.gameObject.name);
                // spawn a floating damage display
                damageTextSpawner.Spawn(damage);
            }
        }

        private void AwardEXP(GameObject instigator)
        {
            // the one causing the damage gets points (should only be the player)
            Experiences experienceComponent = instigator.GetComponent<Experiences>();

            // null check will prevent enemies from getting experience points
            // since they do not have a 'Experiences' attached to them
            if (experienceComponent != null)
            {
                // gives the player exp points by searching in the right place in the enemy's stats
                int howMuchExpToGain = GetComponent<BaseStats>().GetStats(StatEnum.ExperienceReward);
                experienceComponent.GainExperience(howMuchExpToGain);
            }
        }

        private void Die()
        {
            if (isDead) { return; }
            isDead = true;
            GetComponent<Animator>().SetTrigger("death");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            // integers are one of the basic data types in C# that are serializable by default
            return health.value;
        }

        public void RestoreState(object state)
        {
            int prevHealthState = (int) state;

            health.value = prevHealthState;

            if (health.value <= 0)
            {
                Die();
            }
        }
    }
}
