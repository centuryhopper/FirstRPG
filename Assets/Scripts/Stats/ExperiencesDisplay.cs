using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.Stats
{
    public class ExperiencesDisplay : MonoBehaviour
    {
        Experiences expInfo;
        TextMeshProUGUI expPoints;

        // Start is called before the first frame update
        void Start()
        {
            // get the info from the player
            expInfo = GameObject.FindWithTag("Player").GetComponent<Experiences>();
            expPoints = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            expPoints.text = expInfo.ExperiencePoints.ToString();
        }
    }
}
