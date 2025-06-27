using Photon.Pun;
using UnityEngine;

public class PlayerColorSync : MonoBehaviourPun, IPunObservable
{
    private Color playerColor;

    void Start()
    {

        if (photonView.IsMine)
        {
            // Assign a random color to local player
            playerColor = new Color(Random.value, Random.value, Random.value);
        }

        // Apply color to material
        ApplyColor();
    }

    private void ApplyColor()
    {
        Renderer rend = GetComponent<Renderer>();

        if (rend == null)
        {
            Debug.LogWarning("Renderer not found on Player prefab in PlayerColorSync.");
            rend = GetComponentInChildren<Renderer>();
        }
        rend.material.color = playerColor;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Send color to other players
            stream.SendNext(playerColor.r);
            stream.SendNext(playerColor.g);
            stream.SendNext(playerColor.b);
        }
        else
        {
            // Receive color
            float r = (float)stream.ReceiveNext();
            float g = (float)stream.ReceiveNext();
            float b = (float)stream.ReceiveNext();
            playerColor = new Color(r, g, b);

            ApplyColor();
        }
    }
}
