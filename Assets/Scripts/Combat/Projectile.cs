using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using System;
using RPG.Resources;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float shootingSpeed = 5f;
        [SerializeField] bool isHeatSeeking = false;
        [SerializeField] GameObject particleEffect = null;
        Health myTarget = null;
        int myDamage = 0;

        // this variable will store whoever launched the projectile
        GameObject instigator = null;

        private void Start()
        {
            if (myTarget != null)
            {
                transform.LookAt(GetAimLocation());
            }
        }

    
        // the program is structured so that this method
        // would be called after a target has been set
        private void OnTriggerEnter(Collider other)
        {
            if (particleEffect != null)
            {
                GameObject impact = Instantiate(particleEffect, GetAimLocation(),
                transform.rotation);
                Destroy(impact, 2f);
            }

            if (other.GetComponent<Health>() == myTarget && !myTarget.isDead)
            {
                print(myTarget.name + " has been hit");
                myTarget.TakeDamage(myDamage, instigator);
                Destroy(gameObject);
            }
            else
            {
                return;
            }
        }

        // this public method will be called from another script
        // sets the target, how much damage it is doing, and who is doing the damage
        public void SetTarget(Health target, int damage, GameObject instigator)
        {
            myTarget = target;
            myDamage = damage;
            this.instigator = instigator;
            print("target has been set to: " + myTarget.gameObject.name);
        }

        void Update()
        {
            if (myTarget != null)
            {
                if (isHeatSeeking && !myTarget.isDead)
                {
                    Start();
                }
                transform.Translate(Vector3.forward * shootingSpeed * Time.deltaTime);
            }
            else
            {
                print("no target");
            }
        }

        private void GuideTheShot()
        {
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * shootingSpeed * Time.deltaTime);
        }

        Vector3 GetAimLocation()
        {
            CapsuleCollider cc = myTarget.GetComponent<CapsuleCollider>();
            if (cc == null) {return myTarget.transform.position;}

            // we have vector3.up so that the x and z values aren't affected
            // in this calculation since we are only interested in returning the new y value
            return myTarget.transform.position + (Vector3.up * (cc.height / 2));
        }
    }
}
