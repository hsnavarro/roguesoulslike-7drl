using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
  public float angularVelocity = 20f;
  public CharacterController enemyController;

  void Update() {
    // enemyController.Move(new Vector3(0f, 0f, -1f) * Time.deltaTime);
  }
}
