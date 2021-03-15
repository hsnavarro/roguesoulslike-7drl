using UnityEngine;

public class WeaponDamage : MonoBehaviour {
    [SerializeField]
    private float lightDamage = 10f;

    [SerializeField]
    private float heavyDamage = 25f;

    private float damageMultiplier = 1f;

    [SerializeField]
    private Collider hitbox;

    private enum AttackType {
        None,
        Light,
        Heavy
    }

    private AttackType attacking;

    public void Start() {
        hitbox.enabled = false;
    }

    public void StartLightAttack(float multiplier = 1f) {
        attacking = AttackType.Light;
        hitbox.enabled = true;
        damageMultiplier = multiplier;
    }

    public void StartHeavyAttack(float multiplier = 1f) {
        attacking = AttackType.Heavy;
        hitbox.enabled = true;
        damageMultiplier = multiplier;
    }

    public void EndAttack() {
        attacking = AttackType.None;
        hitbox.enabled = false;
    }

    private void OnTriggerEnter(Collider collider) {
        if (attacking == AttackType.None) return;

        Resilience resilience = null;
        if (collider.gameObject.layer == (int)Layers.SATYR) {
            resilience = collider.gameObject.GetComponent<Resilience>();
        }

        if (collider.gameObject.layer == (int)Layers.CYCLOPS) {
            resilience = collider.transform.parent.parent.parent.gameObject.GetComponent<Resilience>();
        } 

        if (collider.gameObject.layer == (int)Layers.REAPER) {
            resilience = collider.gameObject.GetComponent<Resilience>();
        }  

        if (collider.gameObject.layer == (int)Layers.WITCH) {
            resilience = collider.gameObject.GetComponent<Resilience>();
        }  

        if (resilience) {
            float damage = ((attacking == AttackType.Light) ? lightDamage : heavyDamage);
            damage *= damageMultiplier;
            resilience.TakeDamage(damage);
        }
    }
}
