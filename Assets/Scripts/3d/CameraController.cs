using UnityEngine;

namespace threeD
{
    public class CameraController : MonoBehaviour
    {
        GameObject Player;
        float speed = 2;
        Vector3 lookTarget;

        // Start is called before the first frame update
        void Start()
        {
            Player = GameObject.FindWithTag("Player");
            lookTarget = Player.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(lookTarget);
            lookTarget = Vector3.MoveTowards(lookTarget, Player.transform.position, speed*Time.deltaTime);
            //Vector3 dir = (Player.transform.position - transform.position).normalized;
            //transform.forward = Vector3.MoveTowards(transform.forward, dir, speed*Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position,new Vector3(Player.transform.position.x, transform.position.y, transform.position.z),speed*Time.deltaTime);
        }
    }
}
