using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    public Transform[] _spawnPositions;

    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    [SerializeField]
    private GameObject _inventory;

    [SerializeField]
    private GameObject _equipment;

    [SerializeField]
    private GameObject _craft;

    [SerializeField]
    private Slider _slider;

    [Header("플레이어")]
    public GameObject _players;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

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

        if (Input.GetKey(KeyCode.K))
        {
            if (DropItemManager.Instance.DropItemList != null)
                Debug.Log(DropItemManager.Instance.DropItemList.name);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        DropItemManager.Instance.AA();
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

        PlayerPutArr();
        PutInventory();
        PutHealthSlider();
    }
    private void PlayerPutArr()
    {
        var players = GameObject.FindGameObjectWithTag("Player");
        _players = players;
    }
    private void PutInventory()
    {
        var p = _players.GetComponent<PlayerInventory>();
        p.inventory = _inventory;
        p.characterSystem = _equipment;
        p.craftSystem = _craft;
    }
    private void PutHealthSlider()
    {
        var p = _players.GetComponent<PlayerHealth>();
        p.healthSlider = _slider;
    }

}
