using System.Collections;
using UnityEngine;

namespace TrigStuff
{
    public static class TrigTools
    {
        private static GameObject cubePrefab = null, spherePrefab = null;

        public static Color GetRandomColor()
        {
            float r = Random.Range(0f, 1f);
            float g = Random.Range(0f, 1f);
            float b = Random.Range(0f, 1f);

            //make grey/sludge colors less likely
            for (int i = 0; i < Random.Range(1, 3); i++)
            {
                if (Random.Range(0, 10) > 1)
                {
                    int a = Random.Range(0, 3);
                    if (a == 0)
                        r = 0;
                    if (a == 1)
                        g = 0;
                    if (a == 2)
                        b = 0;
                }
            }

            return new Color(r, g, b);
        }

        public static GameObject MakeCube(Vector3 position, Color color, float size)
        {
            return MakeCube(position.x, position.y, position.z, color, size);
        }

        public static GameObject MakeCube(float x, float y, float z, Color color, float size)
        {
            if (cubePrefab == null)
            {
                cubePrefab = Resources.Load<GameObject>("Geometry/Cube");
            }

            Vector3 position = new Vector3(x, y, z);
            GameObject newCube = Object.Instantiate(cubePrefab, position, Quaternion.identity) as GameObject;
            newCube.GetComponent<Renderer>().material.color = color;
            newCube.transform.localScale = new Vector3(size, size, size);
            return newCube;
        }

        public static GameObject MakeSphere(float x, float y, float z, Color color, float size)
        {
            if (spherePrefab == null)
            {
                spherePrefab = Resources.Load<GameObject>("Geometry/Sphere");
            }

            // define a position for the spawned shape
            Vector3 position = new Vector3(x, y, z);

            // spawn the gameobject
            GameObject newSphere =
                Object.Instantiate(spherePrefab, position, Quaternion.identity) as GameObject;

            // set the color
            newSphere.GetComponent<Renderer>().material.color = color;

            // set the size
            newSphere.transform.localScale = new Vector3(size, size, size);
            return newSphere;
        }
    }
}