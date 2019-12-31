using UnityEngine;

namespace Platformer {
    public class PlayerController : MonoBehaviour {
        Rigidbody2D rb;
        bool isJumping,doubleJump;

        void Start() {
            rb = GetComponent<Rigidbody2D>();
            isJumping = false;
            doubleJump = false;
        }

        void Update() {
            if(Input.GetKeyDown(KeyCode.Space) && !doubleJump) {
                if(isJumping && rb.velocity.y <= 0) rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up*16,ForceMode2D.Impulse);
                if(isJumping)   doubleJump = true;
                isJumping = true;
            }
        }

        void OnCollisionEnter2D(Collision2D other) {
            if(other.gameObject.layer == LayerMask.NameToLayer("Floor")) {
                isJumping = false;
                doubleJump = false;
            }
        }
    }
}
