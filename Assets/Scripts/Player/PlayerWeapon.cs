using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
    [SerializeField]
    private GameObject initialWeapon;

    [SerializeField]
    private Transform handBone;

    [SerializeField]
    private Vector3 weaponPosition;

    private GameObject currentWeapon;

    private void Start() {
        Equip(initialWeapon);
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
        currentWeapon.GetComponent<WeaponDamage>().StartLightAttack();
    }

    public void StartHeavyAttack() {
        currentWeapon.GetComponent<WeaponDamage>().StartHeavyAttack();
    }

    public void EndAttack() {
        currentWeapon.GetComponent<WeaponDamage>().EndAttack();
    }
}
