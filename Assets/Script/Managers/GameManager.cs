using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

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

        DropItemManager.Instance.AA();
    }
    private void PlayerPutArr()
    {
        var players = GameObject.FindGameObjectWithTag("Player");
        _players = players;
        /*if (players.Length > 1)
        {
            BubbleSort(players);
        }
        else
        {
            _players = players;
        }*/
    }

    /*public void BubbleSort(GameObject[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j].GetComponent<PhotonView>().ViewID > arr[j + 1].GetComponent<PhotonView>().ViewID)
                {
                    // ������ ��Ҹ� ��ȯ
                    GameObject temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }

        _players = arr;
    }*/
    private void PutInventory()
    {
        var p = _players.GetComponent<PlayerInventory>();
        p.inventory = _inventory;
        p.characterSystem = _equipment;
        p.craftSystem = _craft;
        /*foreach (var player in _players)
        {
            var p = player.GetComponent<PlayerInventory>();

            if (photonView.IsMine)
            {
                if (p.inventory != null)
                {
                    continue;
                }
                p.inventory = _inventory;
                p.characterSystem = _equipment;
                p.craftSystem = _craft;
            }
            else
            {
                if (p.inventory != null)
                {
                    continue;
                }
                p.inventory = _inventory;
                p.characterSystem = _equipment;
                p.craftSystem = _craft;
            }
        }*/
    }

}
