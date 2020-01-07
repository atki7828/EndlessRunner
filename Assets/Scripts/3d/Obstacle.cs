using UnityEngine;
using UnityEngine.SceneManagement;

namespace threeD
{
    public class Obstacle : MonoBehaviour
    {
        float xPos, zPos;
        Vector2 CamSize;
        Vector3 ObstacleSize;
        Vector3 GroundBlockSize;

        void Awake()
        {
            CamSize = Map.GetCamSize();
            GroundBlockSize = Map.GetGroundPlaneSize();
            ObstacleSize = GetComponent<MeshRenderer>().bounds.size;
            zPos = Map.LastPlane().transform.position.z;
            xPos = Mathf.Floor(Random.Range(-GroundBlockSize.x / 2 + 1, GroundBlockSize.x / 2));
            transform.position = new Vector3(xPos, 0.5f, zPos);
        }

        void Update()
        {
            if (transform.position.z < -10)
            {
                Map.Obstacles.Remove(this.gameObject);
                Destroy(this.gameObject);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject);
            if (other.gameObject.tag == "Player")
            {
                Map.StopGame();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
