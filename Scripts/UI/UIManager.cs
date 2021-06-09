using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 管理所有UI，由于项目并不是很大，所以现阶段没有使用json存储所有的UI类型,UI的存取通过对象池来获得
/// 在场景切换的时候需要把数据结构清空
/// </summary>
public class UIManager : BaseManager<UIManager>
{
    /// <summary>
    /// 管理当前场景的UI的栈
    /// </summary>
    private Stack<BasePanel> panelStack;
    /// <summary>
    /// 管理状态（面板）的字典
    /// </summary>
    private Dictionary<PanelType, BasePanel> panelDict;
    private Transform canvasTransform;
    private string GetPanelString(PanelType type)
    {
        switch (type)
        {
            case PanelType.StartPanel:
                return "StartPanel";
            case PanelType.TestPanel:
                return "TestPanel";
            case PanelType.TestPanel2:
                return "TestPanel2";
            default:
                Debug.Log($"不存在{type.ToString()}面板");
                break;
        }
        return "\0";
    }
    private Transform CanvasTransform {
        get {
            if(canvasTransform == null)
            {
                canvasTransform = GameObject.Find("Canvas").transform;
            }
            return canvasTransform;
        }
    }

    public GameObject currentPanel;
    public UIManager()
    {
        panelStack = new Stack<BasePanel>();
    }
    private BasePanel GetUI(PanelType panelType)
    {
       if(panelDict == null)
        {
            panelDict = new Dictionary<PanelType, BasePanel>();
        }
        BasePanel panel;
        if(!panelDict.TryGetValue(panelType, out panel))
        {
            GameObject curPanel = PoolManager.GetInstance().GetObject(GetPanelString(panelType), CanvasTransform.position);
            curPanel.transform.SetParent(CanvasTransform);
            panel = curPanel.GetComponent<BasePanel>();
            panelDict.Add(panelType, panel);
        }
        else
        {
            panel = panelDict[panelType];
            GameObject curPanel = PoolManager.GetInstance().GetObject(GetPanelString(panelType), CanvasTransform.position);
        }
        return panel;
    }
    /// <summary>
    /// 推入一个面板
    /// </summary>
    /// <param name="newPanel"></param>
    public void PushPanel(PanelType panelType)
    {
        if(panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }
        if(panelStack.Count > 0)
        {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }
        BasePanel panel = GetUI(panelType);
        panelStack.Push(panel);
        panel.OnEnter();
    }
    /// <summary>
    /// 弹出面板
    /// </summary>
    public void PopPanel()
    {
        if(panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }
        if(panelStack.Count<=0)
        {
            return;
        }
        //退出栈顶面板
        BasePanel topPanel = panelStack.Pop();
        topPanel.OnExit();

        //恢复上一个面板
        if (panelStack.Count > 0)
        {
            BasePanel panel = panelStack.Peek();
            panel.OnResume();
        }
    }
}
 