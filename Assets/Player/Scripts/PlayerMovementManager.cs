using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    private PlayerController playerController;

    public Vector2 currentDirection;

    [SerializeField] float speed;
    [SerializeField] PlayerFootSteps footSteps;
    

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        if (!playerController.isPlayerJumping && !playerController.player.isPlayerDead)
        {

            transform.position += new Vector3(currentDirection.x, 0,0) * speed * Time.deltaTime;

        }
    }

    private void OnPlayerTurning()
    {
        float scaleX = 1;

        if (currentDirection.x > 0)
        {
            scaleX = 1;
        }
        else if (currentDirection.x < 0)
        {
            scaleX = -1;
        }
        else if (currentDirection.x == 0)
        {
            scaleX = transform.localScale.x;
        }

        transform.localScale = new Vector3(scaleX, 1, 1);
    }
    private void ManageRunningAnimation()
    {
        if (currentDirection.x != 0 && !playerController.isPlayerJumping)
        {
            StartRunning();
        }
        else
        {
            StopRunning();
        }
    }

    public void StartRunning()
    {
        playerController.animationController.SetBoolForAnimations("isRunning", true);
        footSteps.OnStartPlayerFootstep();
        
    }

    public void StopRunning()
    {
        playerController.animationController.SetBoolForAnimations("isRunning", false);
        footSteps.OnEndPlayerFootstep();
       
    }


    public void HandleMovement(Vector2 dir)
    {
        currentDirection = dir;
        OnPlayerTurning();
        ManageRunningAnimation();
    }
}
