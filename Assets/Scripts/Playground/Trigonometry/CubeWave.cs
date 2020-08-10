using System.Collections;
using UnityEngine;

namespace TrigStuff
{
    public class CubeWave : MonoBehaviour
    {
        GameObject[,] cubes;
        float[,] distances;
        public float amplitude = 1;
        public float frequency = 5;
        public int width = 25;
        public Vector2 center;
        private Vector3 realCenter;

        void Start()
        {
            realCenter = GetRealCenter();
            MakeCubes();
        }

        Vector3 GetRealCenter()
        {
            return transform.position + new Vector3(center.x, 0, center.y);
        }

        void MakeCubes()
        {
            cubes = new GameObject[width, width];
            distances = new float[width, width];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Vector3 startPosition = GetCubeStartPosition(i, j);
                    cubes[i, j] = TrigTools.MakeCube(startPosition, Color.cyan, 1);
                    distances[i, j] = Vector3.Distance(startPosition, realCenter);

                }
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(GetRealCenter(), 2);
        }

        private Vector3 GetCubeStartPosition(int i, int j)
        {
            float xOffset = i - width / 2;
            float zOffset = j - width / 2;

            return new Vector3(transform.position.x + xOffset,
                transform.position.y,
                transform.position.z + zOffset);
        }

        void Update()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    GameObject cube = cubes[i, j];
                    if (cube == null) { print("cube is null"); return; }
                    Vector3 startPosition = GetCubeStartPosition(i, j);
                    float phaseShift = distances[i, j];

                    float y =
                    amplitude * Mathf.Sin(Time.timeSinceLevelLoad * frequency + phaseShift);
                    Vector3 offset = new Vector3(0, y, 0);
                    cube.transform.position = GetCubeStartPosition(i, j) + offset;
                }
            }
        }
    }
}