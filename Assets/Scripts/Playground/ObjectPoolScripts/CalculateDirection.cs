using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateDirection : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        // Debug.Log("magnitude of the direction vector: " + direction.magnitude);
        // Debug.DrawRay(transform.position, direction, Color.blue);

        transform.Translate(direction * speed * Time.deltaTime);

    }
}
