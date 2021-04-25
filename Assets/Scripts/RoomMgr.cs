using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nettention.Proud;
using TMPro;

public class Room
{
    public Room(string name, int pin, int max)
    {
        this.name = name;
        this.pin = pin;
        this.max = max;
    }
    public string name;
    public int pin;
    public int max;
}
public class RoomMgr : MonoBehaviour
{
    public static RoomMgr Instance = null;
    public List<Room> Rooms = new List<Room>();

    [Header("DropDowns")]
    public TMP_Dropdown Max;
    [Header("InputFields")]
    public TMP_InputField Name;
    public TMP_InputField Pin;

    private void Start()
    {
        Instance = this;
    }
    public void RoomCreate()
    {
        Client.Proxy.CreateRoom(HostID.HostID_Server, RmiContext.ReliableSend, Name.text, int.Parse(Pin.text), Max.value + 2);
        RoomJoin();
        Rooms.Add(new Room(Name.text, int.Parse(Pin.text), Max.value));
        Debug.Log($"Name : {Name.text}, Pin : {int.Parse(Pin.text)}, Max : {Max.value + 2}");
    }
    public void RoomLeave()
    {
        Client.Proxy.LeaveRoom(HostID.HostID_Server, RmiContext.ReliableSend, Client.Room_name);
        Client.Room_name = default;
        UiMgr.Instance.GoLobby();
    }
    public void RoomJoin()
    {
        Client.Proxy.JoinRoom(HostID.HostID_Server, RmiContext.ReliableSend, Name.text, int.Parse(Pin.text));
        Client.Room_name = Name.text;
        Client.isReady = false;
        UiMgr.Instance.GoWaiting();
    }
}
