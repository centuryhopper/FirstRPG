using UnityEngine;
using NaughtyAttributes;
using RPG.Saving;
using System;

namespace RPG.Stats
{
    public class Experiences : MonoBehaviour, ISaveable
    {
        [ShowNonSerializedField] int experiencePoints = 0;
        public int ExperiencePoints { get { return experiencePoints; } }
        public event Action onExperienceGained;
        
        public void GainExperience(int experiencePoints)
        {
            // increase exp points
            this.experiencePoints += experiencePoints;

            // put the listener into action
            onExperienceGained();
        }

        public object CaptureState()
        {
            return experiencePoints;
        }

        public void RestoreState(object state)
        {
            print("restored exp");
            experiencePoints = (int) state;
        }
    }
}
