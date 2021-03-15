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
  private float smoothTime = 0.5f;
  [SerializeField]
  private float projectileDuration = 10f;

  private Vector2 projectileCurrentDirection;
  private Vector2 projectileDesiredDirection;

  private Vector2 currentVelocity;

  private float timeProjectileWasSpawned;

  private Vector3 playerHeadOffset = new Vector3(0f, 1.5f, 0f);

  private void Start() {
    playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    timeProjectileWasSpawned = Time.time;

    Vector3 direction = (playerStats.transform.position + playerHeadOffset - transform.position);
    projectileCurrentDirection = new Vector2(direction.x, direction.z).normalized;
  }

  private void OnTriggerEnter(Collider collider) {
    if (collider.gameObject.layer == (int)Layers.PLAYER_COLLISION_TRIGGER) playerStats.playerResilience.TakeDamage(projectileDamage);
    Object.Destroy(gameObject);
  }

  private void FixedUpdate() {
    if(playerStats) {
      Vector3 direction = (playerStats.transform.position + playerHeadOffset - transform.position);
      float directionY = direction.y;
      direction.y = 0;
      projectileDesiredDirection = new Vector2(direction.x, direction.z).normalized;

      projectileCurrentDirection = Vector2.SmoothDamp(projectileCurrentDirection, projectileDesiredDirection, ref currentVelocity, smoothTime);

      Vector3 projectileMovement = new Vector3(projectileCurrentDirection.x, directionY, projectileCurrentDirection.y).normalized;
      projectileCharacterController.Move(projectileMovement * projectileSpeed * Time.deltaTime);
    }

    if (Time.time - timeProjectileWasSpawned > projectileDuration) Object.Destroy(gameObject);
  }
}
