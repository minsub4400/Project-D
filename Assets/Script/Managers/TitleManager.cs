using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using TMPro;

public class TitleManager : MonoBehaviourPunCallbacks
{
    private readonly string _gameVersion = "1.0";

    [SerializeField]
    private TMP_InputField _inputRoomName;

    [Header("��ư")]
    [Header("���� ����")]
    [SerializeField]
    private Button _startGame;
    [Header("���� ����")]
    [SerializeField]
    private Button _joinGame;
    [Header("���� ������")]
    [SerializeField]
    private Button _exitGame;
    

    private static TitleManager _instance;
    public static TitleManager Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(TitleManager)) as TitleManager;

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

    void Start()
    {
        PhotonNetwork.GameVersion = _gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("������ ���� ���� ����...");
        //Instantiate(playerStoragePre, transform.position, transform.rotation);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        // �� ���� ��ư�� ��Ȱ��ȭ
        //joinButton.interactable = false;
        // ���� ���� ǥ��
        Debug.Log("������ ���� ���� ����...");

        // ������ �������� ������ �õ�
        //PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("�� ���� ����");
        PhotonNetwork.LoadLevel("InGameScene");
    }

    #region Button �Լ�
    // ���� ����
    public void OnConnect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
            //PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }

    }

    // ����
    public void OnJoin()
    {
        PhotonNetwork.JoinRoom(_inputRoomName.text);
    }

    // ������
    public void OnExit()
    {

    }
    #endregion
}
