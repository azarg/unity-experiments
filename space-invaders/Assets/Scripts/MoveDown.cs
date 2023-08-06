using UnityEngine;

public class MoveDown : MonoBehaviour {
    [SerializeField] float speed;
    [SerializeField] BoundsVariable bounds;

    private void Update() {
        if (transform.position.y > bounds.bottom) {
            transform.Translate(speed * Time.deltaTime * Vector3.down);
        }
        else {
            Destroy(gameObject);
        }
    }
}