using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TrashEnemy : MonoBehaviour, AnimationManager.AnimationProvider, CharacterDetector.Listener, SiphonSource
{

    private Animator animator;

    [SerializeField]
    private CharacterDetector characterDetector;

    [SerializeField]
    private State state = State.MOUND;

    [SerializeField]
    private Transform siphonSourcePosition;

    [SerializeField]
    private float spawnDropletCooldown = 0.1f;

    [SerializeField]
    private GameObject dropletPrototype;

    private AnimationManager animationManager;

    private float timeSinceSpawnedDroplet;

    private int health = 500;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.animationManager = new AnimationManager(animator, this);
        characterDetector.AddListener(this);
    }

    // Update is called once per frame
    void Update()
    {
        animationManager.Update();
        timeSinceSpawnedDroplet += Time.deltaTime;
    }

    public void OnSiphoned(Vector3 siphonPosition, float siphonForce) {
        Debug.Log("siphoned");
        if (state == State.IDLE) {
            ChangeState(State.PAINED);
        } else if (state == State.PAINED) {
            health -= 1;
            if (timeSinceSpawnedDroplet > spawnDropletCooldown) {
                SiphonDroplet droplet = GameObject.Instantiate(dropletPrototype, siphonSourcePosition.position, Quaternion.identity).GetComponent<SiphonDroplet>();
                droplet.SetInitialVelocity(Random.insideUnitCircle * 5);
                timeSinceSpawnedDroplet = 0f;
            }
        }

        if (health <= 0) {
            ChangeState(State.DYING);
        }
    }

    public void OnSiphonStopped() {
        Debug.Log("siphonStopped");
        if (state == State.PAINED) {
            if (characterDetector.isPlayerDetected) {
                ChangeState(State.IDLE);
            } else {
                ChangeState(State.RETREATING);
            }
        }
    }

    public void OnPlayerEntered() {
        Debug.Log("state: " + state);
        if (state == State.MOUND) {
            ChangeState(State.EMERGING);
        }
    }

    public void OnPlayerExited() {
        if (state == State.IDLE) {
            ChangeState(State.RETREATING);
        }
    }

    public void OnFinishRetreating() {
        if (characterDetector.isPlayerDetected) {
            ChangeState(State.EMERGING);
        } else {
            ChangeState(State.MOUND);
        }
    }

    public void OnFinishEmerging() {
        if (characterDetector.isPlayerDetected) {
            ChangeState(State.IDLE);
        } else {
            ChangeState(State.RETREATING);
        }
    }

    public string GetAnimation() {
        switch (state) {
            case State.MOUND:
                return "TrashEnemyMound";
            case State.EMERGING:
                return "TrashEnemyEmerging";
            case State.IDLE:
                return "TrashEnemyIdle";
            case State.RETREATING:
                return "TrashEnemyRetreating";
            case State.PAINED:
                return "TrashEnemyPained";
            case State.DYING:
                return "TrashEnemyDying";
        }

        return "TrashEnemyMound";
    }

    private void ChangeState(State state) {
        this.state = state;
    }

    private enum State {
        MOUND,
        EMERGING,
        IDLE,
        RETREATING,
        PAINED,
        DYING
    }
}
