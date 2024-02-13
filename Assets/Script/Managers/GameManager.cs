using Cinemachine;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public Transform[] _spawnPositions;

    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    private void Start()
    {
        PlayerSpawn();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            if (PhotonNetwork.InRoom)
            {
                Debug.Log("현재 방 이름 : " + PhotonNetwork.CurrentRoom.Name);
            }
        }
    }

    private void PlayerSpawn()
    {
        var localPlayerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        var spawnPosition = _spawnPositions[localPlayerIndex % _spawnPositions.Length];

        var player = PhotonNetwork.Instantiate(_playerPrefab.name, spawnPosition.position, Quaternion.identity);
        if (photonView.IsMine)
        {
            _cinemachineVirtualCamera.Follow = player.transform.GetChild(0).transform;
        }
        else
        {
            _cinemachineVirtualCamera.Follow = player.transform.GetChild(0).transform;
        }
    }
}
