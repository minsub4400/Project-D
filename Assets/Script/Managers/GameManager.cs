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
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
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

    [Header("�÷��̾�")]
    public GameObject _players;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
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
                Debug.Log("���� �� �̸� : " + PhotonNetwork.CurrentRoom.Name);
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
