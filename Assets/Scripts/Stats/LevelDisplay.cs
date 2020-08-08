using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        TextMeshProUGUI level;
        BaseStats baseStats;

        // Start is called before the first frame update
        void Start()
        {
            level = GetComponent<TextMeshProUGUI>();
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        // Update is called once per frame
        void Update()
        {
            level.text = baseStats.CalculateLevel().ToString();
        }
    }
}
