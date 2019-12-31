using UnityEngine;
using UnityEngine.SceneManagement;

namespace Topdown {
    public class Obstacle : MonoBehaviour {
        float xPos,yPos;
        Vector2 CamSize;
        Vector2 ObstacleSize;
        Vector2 GroundBlockSize;

        void Start() {
            CamSize = Map.GetCamSize();
            GroundBlockSize = Map.GetGroundBlockSize();
            ObstacleSize = GetComponent<SpriteRenderer>().sprite.bounds.size;
            yPos = CamSize.y/2+ObstacleSize.y;
            xPos = Mathf.Floor(Random.Range(-GroundBlockSize.x/2+1,GroundBlockSize.x/2));
            transform.position = new Vector3(xPos,yPos,0);
        }

        void Update() {
        }
        void OnTriggerEnter2D(Collider2D other) {
            Debug.Log(other.gameObject);
            if(other.gameObject.tag == "Player") {
                Map.StopGame();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
