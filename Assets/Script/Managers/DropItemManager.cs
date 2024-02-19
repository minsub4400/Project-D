using Newtonsoft.Json;
using Photon.Pun;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemManager : MonoBehaviourPun
{
    private static DropItemManager _instance;
    public static DropItemManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(DropItemManager)) as DropItemManager;

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

    //private List<GameObject> _dropItemSync = new List<GameObject>();
    private List<int> _dropItemSync_Id = new List<int>();
    private List<int> _dropItemSync_Count = new List<int>();

    private GameObject _dropItemList;
    public GameObject DropItemList
    {
        get => _dropItemList;
        set
        {
            _dropItemList = value;
            var id = _dropItemList.GetComponent<PickUpItem>().item.itemID;
            var count = _dropItemList.GetComponent<PickUpItem>().item.itemValue;

            var jsonConvert_id = JsonConvert.SerializeObject(id);
            var jsonConvert_count = JsonConvert.SerializeObject(count);

            photonView.RPC(nameof(SetDropItemList_ID), RpcTarget.All, jsonConvert_id);
            photonView.RPC(nameof(SetDropItemList_Count), RpcTarget.All, jsonConvert_count);

            /*var a = _dropItemList;
            var s = JsonUtility.ToJson(_dropItemList);
            photonView.RPC(nameof(DropItemSync), RpcTarget.All, s);*/
        }
    }


    [PunRPC]
    public void SetDropItemList_ID(string value)
    {
        /*var jsonConvert_id = JsonConvert.DeserializeObject<int>(value);
        var gameobj = (GameObject)jsonConvert;
        _dropItemList = gameobj;*/
    }

    [PunRPC]
    public void SetDropItemList_Count(string value)
    {
        /*var jsonConvert = JsonConvert.DeserializeObject<object>(obj);
        var gameobj = (GameObject)jsonConvert;
        _dropItemList = gameobj;*/
    }




    [PunRPC]
    public void DropItemSync(string s)
    {
        /*ItemDataBaseList itemDataBaseList = new ItemDataBaseList();
        _dropItemList = itemDataBaseList.getItemByID(int.Parse(s));
        var g = JsonConvert.DeserializeObject<int>(s);
        _dropItemSync.Add(g);*/
    }

    
    public void AA()
    {
        photonView.RPC(nameof(BB), RpcTarget.All);
    }

    [PunRPC]
    public void BB()
    {
        if (PhotonNetwork.IsMasterClient == false)
        {
            
        }
    }

}
