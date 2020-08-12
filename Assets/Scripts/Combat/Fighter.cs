using System;
using System.Collections;
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
        [SerializeField] Transform rightHand = null;
        [SerializeField] Transform leftHand = null;

        // player should not have any weapons on start unless you're debugging
        [SerializeField] Weapon defaultWeapon = null;
        // [SerializeField] string defaultWeaponName = "Unarmed";

        // this will be used throughout the script
        LazyValue<Weapon> currentWeapon = null;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            currentWeapon = new LazyValue<Weapon>(SetUpDefaultWeapon);
        }

        void Start()
        {
            // makes sure currentWeapon.value has a valid value
            currentWeapon.ForceInit();
        }

        public Health GetTarget()
        {
            return target == null ? null : target;
        }

        public Weapon SetUpDefaultWeapon()
        {
            AttachWeapon(defaultWeapon);
            return defaultWeapon;
        }

        public void AttachWeapon(Weapon myWeapon)
        {
            // we always pass in the transforms of both hands
            // so that the Weapon script can use either or at its disposal
            myWeapon.Spawn(leftHand, rightHand, anim);
        }

        public void EquipWeapon(Weapon myWeapon)
        {
            currentWeapon.value = myWeapon;
            AttachWeapon(myWeapon);
        }


        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target != null && currentWeapon != null)
            {
                bool isInRange = Vector3
                .Distance(target.transform.position, transform.position) < currentWeapon.value.weaponRange;

                if (!isInRange)
                {
                    GetComponent<Mover>().MoveTo(target.transform.position, 1f);
                }
                else
                {
                    // stop moving and start attacking
                    GetComponent<Mover>().Cancel();
                    AttackBehaviour();
                }
            }
            else
            {
                print("you either have no attack target or no weapon assigned");
            }
        }

        void Hit()
        {
            if (target == null || target.show_current_health <= 0) return;

            int doDamage = GetComponent<BaseStats>().GetStats(StatEnum.Damage);

            if (currentWeapon.value.HasProjectile())
            {
                print("launched projectile at target: " + target.gameObject.name);

                currentWeapon.value.LaunchProjectile(leftHand, rightHand, target, this.gameObject, doDamage);
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
                // Debug.Log("killing enemy!!");
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
            return combTarg.GetComponent<Health>() != null && !combTarg.GetComponent<Health>().isDead;
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
            if (currentWeapon != null)
            {
                return currentWeapon.value.name;
            }

            return "no_name";
        }

        public void RestoreState(object state)
        {
            // get the object and cast it to a string
            string restoredWeaponName = state as string;

            // create weapon object loaded from the Resources folder
            Weapon restoredWeapon = UnityEngine.Resources.Load<Weapon>(restoredWeaponName);

            // equip it to the character this script is attached to
            EquipWeapon(restoredWeapon);
        }

        public IEnumerable<int> GetAdditiveModifier(StatEnum stat)
        {
            // only the damage component should be modified,
            // so we have to make this check
            if (stat == StatEnum.Damage)
            {
                yield return currentWeapon.value.weaponDamage;
            } 
        }

        public IEnumerable<float> GetPercentageModifiers(StatEnum stat)
        {
            if (stat == StatEnum.Damage)
            {
                yield return currentWeapon.value.percentageBonus;
            }
        }
    }
}

