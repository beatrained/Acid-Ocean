using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlowerBot : EnemyCharacter
{

    [SerializeField] private ScrEnemyCharacterMelee _scriptableEnemyCharacter;
    public ScrEnemyCharacterMelee ScriptableEnemyCharacter { get; private set; }

    private void Awake()
    {
        ChangeState(ActorState.Sleeping);
    }

    private void Start()
    {
        SetStats(_scriptableEnemyCharacter);
    }

    public override void HandleSleeping()
    {
        //base.HandleSleeping();
        print("flower bot is SLEEPING");
    }

    public override void HandleChasing()
    {
        //base.HandleChasing();
        print("flower bot is CHASING sth");
    }

    public void CanSeeTarget(DetectableTargetComponent target) // выше
    {
        print("I can see " + target.gameObject.name);
    }

    private void Update()
    {
        // debug
        if (Input.GetKeyDown(KeyCode.N))
        {
            ChangeState(ActorState.Chasing);
        }
    }
}
