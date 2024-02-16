using Photon.Pun;
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
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
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

        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private GameObject _dropItemList;
    public GameObject DropItemList
    {
        get => _dropItemList;
        set
        {
            _dropItemList = value;
        }
    }

    public void AA()
    {
        if (PhotonNetwork.IsMasterClient == false)
        {
            var a = GameObject.Find("ItemOnTheGround(Clone)");
            a.GetComponent<PickUpItem>().item = _dropItemList.GetComponent<PickUpItem>().item;
        }
    }

    [PunRPC]
    private void DropItemData()
    {

    }
}
