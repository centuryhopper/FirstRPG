using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health healthComponent = null;
        [SerializeField] RectTransform foregroundRectTransform = null;
        [SerializeField] Canvas canvas = null;

        void Start()
        {
            // subscribe (point the action to the method)
            Health.onHealthChange += HandleHealth;
        }

        // get health component percentage decimal value, then assign
        // rect transform scale's x-value to that
        public void HandleHealth()
        {
            // hides the health bar once the enemy dies
            // and also hides it when it is full
            if ( Mathf.Approximately(healthComponent.GetHealthDecimalValue(), 0)
                || Mathf.Approximately(healthComponent.GetHealthDecimalValue(), 1) )
            {
                canvas.enabled = false;
                return;
            }

            canvas.enabled = true;

            foregroundRectTransform.localScale =
                new Vector3(healthComponent.GetHealthDecimalValue(), 1, 1);
        }
    }
}
