using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestPanel : BasePanel
{
    public Button exitButton;
    private CanvasGroup m_CanvasGroup;
    private void Start()
    {
        exitButton.onClick.AddListener(exitButtonAction);
    }

    public override void OnEnter()
    {
        Debug.Log("打开测试界面1");
    }

    public override void OnExit()
    {
        Debug.Log("测试界面1退出");
        PoolManager.GetInstance().PushObj("TestPanel", this.gameObject);
    }

    public override void OnPause()
    {
        Debug.Log("测试界面1被覆盖");
        m_CanvasGroup.blocksRaycasts = false;
    }

    public override void OnResume()
    {
        Debug.Log("测试界面1恢复");
        m_CanvasGroup.blocksRaycasts = true;
    }
    private void exitButtonAction()
    {
        UIManager.GetInstance().PopPanel();
    }
}
