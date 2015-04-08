using UnityEngine;
using KBEngine;
using System; 
using System.Collections;

public class ui : MonoBehaviour {

	public static string currstate = "";
	public static bool started = false;
	public static UIAtlas uiatlas = null;

    //add by firstOne
    UILabel log_Label;
    UILabel reg_repassword_Label;
    UILabel password_Label;
    //UILabel log_label;

    UIButton losepasswordBtn;
    UIButton backBtn;
    
    UIButton registerBtn;
    UIButton loginBtn;
    UIButton reg_commitBtn;

    UIInput username;
    UIInput password;
    UIInput reg_username;
    UIInput reg_password;
    UIInput reg_passwordok;

	
	void Awake ()     
	{
		if(started == false)
		{
			started = true;
			InvokeRepeating("installEvents", 0.5f, 0.0f);
		}
	}
	
	// Use this for initialization
	void Start () {
        log_Label = transform.Find("log_Label").GetComponent<UILabel>();
        reg_repassword_Label = transform.Find("reg_repassword_Label").GetComponent<UILabel>();
        password_Label = transform.Find("password_Label").GetComponent<UILabel>();
        loginBtn = transform.Find("login").GetComponent<UIButton>();
        backBtn = transform.Find("back").GetComponent<UIButton>();
        registerBtn = transform.Find("register").GetComponent<UIButton>();
        losepasswordBtn = transform.Find("losepassword").GetComponent<UIButton>();
        reg_commitBtn = transform.Find("reg_commit").GetComponent<UIButton>();
        //UILabel log_label;
        if (init_screen.inst != null)
            init_screen.inst.close_screen();
        //UIButton losepassword;
        //UIButton back;
        //UIButton reg_password;
        //UIButton reg_passwordok;
        //UIButton register;
        username = transform.Find("username").GetComponent<UIInput>();
        password = transform.Find("password").GetComponent<UIInput>();
        reg_username = transform.Find("reg_username").GetComponent<UIInput>();
        reg_password = transform.Find("reg_password").GetComponent<UIInput>();
        reg_passwordok = transform.Find("reg_passwordok").GetComponent<UIInput>();
        NGUITools.SetActive(reg_repassword_Label.gameObject, false);
        NGUITools.SetActive(reg_passwordok.gameObject, false);
        NGUITools.SetActive(backBtn.gameObject, false);
        NGUITools.SetActive(reg_commitBtn.gameObject, false);
        NGUITools.SetActive(reg_username.gameObject, false);
        NGUITools.SetActive(reg_password.gameObject, false);
        UIEventListener.Get(loginBtn.gameObject).onClick = (UnityEngine.GameObject go) =>
        {
            login();
        };
        //UIButton login;
        //UIButton reg_commit;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnDestroy()
	{
		KBEngine.Event.deregisterOut(this);
		started = false;
	}
	
	void installEvents()
	{
		CancelInvoke("installEvents");
		KBEngine.Event.registerOut("onCreateAccountResult", this, "onCreateAccountResult");
		KBEngine.Event.registerOut("onLoginFailed", this, "onLoginFailed");
		KBEngine.Event.registerOut("onVersionNotMatch", this, "onVersionNotMatch");
		KBEngine.Event.registerOut("onScriptVersionNotMatch", this, "onScriptVersionNotMatch");
		KBEngine.Event.registerOut("onLoginGatewayFailed", this, "onLoginGatewayFailed");
		KBEngine.Event.registerOut("onLoginSuccessfully", this, "onLoginSuccessfully");
		KBEngine.Event.registerOut("login_baseapp", this, "login_baseapp");
		KBEngine.Event.registerOut("onConnectStatus", this, "onConnectStatus");
		KBEngine.Event.registerOut("Loginapp_importClientMessages", this, "Loginapp_importClientMessages");
		KBEngine.Event.registerOut("Baseapp_importClientMessages", this, "Baseapp_importClientMessages");
		KBEngine.Event.registerOut("Baseapp_importClientEntityDef", this, "Baseapp_importClientEntityDef");
	}
	
	void login()
	{
        Common.DEBUG_MSG("login is Click, name=" + username.value + ", password=" + password.value + "!");
		
		log_Label.text = "请求连接服务器...";
        log_Label.color = UnityEngine.Color.green;
        if (username.value == "" || username.value.Length > 30)
		{
            log_Label.text = "用户名或者邮箱地址不合法。";
            log_Label.color = UnityEngine.Color.red;
			Common.WARNING_MSG("ui::login: invalid username!");
			return;
		}

        if (password.value.Length < 6 || password.value.Length > 16)
		{
            log_Label.text = "密码不合法, 长度应该在6-16位之间。";
            log_Label.color = UnityEngine.Color.red;
			Common.WARNING_MSG("ui::login: invalid reg_password!");
			return;
		}

        KBEngineApp.app.username = username.value;
        KBEngineApp.app.password = password.value;
		KBEngineApp.app.login_loginapp(true);
        log_Label.text = "连接成功，等待处理请稍后...";
	}
	
	void back()
	{
		if(currstate == "register")
		{
			username.value = reg_username.value;
			NGUITools.SetActive(loginBtn.gameObject, true);
            NGUITools.SetActive(registerBtn.gameObject, true);
            NGUITools.SetActive(username.gameObject, true);
			NGUITools.SetActive(password.gameObject, true);
			NGUITools.SetActive(losepasswordBtn.gameObject, true);

            NGUITools.SetActive(reg_commitBtn.gameObject, false);
            NGUITools.SetActive(reg_username.gameObject, false);
			NGUITools.SetActive(reg_password.gameObject, false);
			NGUITools.SetActive(reg_passwordok.gameObject, false);
            NGUITools.SetActive(password_Label.gameObject, false);
			NGUITools.SetActive(backBtn.gameObject, false);
			
			reg_username.value = "";
            reg_password.value = "";
            reg_passwordok.value = "";
            password.value = "";
		}
		
		currstate = "";
	}
	
	void losepassword()
	{
        if (!KBEngineApp.validEmail(username.value))
		{
            log_Label.color = UnityEngine.Color.red;
            log_Label.text = "请在用户名处输入合法的邮箱地址，系统将发送一份验证邮件帮助您找回密码！";
			return;
		}
		
		KBEngineApp.app.resetpassword_loginapp(true);
	}
	
	void register()
	{
		Common.DEBUG_MSG("register is Click !");
		currstate = "register";
		
		// NGUITools.SetActive(loginpanel.panel.gameObject, false);
        NGUITools.SetActive(loginBtn.gameObject, false);
        NGUITools.SetActive(registerBtn.gameObject, false);
		NGUITools.SetActive(username.gameObject, false);
		NGUITools.SetActive(password.gameObject, false);
        NGUITools.SetActive(losepasswordBtn.gameObject, false);

        NGUITools.SetActive(reg_commitBtn.gameObject, true);
		NGUITools.SetActive(reg_username.gameObject, true);
		NGUITools.SetActive(reg_password.gameObject, true);
		NGUITools.SetActive(reg_passwordok.gameObject, true);
        NGUITools.SetActive(reg_repassword_Label.gameObject, true);
		NGUITools.SetActive(backBtn.gameObject, true);

        backBtn.transform.position = losepasswordBtn.transform.position;
	}
	
	void reg_ok()
	{
		log_Label.text = "请求连接服务器...";
        log_Label.color = UnityEngine.Color.green;

        if (reg_username.value == "" || reg_username.value.Length > 30)
		{
            log_Label.color = UnityEngine.Color.red;
            log_Label.text = "用户名或者邮箱地址不合法, 最大长度限制30个字符。";
			Common.WARNING_MSG("ui::reg_ok: invalid username!");
			return;
		}

        if (reg_password.value.Length < 6 || reg_password.value.Length > 16)
		{
            log_Label.color = UnityEngine.Color.red;
            log_Label.text = "密码不合法, 长度限制在6~16位之间。";
			Common.WARNING_MSG("ui::reg_ok: invalid reg_password!");
			return;
		}

        if (reg_password.value != reg_passwordok.value)
		{
            log_Label.color = UnityEngine.Color.red;
            log_Label.text = "二次输入密码不匹配。";
			Common.WARNING_MSG("ui::reg_ok: reg_password != reg_passwordok!");
			return;
		}
		
		KBEngineApp.app.username = reg_username.value;
		KBEngineApp.app.password = reg_passwordok.value;
		KBEngineApp.app.createAccount_loginapp(true);

        log_Label.text = "连接成功，等待处理请稍后...";
	}
		
	public void onCreateAccountResult(UInt16 retcode, byte[] datas)
	{
        log_Label.text = "";
        log_Label.color = UnityEngine.Color.red;
		
		if(retcode != 0)
		{
            log_Label.text = "服务器返回注册错误:" + KBEngineApp.app.serverErr(retcode) + "!";
			return;
		}

        log_Label.color = UnityEngine.Color.green;
		
		if(KBEngineApp.validEmail(username.value))
		{
            log_Label.text = "注册成功, 请进入邮箱激活账号。";
		}
		else
		{
            log_Label.text = "注册成功, 请点击登录按钮进入游戏！";
		}
		
		back();
	}

	public void onConnectStatus(bool success)
	{
		if(!success)
		{
            log_Label.text = "无法连接服务器。";
            log_Label.color = UnityEngine.Color.red;
		}
	}
	
	public void onLoginFailed(UInt16 failedcode)
	{
        log_Label.color = UnityEngine.Color.red;
        log_Label.text = "登陆服务器失败, 错误:" + KBEngineApp.app.serverErr(failedcode) + "!";
	}
	
	public void onVersionNotMatch(string verInfo, string serVerInfo)
	{
        log_Label.color = UnityEngine.Color.red;
        log_Label.text = "与服务端版本(" + serVerInfo + ")不匹配, 当前版本(" + verInfo + ")!";
	}
	
	public void onScriptVersionNotMatch(string verInfo, string serVerInfo)
	{
        log_Label.color = UnityEngine.Color.red;
        log_Label.text = "与服务端脚本版本(" + serVerInfo + ")不匹配, 当前版本(" + verInfo + ")!";
	}

	public void onLoginGatewayFailed(UInt16 failedcode)
	{
        log_Label.color = UnityEngine.Color.red;
        log_Label.text = "登陆网关服务器失败, 错误:" + KBEngineApp.app.serverErr(failedcode) + "!";
	}
	
	public void login_baseapp()
	{
        log_Label.color = UnityEngine.Color.green;
        log_Label.text = "请求连接到网关服务器...";
	}

	public void onLoginSuccessfully(UInt64 rndUUID, Int32 eid, Account accountEntity)
	{
        log_Label.color = UnityEngine.Color.green;
        log_Label.text = "登陆成功!";
		
		loader.inst.enterScene("selavatar");
	}
	
	public void Loginapp_importClientMessages()
	{
        log_Label.color = UnityEngine.Color.green;
        log_Label.text = "请求建立登录通信协议...";
	}

	public void Baseapp_importClientMessages()
	{
        log_Label.color = UnityEngine.Color.green;
        log_Label.text = "请求建立网关通信协议...";
	}
	
	public void Baseapp_importClientEntityDef()
	{
        log_Label.color = UnityEngine.Color.green;
        log_Label.text = "请求导入脚本...";
	}
}
