using UnityEngine;
using System.Collections.Generic;

namespace Platformer {
    public class Map : MonoBehaviour {
        GameObject Player;
        Vector2 PlayerSize;
        const int PPU = 16;
        [SerializeField]
        GameObject GroundBlock;
        [SerializeField]
        GameObject Obstacle;
        Vector2 GroundBlockSize;
        Vector2 ObstacleSize;
        [SerializeField]
        float speed = 2f;
        LinkedList<GameObject> GroundBlocks;
        List<GameObject> Obstacles;
        Vector2 CamSize;
        int NumBlocks;

        void Start() {
            Player = GameObject.FindWithTag("Player");
            CamSize = new Vector2(Camera.main.orthographicSize*Camera.main.aspect,Camera.main.orthographicSize)*2;
            GroundBlockSize = GroundBlock.GetComponent<SpriteRenderer>().sprite.bounds.size;
            ObstacleSize = Obstacle.GetComponent<SpriteRenderer>().sprite.bounds.size;
            PlayerSize = Player.GetComponent<SpriteRenderer>().sprite.bounds.size;
            Player.transform.position = new Vector3(0,-GroundBlockSize.y*2+GroundBlockSize.y/2+PlayerSize.y/2,0);

            NumBlocks = (int)Mathf.Ceil(CamSize.x/GroundBlockSize.x);
            GroundBlocks = new LinkedList<GameObject>();
            Obstacles = new List<GameObject>();

            for(int i = 0; i < NumBlocks+1; i++) {
                GameObject b = GameObject.Instantiate(GroundBlock,new Vector3(i*GroundBlockSize.x,-GroundBlockSize.y*2,0),Quaternion.identity);
                GroundBlocks.AddLast(b);
            }
        }

        void Update() {
            if(Random.value > 0.99f) {
                GameObject o = GameObject.Instantiate(Obstacle, new Vector3(CamSize.x/2,-GroundBlockSize.y*2+GroundBlockSize.y/2+ObstacleSize.y/2,0),Quaternion.identity);
                Obstacles.Add(o);
            }
        }

        void FixedUpdate() {
            foreach(GameObject o in Obstacles) {
                o.transform.position += -Vector3.right * speed * Time.deltaTime;
            }

            GameObject first = GroundBlocks.First.Value;
            GameObject last = GroundBlocks.Last.Value;
            foreach(GameObject b in GroundBlocks) {
                b.transform.position += -Vector3.right * speed * Time.deltaTime;
            }
            if(first.transform.position.x + GroundBlockSize.x/2 < -CamSize.x/2) {
                GroundBlocks.RemoveFirst();
                Destroy(first);
                GameObject b = GameObject.Instantiate(GroundBlock,last.transform.position + new Vector3(GroundBlockSize.x,0,0),Quaternion.identity);
                GroundBlocks.AddLast(b);
            }
        }

    }
}
