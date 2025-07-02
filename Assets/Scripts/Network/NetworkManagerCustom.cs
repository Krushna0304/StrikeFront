using TMPro;
using Mirror;
using UnityEngine;
public class NetworkManagerCustom : NetworkManager
{
    public TMP_InputField ipInputField;
    public void JoinServer()
    {
        //NetworkManager.singleton.StartHost();
        NetworkManager.singleton.StartClient();
    }
/*    public override void OnClientConnect()
    {
        Debug.Log("Player Connected");
    }
    public override void OnStartClient()
    {
        Debug.Log("Player Start");
    }*/
}
