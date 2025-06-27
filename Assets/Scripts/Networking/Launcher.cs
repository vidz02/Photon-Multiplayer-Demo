using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    public InputField nameInput;
    public Button connectButton;
    public GameObject loginUI;

    public void Start()
    {
        connectButton.onClick.AddListener(() =>
        {
            PhotonNetwork.NickName = nameInput.text;

            Debug.Log("Connecting " + PhotonNetwork.NickName + " to Photon...");
            PhotonNetwork.ConnectUsingSettings();
        });
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No room found, creating one.");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room!");
        
        loginUI.SetActive(false);

        Vector3 randomSpawn = new Vector3(Random.Range(-3f, 3f), 0.5f, Random.Range(-3f, 3f));
        PhotonNetwork.Instantiate("Player", randomSpawn, Quaternion.identity);
    }
}