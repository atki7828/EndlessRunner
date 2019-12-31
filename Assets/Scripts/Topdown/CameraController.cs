using UnityEngine;

namespace Topdown {
    public class CameraController : MonoBehaviour {
        GameObject Player;

        void Start() {
            Player = GameObject.FindWithTag("Player");
        }
        void Update() {
            if(Player.transform.position.x != transform.position.x) {
                float newX = Player.transform.position.x;
                Vector3 newPos = transform.position;
                newPos.x = newX;
                transform.position = Vector3.MoveTowards(transform.position,newPos,Time.deltaTime);
            }
        }
    }
}
