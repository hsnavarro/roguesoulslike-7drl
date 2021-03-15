using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
    [SerializeField]
    private GameObject initialWeapon;

    [SerializeField]
    private Transform handBone;

    [SerializeField]
    private Vector3 weaponPosition;

    private PlayerStats playerStats;

    private GameObject currentWeapon;

    private void Start() {
        Equip(initialWeapon);
        playerStats = GetComponent<PlayerStats>();
    }

    public void Equip(GameObject weapon) {
        if (currentWeapon) {
            Destroy(currentWeapon);
        }

        currentWeapon = GameObject.Instantiate(
            initialWeapon,
            new Vector3(),
            Quaternion.identity
        );

        currentWeapon.transform.SetParent(handBone);
        currentWeapon.transform.localPosition = weaponPosition;
    }

    public void StartLightAttack() {
        currentWeapon.GetComponent<WeaponDamage>().StartLightAttack(playerStats.attackMultiplier / 100f);
    }

    public void StartHeavyAttack() {
        currentWeapon.GetComponent<WeaponDamage>().StartHeavyAttack(playerStats.attackMultiplier / 100f);
    }

    public void EndAttack() {
        currentWeapon.GetComponent<WeaponDamage>().EndAttack();
    }
}
