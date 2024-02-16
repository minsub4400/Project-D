﻿using UnityEngine;
using System.Collections;
using Photon.Pun;
public class PickUpItem : MonoBehaviourPunCallbacks, IPunObservable
{
    public Item item;
    private Inventory _inventory;
    private GameObject _player;
    

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(item.itemID);
        }

        if (stream.IsReading)
        {
            item.itemID = (int)stream.ReceiveNext();
        }
    }

    // Use this for initialization

    /*void Start()
    {
        _player = GameManager.Instance._players;
        if (_player != null)
            _inventory = _player.GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
    }

    void Update()
    {
        if (_inventory != null && Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector3.Distance(this.gameObject.transform.position, _player.transform.position);

            if (distance <= 3)
            {
                bool check = _inventory.checkIfItemAllreadyExist(item.itemID, item.itemValue);
                if (check)
                    Destroy(this.gameObject);
                else if (_inventory.ItemsInInventory.Count < (_inventory.width * _inventory.height))
                {
                    _inventory.addItemToInventory(item.itemID, item.itemValue);
                    _inventory.updateItemList();
                    _inventory.stackableSettings();
                    Destroy(this.gameObject);
                }

            }
        }
    }*/

}