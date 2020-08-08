using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.Resources
{
    public class HealthDisplay : MonoBehaviour
    {
        TextMeshProUGUI healthPercentage;
        Health playerHealth;
        int startingHealth;

        private void Awake()
        {
            healthPercentage = GetComponent<TextMeshProUGUI>();
            playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        // Start is called before the first frame update
        void Start()
        {
            startingHealth = playerHealth.show_current_health;
            // print("starting health: " + startingHealth);
        }

        // Update is called once per frame
        void Update()
        {
            healthPercentage.text = 
            String.Format("{0:N1}%", playerHealth.GetHealthPercentage());
        }
    }
}
