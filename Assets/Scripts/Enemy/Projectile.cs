using UnityEngine;

public class Projectile : MonoBehaviour {
  private PlayerStats playerStats;

  [Header("Projectile Stats")]
  [SerializeField]
  private CharacterController projectileCharacterController;
  [SerializeField]
  private float projectileDamage;
  [SerializeField]
  private float projectileSpeed;
  [SerializeField]
  private float rotateSmooth = 1f;
  [SerializeField]
  private float projectileDuration = 10f;

  private Vector2 projectileCurrentDirection;
  private Vector2 projectileDesiredDirection;

  private float timeProjectileWasSpawned;

  private void Start() {
    playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    timeProjectileWasSpawned = Time.time;

    Vector3 direction = (playerStats.transform.position - transform.position);
    projectileCurrentDirection = new Vector2(direction.x, direction.z).normalized;
  }

  private void OnTriggerStay(Collider collider) {
    if (collider.gameObject.layer == (int)Layers.PLAYER_COLLISION_TRIGGER) playerStats.playerResilience.TakeDamage(projectileDamage);
    Object.Destroy(gameObject);
  }

  private void Update() {
    if(playerStats) {
      Vector3 direction = (playerStats.transform.position - transform.position);
      direction.y = 0;
      projectileDesiredDirection = new Vector2(direction.x, direction.z).normalized;

      projectileCurrentDirection = Vector2.Lerp(projectileCurrentDirection, projectileDesiredDirection, rotateSmooth).normalized;

      Vector3 projectileMovement = new Vector3(projectileCurrentDirection.x, 0, projectileCurrentDirection.y).normalized;
      projectileCharacterController.Move(projectileMovement * projectileSpeed * Time.deltaTime);
    }

    if (Time.time - timeProjectileWasSpawned > projectileDuration) Object.Destroy(gameObject);
  }
}
