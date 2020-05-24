using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerDead))]
public class PlayerManager : TurnBaseManager
{
    public PlayerInput playerInput;
    public PlayerMovement playerMovement;
    public UnityEvent deadEvent;

    protected override void Awake()
    {
        base.Awake();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerInput.InputEnable = true;
    }
    void Update()
    {
        if (playerMovement.isMove || m_gameManager.Currenturn!=Turn.Player)
        {
            return;
        }

        playerInput.GetKeyInput();

        if (playerInput.Vertical == 0)
        {
            if (playerInput.Horizontal > 0)
            {
                playerMovement.MoveRight();
            }
            else if (playerInput.Horizontal < 0)
            {
                playerMovement.MoveLeft();
            }
        }
        else if (playerInput.Horizontal == 0)
        {
            if (playerInput.Vertical > 0)
            {
                playerMovement.MoveForword();
            }
            else if (playerInput.Vertical < 0)
            {
                playerMovement.MoveBackword();
            }
        }
    }

    public void Die()
    {
        if (deadEvent != null)
        {
            deadEvent.Invoke();
        } 
    }
}
