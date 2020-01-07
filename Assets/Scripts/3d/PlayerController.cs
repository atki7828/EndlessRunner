using UnityEngine;
using threeD;

namespace threeD 
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        float speed = 5;
        Vector3 position;
        Rigidbody rb;
        Vector3 PlayerSize;
        Vector3 GroundPlaneSize;
        bool isJumping = false;
        float edge;

        void Start()
        {
            PlayerSize = GetComponent<MeshRenderer>().bounds.size;
            GroundPlaneSize = Map.GetGroundPlaneSize();
            rb = GetComponent<Rigidbody>();
            transform.position = new Vector3(0, PlayerSize.y / 2, 0);
            position = rb.transform.position;
            edge = GroundPlaneSize.x / 2 - PlayerSize.x;
        }

        float t = 0.0f;
        void Update()
        {
            /*
            Vector3 pos = position;
            position = new Vector3(pos.x,yPos+Mathf.Sin(t)/4,pos.z);
            t+= 0.05f;
            */

            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                isJumping = true;
                rb.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
            }
            float x;
            if (transform.position == position && (x = Input.GetAxisRaw("Horizontal")) != 0)
            {
                position.x += PlayerSize.x * x;
                position.x = Mathf.Clamp(position.x, -edge,edge);
            }
            /*
            if (Input.GetKeyDown(KeyCode.A))
            { // left
                Vector3 newPosition = position - new Vector3(PlayerSize.x, 0, 0);
                if(position.x > -GroundPlaneSize.x/2 + PlayerSize.x)
                    position = newPosition;
            }
            if (Input.GetKeyDown(KeyCode.D))
            { // right
                Vector3 newPosition = position + new Vector3(PlayerSize.x, 0, 0);
                if(position.x < GroundPlaneSize.x/2 - PlayerSize.x)
                    position = newPosition;
            }
            */
            if (rb.transform.position != position)
                rb.transform.position = Vector3.MoveTowards(rb.transform.position, position, speed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
            {
                isJumping = false;
            }
        }
    }
}
