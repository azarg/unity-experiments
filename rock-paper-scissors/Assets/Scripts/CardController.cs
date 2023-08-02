using UnityEngine;

public class CardController : MonoBehaviour {

    [SerializeField] Hand hand;
    [SerializeField] HandVariable playerHand;

    void OnMouseDown() {
        playerHand.SetValue(hand);
    }
}
