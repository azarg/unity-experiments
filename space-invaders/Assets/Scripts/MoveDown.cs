using UnityEngine;

public class MoveDown : MonoBehaviour {
    [SerializeField] float speed;
    [SerializeField] BoundsVariable bounds;

    private void Update() {
        if (transform.position.y > bounds.bottom) {
            // move down until reaching the bottom
            transform.Translate(speed * Time.deltaTime * Vector3.down);
        }
        else {
            // destroy once bottom is reached
            Destroy(gameObject);
        }
    }
}