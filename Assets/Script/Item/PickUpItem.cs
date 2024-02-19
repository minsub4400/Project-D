using UnityEngine;
using System.Collections;
using Photon.Pun;
using static UnityEditor.Progress;
using Photon.Realtime;
using UnityEngine.InputSystem;
using Cysharp.Threading.Tasks;


namespace StarterAssets
{
    public class PickUpItem : MonoBehaviourPunCallbacks, IPunObservable, IPunOwnershipCallbacks


    {
        public Item item;
        private Inventory _inventory;
        private GameObject _player;


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(item.itemID);

                /*stream.SendNext(item.itemName);
                stream.SendNext(item.itemID);
                stream.SendNext(item.itemDesc);
                stream.SendNext(item.itemValue);
                stream.SendNext(item.itemWeight);
                stream.SendNext(item.maxStack);
                stream.SendNext(item.indexItemInList);
                stream.SendNext(item.rarity);*/
            }

            if (stream.IsReading)
            {
                item.itemID = (int)stream.ReceiveNext();
            }
        }
        void Start()
        {
            if (photonView.IsMine == false)
            {
                if (item != null)
                {
                    var itemDatabase = Resources.Load<ItemDataBaseList>("ItemDatabase");
                    item = itemDatabase.getItemByID(item.itemID);
                }

            }
            _player = GameManager.Instance._players;
            if (_player != null)
                _inventory = _player.GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
        }

        void Update()
        {
            /*if (_inventory != null && Input.GetKeyDown(KeyCode.E))
            {
                float distance = Vector3.Distance(this.gameObject.transform.position, _player.transform.position);

                if (distance <= 3)
                {
                    OnOwnerTransferOwnerShip(_player);
                    bool check = _inventory.checkIfItemAllreadyExist(item.itemID, item.itemValue);
                    if (check)
                    {
                        //PhotonNetwork.Destroy(this.gameObject);
                    }
                    else if (_inventory.ItemsInInventory.Count < (_inventory.width * _inventory.height))
                    {
                        _inventory.addItemToInventory(item.itemID, item.itemValue);
                        _inventory.updateItemList();
                        _inventory.stackableSettings();
                        //PhotonNetwork.Destroy(this.gameObject);
                    }

                    if (this.photonView.IsMine)
                    {
                        PhotonNetwork.Destroy(this.gameObject);
                    }
                }
            }*/
        }

        public void DropItemParming()
        {
            float distance = Vector3.Distance(this.gameObject.transform.position, _player.transform.position);

            if (distance <= 1.5)
            {
                OnOwnerTransferOwnerShip(_player);
                bool check = _inventory.checkIfItemAllreadyExist(item.itemID, item.itemValue);
                if (check)
                {
                    //PhotonNetwork.Destroy(this.gameObject);
                }
                else if (_inventory.ItemsInInventory.Count < (_inventory.width * _inventory.height))
                {
                    _inventory.addItemToInventory(item.itemID, item.itemValue);
                    _inventory.updateItemList();
                    _inventory.stackableSettings();
                    //PhotonNetwork.Destroy(this.gameObject);
                }

                StartCoroutine(WaitingForIsMine());
            }
        }

        private IEnumerator WaitingForIsMine()
        {
            yield return new WaitForSecondsRealtime(1.0f);

            if (this.photonView.IsMine)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
        }

        private void OnOwnerTransferOwnerShip(GameObject player)
        {
            int playerPhotonView = player.GetComponent<PhotonView>().ViewID;
            photonView.RequestOwnership();
            //photonView.TransferOwnership(playerPhotonView);
        }

        public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
        {
            //Debug.Log($"{targetView.ViewID} : 요청");
        }

        public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
        {
            //Debug.Log($"{targetView.ViewID} : 로 이전");
        }

        public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
        {
            //Debug.Log("소유권 이전 실패!");
        }
        public void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            var playerInput = other.GetComponent<StarterAssetsInputs>();
            playerInput._pickUpItem = this;
        }
    }
}

