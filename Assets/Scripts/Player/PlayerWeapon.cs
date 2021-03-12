using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour {
    public GameObject initialWeapon;

    [SerializeField]
    private Transform handBone;

    [SerializeField]
    private Vector3 weaponPosition;

    private GameObject currentWeapon;

    [SerializeField]
    private Animator anim;

    void Start() {
        Equip(initialWeapon);
    }

    void Equip(GameObject weapon) {
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

    public void OnAttack(InputAction.CallbackContext context) {
        anim.SetTrigger("LightAttack");
    }
}
