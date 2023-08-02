using UnityEngine;

public class EnemyDisplay : MonoBehaviour {

    [SerializeField] HandVariable enemyHand;
    private SpriteRenderer spriteRenderer;
    
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyHand.OnValueChanged += EnemyHand_OnValueChanged;
    }

    private void EnemyHand_OnValueChanged() {
        spriteRenderer.sprite = enemyHand.GetValue().sprite;
    }
}
