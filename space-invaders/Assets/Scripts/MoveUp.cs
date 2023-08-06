using UnityEngine;

public class MoveUp : MonoBehaviour {
    [SerializeField] float speed;
    [SerializeField] BoundsVariable bounds;

    private void Update() {
        if (transform.position.y < bounds.top) {
            // move up until reaching the top
            transform.Translate(speed * Time.deltaTime * Vector3.up);
        }
        else {
            // destroy once top is reached
            Destroy(gameObject);
        }
    }
}