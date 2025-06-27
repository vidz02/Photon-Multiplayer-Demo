using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPun
{
    public float speed = 5f;

    void Update()
    {
        if (!photonView.IsMine) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v) * speed * Time.deltaTime;
        transform.Translate(move);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //PhotonNetwork.Instantiate("Bullet", transform.position + transform.forward, transform.rotation);
            // This line is commented out because we are using RPC to instantiate the bullet across the network.
            photonView.RPC("FireBullet", RpcTarget.All);
        }
    }
    
    [PunRPC]
    void FireBullet()
    {
        GameObject bullet = Instantiate(Resources.Load("Bullet") as GameObject, transform.position + transform.forward, transform.rotation);
    }
}
