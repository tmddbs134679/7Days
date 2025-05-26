using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    private PlayerController playerController;
    private PlayerStatus playerStatus;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        //playerStatus = GetComponent<PlayerStatus>();

        playerController.Init(playerData);
    }
}
