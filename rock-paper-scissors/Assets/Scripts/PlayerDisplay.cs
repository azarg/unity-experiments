using UnityEngine;

public class PlayerDisplay : MonoBehaviour {

    [SerializeField] private HandVariable selectedHand;
    private SpriteRenderer spriteR;

    void Start() {
        spriteR = GetComponent<SpriteRenderer>();
        selectedHand.OnValueChanged += SelectedHand_OnValueChanged;
    }

    private void SelectedHand_OnValueChanged() {
        if (selectedHand.HasSprite())
            spriteR.sprite = selectedHand.GetValue().sprite;
    }
}
