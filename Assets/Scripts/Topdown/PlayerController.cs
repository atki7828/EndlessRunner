using UnityEngine;
using Topdown;

namespace Topdown {
    public class PlayerController : MonoBehaviour {
        [SerializeField]
        float speed = 5;
        Vector2 GroundBlockSize;
        Vector2 CamSize;
        Vector2 PlayerSize;
        Vector3 position;
        float yPos;
        Rigidbody2D rb;

        void Start() {
            rb = GetComponent<Rigidbody2D>();
            GroundBlockSize = Map.GetGroundBlockSize();
            CamSize = Map.GetCamSize();
            Debug.Log(CamSize);
            PlayerSize = GetComponent<SpriteRenderer>().sprite.bounds.size;
            yPos = -CamSize.y/2+PlayerSize.y*2;
            rb.transform.position = new Vector3(0,yPos,0);
            position = rb.transform.position;
        }

        float t = 0.0f;
        void Update() {
            /*
            Vector3 pos = position;
            position = new Vector3(pos.x,yPos+Mathf.Sin(t)/4,pos.z);
            t+= 0.05f;
            */

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
            if(rb.transform.position != position)
                rb.transform.position = Vector3.MoveTowards(rb.transform.position,position,speed*Time.deltaTime);
        }
    }
}
