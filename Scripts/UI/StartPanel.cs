using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartPanel : BasePanel
{
    public Button testButton1;
    public Button testButton2;
    private CanvasGroup m_CanvasGroup;
    private void Start()
    {
        testButton1.onClick.AddListener(test1ButtonEvent);
        testButton2.onClick.AddListener(test2ButtonEvent);
        m_CanvasGroup = gameObject.GetComponent<CanvasGroup>();
    }
    public override void OnEnter()
    {
        Debug.Log("进入开始面板");
    }
    private void test1ButtonEvent()
    {
        UIManager.GetInstance().PushPanel(PanelType.TestPanel);
    }
    private void test2ButtonEvent()
    {
        UIManager.GetInstance().PushPanel(PanelType.TestPanel2);
    }
    public override void OnExit()
    {}

    public override void OnPause()
    {
        Debug.Log("开始界面被覆盖");
        m_CanvasGroup.blocksRaycasts = false;
    }

    public override void OnResume()
    {
        m_CanvasGroup.blocksRaycasts = true;
    }
}
