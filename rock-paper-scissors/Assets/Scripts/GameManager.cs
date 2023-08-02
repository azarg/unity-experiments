using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private Hand[] hands = null;
    [SerializeField] private HandVariable playerHand;
    [SerializeField] private HandVariable enemyHand;
    [SerializeField] private StringVariable message;

    public void Play() {
        if (playerHand.GetValue() == null) {
            message.SetValue("Select your hand first");
            return;
        }
        int i = Random.Range(0, hands.Length);
        enemyHand.SetValue(hands[i]);

        if (playerHand.GetValue().defeats == hands[i]) {
            message.SetValue("Player wins");
        }
        else if (hands[i].defeats == playerHand.GetValue()) {
            message.SetValue("AI wins");
        }
        else {
            message.SetValue("Draw");
        }

    }
}
