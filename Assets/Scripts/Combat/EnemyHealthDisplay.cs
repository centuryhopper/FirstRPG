using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Resources;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        TextMeshProUGUI enemyHealthPercentage;
        Fighter findEnemyTarget;
        int startingHealth;
        bool foundEnemy = false;

        private void Awake()
        {
            findEnemyTarget = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        // Start is called before the first frame update
        void Start()
        {
            enemyHealthPercentage = GetComponent<TextMeshProUGUI>();
            

            // WARNING: referencing here will not carry over per frame
            // Health enemyHealth = GameObject.FindWithTag("Player").GetComponent<Fighter>().GetTarget();
        }

        // Update is called once per frame
        void Update()
        {
            Health enemyHealth = findEnemyTarget.GetTarget();
            
            // reference to a potential target
            if (enemyHealth != null)
            {
                startingHealth = enemyHealth.show_current_health;
                enemyHealthPercentage.text =
                String.Format("{0:N1}%", enemyHealth.GetHealthPercentage());
            }
            else
            {
                enemyHealthPercentage.text = "N/A";
            }
        }
    }
}
