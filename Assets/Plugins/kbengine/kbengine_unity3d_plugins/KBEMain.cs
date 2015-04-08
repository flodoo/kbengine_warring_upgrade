﻿using UnityEngine;
using System;
using System.Collections;
using KBEngine;

/*
	可以理解为插件的入口模块
	在这个入口中安装了需要监听的事件(installEvents)，同时初始化KBEngine(initKBEngine)
*/
	
public class KBEMain : MonoBehaviour 
{
	public KBEngineApp gameapp = null;
	
	// 在unity3d界面中可见选项
	public bool isMultiThreads = true;
	public string ip = "127.0.0.1";
	public int port = 20013;
	public KBEngineApp.CLIENT_TYPE clientType = KBEngineApp.CLIENT_TYPE.CLIENT_TYPE_MINI;
	public string persistentDataPath = "Application.persistentDataPath";
	public bool syncPlayer = true;
	public int HZ_TICK = 100;
	public int SEND_BUFFER_MAX = (int)KBEngine.NetworkInterface.TCP_PACKET_MAX;
	public int RECV_BUFFER_MAX = (int)KBEngine.NetworkInterface.TCP_PACKET_MAX;
	
	void Awake() 
	 {
		DontDestroyOnLoad(transform.gameObject);
	 }
 
	// Use this for initialization
	void Start () 
	{
		MonoBehaviour.print("clientapp::start()");
		installEvents();
		initKBEngine();
	}
	
	public virtual void installEvents()
	{
	}
	
	public virtual void initKBEngine()
	{
		// 如果此处发生错误，请查看 Assets\Scripts\kbe_scripts\if_Entity_error_use______git_submodule_update_____kbengine_plugins_______open_this_file_and_I_will_tell_you.cs
		
		KBEngineArgs args = new KBEngineArgs();
		
		args.ip = ip;
		args.port = port;
		args.clientType = clientType;
		
		if(persistentDataPath == "Application.persistentDataPath")
			args.persistentDataPath = Application.persistentDataPath;
		else
			args.persistentDataPath = persistentDataPath;
		
		args.syncPlayer = syncPlayer;
		args.HZ_TICK = HZ_TICK;
		
		args.SEND_BUFFER_MAX = (UInt32)SEND_BUFFER_MAX;
		args.RECV_BUFFER_MAX = (UInt32)RECV_BUFFER_MAX;
		
		args.isMultiThreads = isMultiThreads;
		
		if(isMultiThreads)
			gameapp = new KBEngineAppThread(args);
		else
			gameapp = new KBEngineApp(args);
	}
	
	void OnDestroy()
	{
		MonoBehaviour.print("clientapp::OnDestroy(): begin");
		KBEngineApp.app.destroy();
		MonoBehaviour.print("clientapp::OnDestroy(): end");
	}
	
	void FixedUpdate () 
	{
		KBEUpdate();
	}

	public virtual void KBEUpdate()
	{
		// 单线程模式必须自己调用
		if(!isMultiThreads)
			gameapp.process();
		
		KBEngine.Event.processOutEvents();
	}
}
