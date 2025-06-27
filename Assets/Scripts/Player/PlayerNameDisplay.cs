using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerNameDisplay : MonoBehaviourPun
{
    public TextMeshPro nameText;
    void Start()
    {
        if (photonView.IsMine)
        {
            nameText.text = PhotonNetwork.NickName;
        }
        else
        {
            nameText.text = photonView.Owner.NickName;
        }
    }

    void LateUpdate()
    {
        nameText.transform.rotation = Camera.main.transform.rotation; // Always face camera
    }
}
