using RPG.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat
{
                        // menu name is important for selecting when right clicking
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] Weapon equippedPrefab = null;
        [SerializeField] AnimatorOverrideController weaponOverrider = null;
        [SerializeField] float _weaponRange = 2f;
        public float weaponRange
        {
            get {return _weaponRange;}
        }
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;

        public bool HasProjectile()
        {
            return projectile != null;
        }
        
        // additive modifier amount from weapon damage
        [SerializeField] int _weaponDamage = 5;
        public int weaponDamage
        {
            get {return _weaponDamage;}
        }

        [SerializeField] float _percentageBonus = 0;
        public float percentageBonus
        {
            get {return _percentageBonus;}
        }

        // reference for the picked up weapon
        const string pickedUpWeaponName = "Weapon";

        // the instigator is who launched the projectile and this information gets passed into the projectile script's
        // SetTarget() method
        public void LaunchProjectile(Transform leftHand, Transform rightHand, Health target, GameObject instigator, int calculatedDamage)
        {
            Transform properHand = GetHandTransform(leftHand, rightHand);

            Instantiate(projectile, properHand.position, Quaternion.identity).SetTarget(target, calculatedDamage, instigator);
        }

        public Weapon SpawnWeapon(Transform leftHand, Transform rightHand, Animator animator)
        {
            // destroy the old weapon in hand (if any exist) before picking up a new one
            // 'unarmed' counts as a weapon too
            DestroyOldWeapon(leftHand, rightHand);

            Weapon tmpWeapon = null;

            if (equippedPrefab != null)
            {
                Transform handTransform = GetHandTransform(leftHand, rightHand);

                // weapon spawns in hand
                tmpWeapon = Instantiate(equippedPrefab, handTransform);

                tmpWeapon.gameObject.name = pickedUpWeaponName;
            }

            // will either be null or of type AnimatorOverrideController
            /// <reminder>
            // this is a problem and will need to be fixed later
            dynamic overrideController = null;

            if (animator != null)
            {
                overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            }

            // if we have an animation that overrides the default punch animation
            if (weaponOverrider != null && animator != null)
            {
                // override the default punch animator to the proper weapon animation
                animator.runtimeAnimatorController = weaponOverrider;
            }
            else if (overrideController != null)
            {
                // we come in here if the characters animator has an override animator from another weapon
                // attached to it. In doing so, we fix the bug of setting the current animation
                // to be the current override controller's default parent animator controller,
                // which in this case, for all override controllers, will be the punching animation
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }

            return tmpWeapon;
        }

        private void DestroyOldWeapon(Transform leftHand, Transform rightHand)
        {
            // check left hand then right hand for a weapon
            Transform oldWeapon = leftHand.Find(pickedUpWeaponName);

            // if the left hand is empty, then we check the right hand
            if (oldWeapon == null)
            {
                oldWeapon = rightHand.Find(pickedUpWeaponName);
            }

            // if neither hand has a weapon, then we don't destroy anything
            if (oldWeapon == null) {return;}

            oldWeapon.name = "DestroyingThisWeapon";

            Destroy(oldWeapon.gameObject);
        }

        public Transform GetHandTransform(Transform leftHand, Transform rightHand)
        {
            return isRightHanded ? rightHand : leftHand;
        }
    }
}


