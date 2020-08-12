using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 1)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] progressionClasses;
        Dictionary<CharacterClass, Dictionary<StatEnum, int[]>> lookUpTable = null;

        // Root getstat method that actually returns the value in a desired index of the array
        // of values
        public int GetStats(StatEnum stat, CharacterClass name, int index)
        {
            // search for the right character class
            // ProgressionCharacterClass[] arr =
            // progressionClasses.Where(x => x.name == name).ToArray();

            // print("arr.length: " + arr.Length);
            // print("starting level: " + healthIndex);
            // print("name of the character class: " + arr[0].name.ToString());
            // print("size of health arr in that character class: " + arr[0].healths.Length);

            // if we didn't find the right character class
            // if (arr.Length == 0) return 0;

            // we found the right character class, now we search for the correct stat in the
            // collection of stats in that character class
            // ProgressionStat[] statArr = arr[0].stats.Where(x => x.stat == stat).ToArray();

            // if we didn't find the stats we're looking for
            // if (statArr.Length == 0) return 0;

            // checks for index out of bounds exceptions. Index can be less than OR
            // EQUAL to the levels array length bc we did a minus 1 check in the 
            // base stats class
            // return (index <= statArr[0].levels.Length) ? statArr[0].levels[index] : 0;

            BuildLookUpTable();

            int[] levels = lookUpTable[name][stat];

            // index will never be negative, so we won't make that check
            if (index > levels.Length) { print("index is out of bounds"); return 0; }

            return levels[index - 1];
        }

        private void BuildLookUpTable()
        {
            // don't rebuild if we build the table already
            if (lookUpTable != null) { return; }

            // instantiate outer dictionary
            lookUpTable = new Dictionary<CharacterClass, Dictionary<StatEnum, int[]>>();
            
            // populate outer dictionary
            foreach (ProgressionCharacterClass characterClass in progressionClasses)
            {
                // instantiate inner
                Dictionary<StatEnum, int[]> statLookUpTable = new Dictionary<StatEnum, int[]>();

                // populate inner dictionary
                foreach (ProgressionStat progressionStat in characterClass.stats)
                {
                    statLookUpTable[progressionStat.stat] = progressionStat.levels;
                }

                lookUpTable[characterClass.name] = statLookUpTable;
            }
        }

        public int GetNumPossibleLevels(CharacterClass name, StatEnum stat)
        {
            BuildLookUpTable();

            return lookUpTable[name][stat].Length;
        }

        [Serializable]
        struct ProgressionCharacterClass
        {
            public CharacterClass name;
            // public int[] healths, damage;
            public ProgressionStat[] stats;
        }

        [Serializable]
        struct ProgressionStat
        {
            public StatEnum stat;
            public int[] levels;
        }

        private void print(object text)
        {
            Debug.Log(text);
        }
    }
}
