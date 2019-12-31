using UnityEngine;
using System.Collections.Generic;
using Topdown;

namespace Topdown {
    public class Map : MonoBehaviour {
        GameObject Player;
        static GameObject StartCanvas;
        [SerializeField]
        GameObject GroundBlock;
        [SerializeField]
        GameObject Obstacle;
        [SerializeField]
        float groundSpeed = 6;
        LinkedList<GameObject> GroundBlocks;
        List<GameObject> Obstacles;
        int NumBlocks;
        static Vector2 CamSize;
        public static Vector2 GetCamSize() { return CamSize; }
        static Vector2 GroundBlockSize;
        public static Vector2 GetGroundBlockSize() { return GroundBlockSize; }
        int ObstaclesPerSecond = 2;
        float TimeOfLastObstacle = 0.0f;
        static bool running = false;

        void Awake() {
            StartCanvas = GameObject.Find("StartCanvas");
            Player = GameObject.FindWithTag("Player");
            CamSize = new Vector2(Camera.main.orthographicSize*Camera.main.aspect,Camera.main.orthographicSize)*2;
            GroundBlockSize = GroundBlock.GetComponent<SpriteRenderer>().sprite.bounds.size;
            NumBlocks = (int)Mathf.Ceil(CamSize.y/GroundBlockSize.y)*2;
            GroundBlocks = new LinkedList<GameObject>();
            Obstacles = new List<GameObject>();
        }

        void Start() {
            for(int i = 0; i < NumBlocks+1; i++) {
                GameObject b = GameObject.Instantiate(GroundBlock,new Vector3(0,i*GroundBlockSize.y,0),Quaternion.identity);
                GroundBlocks.AddLast(b);
            }
        }

        void Update() {
            if(running) Time.timeScale = 1;
            else        Time.timeScale = 0;
        }

        void FixedUpdate() {
            if(Time.time - TimeOfLastObstacle >= 1f/ObstaclesPerSecond) {
                GameObject o = GameObject.Instantiate(Obstacle);
                Obstacles.Add(o);
                TimeOfLastObstacle = Time.time;
            }

            foreach(GameObject o in Obstacles) {
                o.transform.position += -Vector3.up * groundSpeed * Time.deltaTime;
            }

            GameObject first = GroundBlocks.First.Value;
            GameObject last = GroundBlocks.Last.Value;
            foreach(GameObject b in GroundBlocks) {
                b.transform.position += -Vector3.up * groundSpeed * Time.deltaTime;
            }
            if(first.transform.position.y + GroundBlockSize.y/2 < -CamSize.y/2) {
                GroundBlocks.RemoveFirst();
                Destroy(first);
                GameObject b = GameObject.Instantiate(GroundBlock,last.transform.position + new Vector3(0,GroundBlockSize.y,0),Quaternion.identity);
                GroundBlocks.AddLast(b);
            }
        }
        public static void StartGame() {
            running = true;
            StartCanvas.SetActive(false);
        }
        public static void StopGame() { 
            running = false;
            StartCanvas.SetActive(true);
        }
    }

}
