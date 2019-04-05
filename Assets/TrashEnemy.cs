using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TrashEnemy : MonoBehaviour, AnimationManager.AnimationProvider, CharacterDetector.Listener
{

    private Animator animator;

    [SerializeField]
    private CharacterDetector characterDetector;

    [SerializeField]
    private State state = State.MOUND;

    private AnimationManager animationManager;

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
        RETREATING
    }
}
