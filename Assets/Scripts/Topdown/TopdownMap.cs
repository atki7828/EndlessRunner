using UnityEngine;
using System.Collections.Generic;

public class TopdownMap : MonoBehaviour {
    GameObject Player;
    Vector2 PlayerSize;
    const int PPU = 16;
    [SerializeField]
        GameObject GroundBlock;
    [SerializeField]
        GameObject Obstacle;
    static Vector2 GroundBlockSize;
    public static Vector2 GetGroundSize() { return GroundBlockSize; }
    Vector2 ObstacleSize;
    [SerializeField]
        float speed = 5f;
    LinkedList<GameObject> GroundBlocks;
    List<GameObject> Obstacles;
    static Vector2 CamSize;
    public static Vector2 GetCamSize() { return CamSize; }
    int NumBlocks;

    void Awake() {
        Player = GameObject.FindWithTag("Player");
        CamSize = new Vector2(Camera.main.orthographicSize*Camera.main.aspect,Camera.main.orthographicSize)*2;
        GroundBlockSize = GroundBlock.GetComponent<SpriteRenderer>().sprite.bounds.size;
        ObstacleSize = Obstacle.GetComponent<SpriteRenderer>().sprite.bounds.size;
        //PlayerSize = Player.GetComponent<SpriteRenderer>().sprite.bounds.size;

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
        /*
        if(Random.value > 0.99f) {
            GameObject o = GameObject.Instantiate(Obstacle, new Vector3(CamSize.x/2,-GroundBlockSize.y*2+GroundBlockSize.y/2+ObstacleSize.y/2,0),Quaternion.identity);
            Obstacles.Add(o);
        }
        */
    }

    void FixedUpdate() {
        foreach(GameObject o in Obstacles) {
            o.transform.position += -Vector3.right * speed * Time.deltaTime;
        }

        GameObject first = GroundBlocks.First.Value;
        GameObject last = GroundBlocks.Last.Value;
        foreach(GameObject b in GroundBlocks) {
            b.transform.position += -Vector3.up * speed * Time.deltaTime;
        }
        if(first.transform.position.y + GroundBlockSize.y/2 < -CamSize.y/2) {
            GroundBlocks.RemoveFirst();
            Destroy(first);
            GameObject b = GameObject.Instantiate(GroundBlock,last.transform.position + new Vector3(0,GroundBlockSize.y,0),Quaternion.identity);
            GroundBlocks.AddLast(b);
        }
    }

}
