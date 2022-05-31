using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using agora_gaming_rtc;
using System;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;        //photon hashtable 사용

public class VoiceChatManager : MonoBehaviourPunCallbacks
{
    string appID = "975ddf37dc2142ef9794a476a70e5b6c";

    public static VoiceChatManager Instance;


    IRtcEngine rtcEngine;


    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);

        }
        else
        {
            Permission.RequestUserPermission(Permission.Microphone);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        rtcEngine = IRtcEngine.GetEngine(appID);
        rtcEngine.OnJoinChannelSuccess += OnJoinChannelSuccess;
        rtcEngine.OnLeaveChannel += OnLeaveChannel;
        rtcEngine.OnError += OnError;

        rtcEngine.EnableSoundPositionIndication(true);
    }

    void OnError(int error, string msg)
    {
        Debug.LogError("Error with.Agora: " + msg);
    }

    void OnLeaveChannel(RtcStats stats)
    {
        Debug.Log("Left channel with duration " + stats.duration);
    }

    void OnJoinChannelSuccess(string channelName, uint uid, int elapsed)        //채널 조인 성공시
    {
        
        // agoraID 해쉬 태이블에 추가
        Hashtable hash = new Hashtable();
        hash.Add("agoraID", uid.ToString());        // agoraID = uid , string으로 형변환
        PhotonNetwork.SetPlayerCustomProperties(hash);
    }

    public IRtcEngine GetRtcEngine()
    {
        return rtcEngine;
    }


    public override void OnJoinedRoom()
    {
        rtcEngine.JoinChannel(PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnLeftRoom()
    {
        rtcEngine.LeaveChannel();
    }

    void OnDestroy()
    {
        IRtcEngine.Destroy();
    }

}
