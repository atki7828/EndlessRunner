using UnityEngine;
using System.Collections.Generic;
using threeD;

namespace threeD
{
    public class Map : MonoBehaviour
    {
        GameObject Player;
        static GameObject StartCanvas;
        [SerializeField]
        GameObject GroundPlane;
        [SerializeField]
        float groundSpeed = 6;
        [SerializeField]
        GameObject Obstacle;
        static LinkedList<GameObject> GroundPlanes;
        public static List<GameObject> Obstacles;
        int NumBlocks;
        static Vector2 CamSize;
        public static Vector2 GetCamSize() { return CamSize; }
        static Vector3 GroundPlaneSize;
        public static Vector3 GetGroundPlaneSize() { return GroundPlaneSize; }
        static bool running = false;
        float TimeOfLastObstacle = 0.0f;
        int ObstaclesPerSecond = 1;

        void Awake()
        {
            StartCanvas = GameObject.Find("StartCanvas");
            Player = GameObject.FindWithTag("Player");
            CamSize = new Vector2(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize) * 2;
            GroundPlaneSize = GroundPlane.GetComponent<MeshRenderer>().bounds.size;
            NumBlocks = 4;
            GroundPlanes = new LinkedList<GameObject>();
            Obstacles = new List<GameObject>();
        }

        void Start()
        {
            for (int i = 0; i < NumBlocks + 1; i++)
            {
                GameObject b = GameObject.Instantiate(GroundPlane, new Vector3(0, 0, i * GroundPlaneSize.z), Quaternion.identity);
                GroundPlanes.AddLast(b);
            }
        }

        void Update()
        {
            if (running) Time.timeScale = 1;
            else Time.timeScale = 0;
        }

        void FixedUpdate()
        {
            if (Time.time - TimeOfLastObstacle >= 1f / ObstaclesPerSecond)
            {
                GameObject o = GameObject.Instantiate(Obstacle);
                Obstacles.Add(o);
                TimeOfLastObstacle = Time.time;
            }

            foreach(GameObject o in Obstacles) {
                o.transform.position += -Vector3.forward * groundSpeed * Time.deltaTime;
            }

            GameObject first = GroundPlanes.First.Value;
            GameObject last = GroundPlanes.Last.Value;
            foreach (GameObject b in GroundPlanes)
            {
                b.transform.position += -Vector3.forward * groundSpeed * Time.deltaTime;
            }
            if(first.transform.position.z < -10)
            {
                GroundPlanes.RemoveFirst();
                Destroy(first);
                GameObject b = GameObject.Instantiate(GroundPlane, last.transform.position + new Vector3(0, 0,GroundPlaneSize.z), Quaternion.identity);
                GroundPlanes.AddLast(b);
            }
            /*
            if (first.transform.position.z + GroundPlaneSize.z / 2 < -CamSize.y / 2)
            {
                GroundPlanes.RemoveFirst();
                Destroy(first);
                GameObject b = GameObject.Instantiate(GroundPlane, last.transform.position + new Vector3(0, GroundPlaneSize.y, 0), Quaternion.identity);
                GroundPlanes.AddLast(b);
            }
            */
        }
        public static void StartGame()
        {
            running = true;
            StartCanvas.SetActive(false);
        }
        public static void StopGame()
        {
            running = false;
            StartCanvas.SetActive(true);
        }

        public static GameObject LastPlane()
        {
            return GroundPlanes.Last.Value;

        }
    }


}
