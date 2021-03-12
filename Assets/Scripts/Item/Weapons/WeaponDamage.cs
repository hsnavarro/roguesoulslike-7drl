using UnityEngine;

public class WeaponDamage : MonoBehaviour {
    [SerializeField]
    private float lightDamage = 10f;

    [SerializeField]
    private float heavyDamage = 25f;

    [SerializeField]
    private Collider hitbox;

    enum AttackType {
        None,
        Light,
        Heavy
    }

    private AttackType attacking;

    public void StartLightAttack() {
        attacking = AttackType.Light;
    }

    public void StartHeavyAttack() {
        attacking = AttackType.Heavy;
    }

    public void EndAttack() {
        attacking = AttackType.None;
    }

    void OnTriggerEnter(Collider collider) {
        if (attacking == AttackType.None) return;

        Defense def = collider.gameObject.GetComponent<Defense>();
        if (def) {
            def.TakeDamage(attacking == AttackType.Light ? lightDamage : heavyDamage);
        }
    }
}
