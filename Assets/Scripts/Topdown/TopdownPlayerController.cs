using UnityEngine;

public class TopdownPlayerController : MonoBehaviour {
    [SerializeField]
    float speed = 10;
    Vector2 GroundBlockSize;
    Vector2 CamSize;
    Vector2 PlayerSize;
    Vector3 position;
    int NumRows;

    void Start() {
        GroundBlockSize = TopdownMap.GetGroundSize();
        CamSize = TopdownMap.GetCamSize();
        PlayerSize = GetComponent<SpriteRenderer>().sprite.bounds.size;
        NumRows = (int)(GroundBlockSize.x/PlayerSize.x);
        transform.position = new Vector3(0,-CamSize.y/2+PlayerSize.y,0);
        position = transform.position;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.A)) { // left
            Vector3 newPosition = position - new Vector3(PlayerSize.x,0,0);
            if(newPosition.x > -GroundBlockSize.x/2)
                position = newPosition;
        }
        if(Input.GetKeyDown(KeyCode.D)) { // right
            Vector3 newPosition = position + new Vector3(PlayerSize.x,0,0);
            if(newPosition.x < GroundBlockSize.x/2)
            position = newPosition;
        }
        if(transform.position != position)
            transform.position = Vector3.MoveTowards(transform.position,position,speed*Time.deltaTime);
    }
}
