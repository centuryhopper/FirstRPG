﻿using System.Reflection.Emit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace RPG.Combat
{
    public class WeaponPickUp : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;

        [Label("Pickup hiding time")]
        [SerializeField] float secs = 5;

        GameObject myChild;
        SphereCollider mySphereCollider;

        private void Start()
        {
            // all pickups are assumed to have these components and features
            // so they shouldn't be null
            myChild = transform.GetChild(0).gameObject;
            mySphereCollider = GetComponent<SphereCollider>();
        }

        private void OnTriggerEnter(Collider other) 
        {
            if (other.tag == "Player")
            {
                print("player has equipped weapon");
                other.gameObject.GetComponent<Fighter>().EquipWeapon(weapon);
                
                // despawn and respawn
                StartCoroutine(HideForSeconds(secs));
            }
        }

        IEnumerator HideForSeconds(float seconds)
        {
            // passing in false means hide, passing in true means show
            ShowOrHidePickUp(false);
            yield return new WaitForSeconds(seconds);
            ShowOrHidePickUp(true);
        }

        private void ShowOrHidePickUp(bool shouldShow)
        {
            // enable child(or children) and sphere collider

            // this foreach loop will cover more cases than
            // if we just disabled the first child
            // so that we can have multiple children for this
            // gameobject and this method will still work
            foreach (Transform child in this.transform)
            {
                child.gameObject.SetActive(shouldShow);
            }

            mySphereCollider.enabled = shouldShow;
        }
    }
}