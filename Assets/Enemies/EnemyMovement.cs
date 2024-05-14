using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //private EnemyAI enemy;
    private EnemyController enemyController;

    private int currentDir = 1;
    private bool isTurning = false;

    EnemyStateEnum lastState;

    private void Awake()
    {
        //enemy = GetComponent<EnemyAI>();
        enemyController = GetComponent<EnemyController>();
    }

    public void MoveTo(Vector2 startPoint, Vector2 endPoint, float speed)
    {
        if (!isTurning)
        {
            Vector2 direction = endPoint - startPoint;
            transform.position = Vector2.MoveTowards(startPoint, endPoint, speed * Time.deltaTime);
            TurnEnemy(direction);
            //enemy.rb.velocity = direction * enemySpeed;
        }

    }

    private void TurnEnemy(Vector2 direction)
    {
        
        if(direction.x < 0 && currentDir != 1)
        {
            OnEnemyBeginTurning(1);

        }
        if(direction.x >= 0 && currentDir != -1)
        {
            OnEnemyBeginTurning(-1);
        }
    }

    private void OnEnemyBeginTurning(int dir)
    {
        currentDir = dir;
        isTurning = true;

        lastState = enemyController.currentStateEnum;
        enemyController.ChangeState(EnemyStateEnum.Turn);
      
    }
    public void OnEnemyEndTurning(bool value)
    {
        isTurning = value;
        enemyController.ChangeState(lastState);
        transform.localScale = new Vector3(currentDir, 1, 1);
        enemyController.GetDialogueBox().transform.localScale = new Vector3(currentDir, 1, 1);


    }
}
