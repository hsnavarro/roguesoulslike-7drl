using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class Reaper : MonoBehaviour {

  public bool isAttacking;
  public bool isInvoking;
  public bool isTeleporting;
  public int enemysAlive;

  public Action StartedAttacking = delegate {  };
  public Action StartedTeleporting = delegate {  };
  public Action StartedInvoking = delegate {  };

  private PlayerStats playerStats;

  public Transform[] teleportLocations;
  public float fadeDuration = 1.0f;
  public Renderer mesh;
  public Animator anim;

  public Transform[] spawnLocations;
  private EnemyGeneration enemyGenerator;

  [Header("Enemy References")]
  [SerializeField]
  private Animator reaperAnimator;
  [SerializeField]
  private EnemyStats reaperStats;
  [SerializeField]
  private CharacterController reaperCharacterController;

  private Vector3 reaperDirection;

  private void Start() {
    playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    enemyGenerator = GetComponent<EnemyGeneration>();
  }

  public void UpdateShouldInvoke() {
    if(enemysAlive == 0) {
      reaperAnimator.SetBool("ShouldInvoke", true);
    }
  }

  public void OnDeath() { 
    Object.Destroy(gameObject);
  }

  private void ApplyMovement() {
    if(isAttacking || isInvoking || isTeleporting) {
      return;
    }

    if(playerStats) {
      Vector3 direction = (playerStats.transform.position - transform.position);
      direction.y = 0;
      reaperDirection = direction.normalized;

      reaperCharacterController.Move(reaperDirection * reaperStats.speed * Time.deltaTime);
      reaperStats.transform.rotation = Quaternion.LookRotation(reaperDirection);     
    }
  }

  private void Update() {
    ApplyMovement();
  }

  public void Teleport() {
    isTeleporting = true;
    Vector3 position = teleportLocations[Random.Range(0, teleportLocations.Length - 1)].position;
    StartCoroutine(TeleportCoroutine(position));
  }

  private IEnumerator TeleportCoroutine(Vector3 position) {
    Color color;

    // fade out
    float startTime = Time.time;
    while (true) {
      yield return new WaitForEndOfFrame();
      float currentTime = Time.time;
      if (currentTime - startTime >= fadeDuration) {
        color = mesh.material.color;
        color.a = 0f;
        mesh.material.color = color;
        break;
      }

      float alpha = (currentTime - startTime) / fadeDuration;
      color = mesh.material.color;
      color.a = 1f - alpha;
      mesh.material.color = color;
    }

    // teleport
    transform.position = position;
    yield return new WaitForSeconds(0.5f);

    // fade in
    startTime = Time.time;
    while (true) {
      yield return new WaitForEndOfFrame();
      float currentTime = Time.time;
      if (currentTime - startTime >= fadeDuration) {
        color = mesh.material.color;
        color.a = 1f;
        mesh.material.color = color;
        break;
      }

      float alpha = (currentTime - startTime) / fadeDuration;
      color = mesh.material.color;
      color.a = alpha;
      mesh.material.color = color;
    }

    isTeleporting = false;
    anim.SetTrigger("Teleported");
  }

  public void SpawnMonsters() {
    // shuffle array
    int p = spawnLocations.Length;
    for (int n = p-1; n > 0 ; n--)
    {
        int r = Random.Range(0, n-1);
        Transform t = spawnLocations[r];
        spawnLocations[r] = spawnLocations[n];
        spawnLocations[n] = t;
    }

    p = 0;
    /*
    foreach (var prefab in spawnSequence[currentSpawn]) {
      GameObject enemy = Instantiate(prefab, spawnLocations[p].position, Quaternion.identity);
      p++;
    }
    */
    int spawnCount = Random.Range(2, 3);
    for (int i = 0; i < spawnCount; i++) {
      GameObject enemy = enemyGenerator.InstantiateEnemy(Random.Range(0, enemyGenerator.enemyPrefabs.Length));
      enemy.transform.position = spawnLocations[p].position;
      p++;
      enemysAlive++;
    }
  }
}