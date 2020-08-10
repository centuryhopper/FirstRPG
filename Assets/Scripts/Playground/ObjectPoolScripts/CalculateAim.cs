using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateAim : MonoBehaviour
{
    // [SerializeField] Transform enemy;

    // Update is called once per frame
    void Update()
    {
        // calculate direction from you to the enemy
        // Vector2 direction = enemy.position - transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        Vector2 direction =  mousePos - transform.position;

        // Use a Debug.DrawRay to visualize the aim
        Debug.DrawRay(transform.position, direction, Color.green);

        // calculate angle from the calulated direction and convert from radians to degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        // use transform.eulerAngles = axis you want to rotate about * the calculated angle
        transform.eulerAngles = Vector3.forward * angle;
    }
}
