using UnityEngine;

namespace Scripts.PlayerMovement
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 10.0f;
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        void Update()
        {
            float v = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
            float h = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
            transform.Translate(h, 0, v);
            if (Input.GetKeyDown("escape"))
                Cursor.lockState = CursorLockMode.None;
        }
    }
}