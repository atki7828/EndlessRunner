using UnityEngine;
using UnityEngine.UI;
using Topdown;

namespace Topdown
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
