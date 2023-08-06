using UnityEngine;

public class MoveUp : MonoBehaviour {
    [SerializeField] float speed;
    [SerializeField] BoundsVariable bounds;

    private void Update() {
        if (transform.position.y < bounds.top) {
            transform.Translate(speed * Time.deltaTime * Vector3.up);
        }
        else {
            Destroy(gameObject);
        }
    }
}