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

    [Header("버튼")]
    [Header("게임 시작")]
    [SerializeField]
    private Button _startGame;
    [Header("게임 참가")]
    [SerializeField]
    private Button _joinGame;
    [Header("게임 나가기")]
    [SerializeField]
    private Button _exitGame;
    

    private static TitleManager _instance;
    public static TitleManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
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

        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
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
        Debug.Log("마스터 서버 접속 성공...");
        //Instantiate(playerStoragePre, transform.position, transform.rotation);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        // 룸 접속 버튼을 비활성화
        //joinButton.interactable = false;
        // 접속 정보 표시
        Debug.Log("마스터 서버 접속 실패...");

        // 마스터 서버로의 재접속 시도
        //PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("방 참가 성공");
        PhotonNetwork.LoadLevel("InGameScene");
    }

    #region Button 함수
    // 게임 시작
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

    // 조인
    public void OnJoin()
    {
        PhotonNetwork.JoinRoom(_inputRoomName.text);
    }

    // 나가기
    public void OnExit()
    {

    }
    #endregion
}
