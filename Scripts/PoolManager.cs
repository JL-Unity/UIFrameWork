using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 对象池管理器，可以调用其中的函数获取对象
/// list存储游戏物体，Dictionary制作抽屉
/// </summary>
public class PoolManager : BaseManager<PoolManager> {
    public Dictionary<string, List<GameObject>> poolDic = new Dictionary<string, List<GameObject>>();
    /// <summary>
    /// 从池子里面取得物品
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetObject(string name, Vector3 location) 
    {
        GameObject obj = null;
        if(poolDic.ContainsKey(name) && poolDic[name].Count > 0)
        {
            //Debug.Log("Find item" + name);
            obj = poolDic[name][0];
            obj.transform.position = location;
            poolDic[name].RemoveAt(0);
        }
        else
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>(name), location, Quaternion.identity);
            //Debug.Log(name);
        }
        obj.SetActive(true);
        return obj;
    }
    public void PushObj(string name, GameObject obj)
    {
        obj.SetActive(false);
        //已经拥有抽屉
        if(poolDic.ContainsKey(name))
        {
            poolDic[name].Add(obj);
        }
        //里面没有抽屉
        else
        {
            poolDic.Add(name, new List<GameObject>() { obj });
        }
    }
    /// <summary>
    /// 场景切换时调用
    /// </summary>
    public void Clear()
    {
        poolDic.Clear();
    }
}
