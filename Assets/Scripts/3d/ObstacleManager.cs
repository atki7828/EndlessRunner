using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace threeD
{
    public class ObstacleManager : MonoBehaviour
    {
        [SerializeField]
        GameObject ObstacleObject;
        Vector2 ObstacleSize;
        Vector2 CamSize;
        List<GameObject> Obstacles;

        // Start is called before the first frame update
        void Start()
        {
            Obstacles = new List<GameObject>();
            InvokeRepeating("SpawnObstacles", 1f, 1f);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void SpawnObstacles()
        {
            Vector3 pos = new Vector3();
            GameObject o = GameObject.Instantiate(ObstacleObject, new Vector3(0, 0, 0), Quaternion.identity);
            Obstacles.Add(o);
        }
    }
}
