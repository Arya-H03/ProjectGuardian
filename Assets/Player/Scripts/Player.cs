using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using static UnityEngine.Rendering.DebugUI;

public class Player : MonoBehaviour
{
    public delegate void MyEventHandler();

    public static event MyEventHandler CorruptionEvent;
    private PlayerController controller;

    [SerializeField] PlayerHealthBar healthBar;
  
    public int maxCorupption = 100;
    public int currentCorupption = 0;

    public int maxHealth = 100;
    public int currentHealth = 0;
    private Material material;

    private void Awake()
    {
        material = GetComponentInChildren<SpriteRenderer>().material;
        controller = GetComponent<PlayerController>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void OnGainCorruption(int value)
    {

        if (currentCorupption < maxCorupption)
        {
            currentCorupption += value;
            Debug.Log(currentCorupption);

            if (currentCorupption > maxCorupption)
            {
                currentCorupption = maxCorupption;
            }

            if(currentCorupption == maxCorupption)
            {
                ChangeShader();
            }
        }

        CorruptionEvent?.Invoke();
    }

    public void OnTakingDamage(int value)
    {

        //controller.animationController.SetTriggerForAnimations("Hit");
        controller.animationController.SetBoolForAnimations("isHit", true);
        if (currentHealth > 0)
        {
            controller.rb.velocity += controller.playerMovementManager.currentDirection * -5;
            currentHealth -= value;
            if(currentHealth <=0)
            {
                currentHealth = 0;
                //Debug.Log("dEAD");
            }
        }
        else
        {
            //Debug.Log("You are dead");
        }

        healthBar.ChangeHealthBar();
       
    }

    public void OnKnockBack(Vector2 launchVector,float enemyXpos)
    {
        if(this.transform.position.x - enemyXpos < 0)
        {
            controller.rb.velocity += new Vector2(- launchVector.x, launchVector.y);
        }
        else
        {
            controller.rb.velocity += new Vector2(launchVector.x, launchVector.y);
        }
        
    }

    private void ChangeShader()
    {
        material.SetFloat("_isCorrupt", 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnGainCorruption(5);

        }

    }
}
