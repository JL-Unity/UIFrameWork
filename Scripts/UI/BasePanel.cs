using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel:MonoBehaviour
{
    /// <summary>
    /// 开启面板的时候调用
    /// </summary>
    public abstract void OnEnter();

    /// <summary>
    /// 当前面板在栈中而不是在栈顶的时候调用
    /// </summary>
    public abstract void OnPause();

    /// <summary>
    /// 当前面板再次处于栈顶的时候调用
    /// </summary>
    public abstract void OnResume();

    /// <summary>
    /// 关闭当前面板的时候调用
    /// </summary>
    public abstract void OnExit();
}
