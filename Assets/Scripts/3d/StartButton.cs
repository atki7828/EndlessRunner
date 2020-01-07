using UnityEngine;
using UnityEngine.UI;
using threeD;

namespace threeD
{
    public class StartButton : MonoBehaviour
    {
        Button button;
        void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(delegate { Map.StartGame(); });
        }
    }
}
