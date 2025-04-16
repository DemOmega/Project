using UnityEngine;

namespace Scenes.Script
{
    public class CursorManager : MonoBehaviour
    {
        void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}