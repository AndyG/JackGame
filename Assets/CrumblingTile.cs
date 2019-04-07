using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CrumblingTile : MonoBehaviour
{

  private Rigidbody2D rb;
  private CharacterDetector characterDetector;

  void Start()
  {
    this.rb = GetComponent<Rigidbody2D>();
    this.characterDetector = GetComponentInChildren<CharacterDetector>();
  }

  public bool IsCharacterDetected() => characterDetector.isPlayerDetected;
}
