using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.UI.DamageHUD
{
    // potentially display the damage
    public class DamageHUD : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI damageText = null;

        public void SetDamageAmount(float damageAmount)
        {
            damageText.text = damageAmount.ToString();
        }

        public void DestroyText()
        {
            Destroy(this.gameObject);
        }
    }
}
