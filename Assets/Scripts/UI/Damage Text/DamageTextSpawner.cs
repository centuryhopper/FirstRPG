using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.UI.DamageHUD
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] DamageHUD damageHUDPrefab = null;

        // for debugging purposes
        // private void Update()
        // {
        //     Spawn(90);
        // }

        public void Spawn(float damageToDisplay)
        {
            var spawnedHUD = Instantiate<DamageHUD>(damageHUDPrefab, transform);

            spawnedHUD.SetDamageAmount(damageToDisplay);

            // we don't need to call destroy here anymore bc
            // we destroy it as an animation event at the end
            // of the fading animation
            //// Destroy(spawnedHUD, 0.5f);
        }
    }
}

