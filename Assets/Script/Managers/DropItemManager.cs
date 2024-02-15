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

    public List<GameObject> _pickUpItemsList;

    public void AA()
    {
        if (PhotonNetwork.IsMasterClient == false)
        {
            var a = GameObject.Find("ItemOnTheGround(Clone)");
            a.GetComponent<PickUpItem>().item = _pickUpItemsList[0].GetComponent<PickUpItem>().item;
        }
    }

    [PunRPC]
    private void DropItemData()
    {

    }
}
