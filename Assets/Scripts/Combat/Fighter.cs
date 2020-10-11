using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Movement;
using RPG.Saving;
using RPG.Attributes;
using RPG.Stats;
using GameDevTV.Utils;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        float timeSinceLastAttack = 0f;
        Health target;
        Animator anim;
        Mover mover;
        [SerializeField] Transform rightHand = null;
        [SerializeField] Transform leftHand = null;

        // player should not have any weapons on start unless you're debugging
        [SerializeField] WeaponConfig defaultWeapon = null;
        // [SerializeField] string defaultWeaponName = "Unarmed";

        // this will be used throughout the script
        WeaponConfig currentWeaponConfig = null;

        // we need a reference to the weapon in order to link a sound from the fighter
        // to the weapon
        LazyValue<Weapon> currentWeapon;

        private void Awake()
        {
            currentWeaponConfig = defaultWeapon;
            currentWeapon = new LazyValue<Weapon>(SetUpDefaultWeapon);
            anim = GetComponent<Animator>();
            mover = GetComponent<Mover>();
            // currentWeaponConfig = new LazyValue<WeaponConfig>(SetUpDefaultWeapon);
        }

        void Start()
        {
            // makes sure currentWeapon.value has a valid value
            // AttachWeapon(currentWeaponConfig);
            currentWeapon.ForceInit();
        }

        public Health GetTarget()
        {
            return target == null ? null : target;
        }

        public Weapon SetUpDefaultWeapon()
        {
            return AttachWeapon(defaultWeapon);
        }

        public Weapon AttachWeapon(WeaponConfig myWeapon)
        {
            // we always pass in the transforms of both hands
            // so that the Weapon script can use either or at its disposal
            return myWeapon.SpawnWeapon(leftHand, rightHand, anim);
        }

        public void EquipWeapon(WeaponConfig myWeapon)
        {
            currentWeaponConfig = myWeapon;
            currentWeapon.value = AttachWeapon(myWeapon);
        }

        // animations happen in update,
        // the actual animations themselves
        // invoke a method called hit() or shoot() for
        // projectiles and give damage to the victim
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target != null && currentWeaponConfig != null)
            {
                bool isInRange = GetIsInRange(target.transform);

                if (!isInRange)
                {
                    mover.MoveTo(target.transform.position, 1f);
                }
                else
                {
                    // stop moving and start attacking
                    mover.Cancel();
                    AttackBehaviour();
                }
            }
            else
            {
                // print("you either have no attack target or no weapon assigned");
            }
        }

        private bool GetIsInRange(Transform targetTransform)
        {
            return Vector3
            .Distance(targetTransform.position, transform.position) < currentWeaponConfig.weaponRange;
        }

        void Hit()
        {
            if (target == null || target.show_current_health <= 0) return;

            // calculate damage
            int doDamage = GetComponent<BaseStats>().GetStats(StatEnum.Damage);

            if (currentWeapon.value != null)
            {
                currentWeapon.value.OnHit();
            }

            if (currentWeaponConfig.HasProjectile())
            {
                print("launched projectile at target: " + target.gameObject.name);

                currentWeaponConfig.LaunchProjectile(leftHand, rightHand, target, this.gameObject, doDamage);
            }
            else
            {
                print("hitting " + target.gameObject.name);
                target.TakeDamage(doDamage, this.gameObject);
            }
        }

        void Shoot()
        {
            print("calling shoot");
            Hit();
        }

        // Animation event
        private void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks && target.show_current_health > 0)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            anim.ResetTrigger("stopAttack");
            anim.SetTrigger("attack");
        }

        // player/enemy is in attack mode or vice versa (called in the AI and player controller scripts)
        public void Attack(GameObject combatTarget)
        {
            // tells us what action is being started
            GetComponent<ActionScheduler>().StartAction(this);

            // get the enemy location and store it into our fighter class member
            target = combatTarget.GetComponent<Health>();
        }

        public bool CanAttack(GameObject combTarg)
        {
            if (combTarg == null) { return false; }
            if (!mover.CanMoveTo(combTarg.transform.position)
                && !GetIsInRange(combTarg.transform))
            {
                return false;
            }

            return combTarg.GetComponent<Health>() != null
                    && !combTarg.GetComponent<Health>().isDead;
        }

        private void StopAttack()
        {
            anim.ResetTrigger("attack");
            anim.SetTrigger("stopAttack");
        }

        // interface implementation
        public void Cancel()
        {
            Debug.Log("Canceling attack");
            StopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        public object CaptureState()
        {
            // get the most recent weapon name and save it for future use
            if (currentWeaponConfig != null)
            {
                return currentWeaponConfig.name;
            }

            return "no_name";
        }

        public void RestoreState(object state)
        {
            // get the object and cast it to a string
            string restoredWeaponName = state as string;

            // create weapon object loaded from the Resources folder
            WeaponConfig restoredWeapon = UnityEngine.Resources.Load<WeaponConfig>(restoredWeaponName);

            // equip it to the character this script is attached to
            EquipWeapon(restoredWeapon);
        }

        public IEnumerable<int> GetAdditiveModifier(StatEnum stat)
        {
            // only the damage component should be modified,
            // so we have to make this check
            if (stat == StatEnum.Damage)
            {
                yield return currentWeaponConfig.weaponDamage;
            }
        }

        public IEnumerable<float> GetPercentageModifiers(StatEnum stat)
        {
            // only the damage component should be modified,
            // so we have to make this check
            if (stat == StatEnum.Damage)
            {
                yield return currentWeaponConfig.percentageBonus;
            }
        }
    }
}

