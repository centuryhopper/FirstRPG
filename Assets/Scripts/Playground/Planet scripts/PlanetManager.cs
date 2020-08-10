using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AstroInfo;

public class PlanetManager : MonoBehaviour
{
    public Planet[] planets = new Planet[8];

    // Start is called before the first frame update
    void Start()
    {
        planets[0].name = "Mercury"; planets[0].radius = 1f; planets[0].distanceFromTheSun = 1f; planets[0].avgTemp = 1f;
        planets[1].name = "Venus"; planets[1].radius = 1f; planets[1].distanceFromTheSun = 1f; planets[1].avgTemp = 1f;
        planets[2].name = "Earth"; planets[2].radius = 1f; planets[2].distanceFromTheSun = 1f; planets[2].avgTemp = 1f;
        planets[3].name = "Mars"; planets[3].radius = 1f; planets[3].distanceFromTheSun = 1f; planets[3].avgTemp = 1f;
        planets[4].name = "Jupiter"; planets[4].radius = 1f; planets[4].distanceFromTheSun = 1f; planets[4].avgTemp = 1f;
        planets[5].name = "Saturn"; planets[5].radius = 1f; planets[5].distanceFromTheSun = 1f; planets[5].avgTemp = 1f;
        planets[6].name = "Uranus"; planets[6].radius = 1f; planets[6].distanceFromTheSun = 1f; planets[6].avgTemp = 1f;
        planets[7].name = "Neptune"; planets[7].radius = 1f; planets[7].distanceFromTheSun = 1f; planets[7].avgTemp = 1f;


        foreach (Planet planet in planets)
        {
            // Debug.Log(planet.name + " " + planet.radius + " " + planet.distanceFromTheSun + " " + planet.avgTemp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
