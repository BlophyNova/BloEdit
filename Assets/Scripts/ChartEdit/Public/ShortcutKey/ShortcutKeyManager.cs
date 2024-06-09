using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShortcutKeyManager : MonoBehaviourSingleton<ShortcutKeyManager>
{

    public ShortcutKey shortcutKey;//快捷键预制件
    public List<ShortcutKey> shortcutKeys = new();//召唤出来的预制件的列表
    public List<ShortcutKeyEvent> shortcutEventList = new();//事件列表
    public List<ShortcutKeyTable> shortcutKeyMap = new();
    private void Start()
    {
        if (File.Exists($"{Application.dataPath}/ShortcutKeyMap.HuaWaterED"))
        {
            shortcutKeyMap = JsonConvert.DeserializeObject<List<ShortcutKeyTable>>(File.ReadAllText($"{Application.dataPath}/ShortcutKeyMap.HuaWaterED"));
            Debug.Log("快捷键配置文件读取完成");
        }
        else
        {
            Debug.LogError($"没找到配置文件，将使用默认快捷键配置！");
        }
        ReloadShortcutKey();
    }
    //private IEnumerator Start()
    //{
    //    while (true)
    //    {
    //        ReloadShortcutKey();
    //        yield return new WaitForSeconds(5);
    //    }
    //}

    private void ReloadShortcutKey()
    {
        int count = shortcutKeys.Count;
        for (int i = 0; i < count; i++)
        {
            ShortcutKey shortcutKey = shortcutKeys[0];
            shortcutKeys.Remove(shortcutKey);
            Destroy(shortcutKey.gameObject);
        }
        for (int i = 0; i < shortcutKeyMap.Count; i++)
        {
            ShortcutKeyEvent @event = null;
            for (int j = 0; j < shortcutEventList.Count; j++)
            {
                if (shortcutKeyMap[i].eventID == shortcutEventList[j].eventID)
                {
                    @event = shortcutEventList[j];
                    break;
                }
            }
            if (@event == null) continue;
            ShortcutKey instShortcutKey =
                Instantiate(shortcutKey, transform)
                .Init(shortcutKeyMap[i].keyCode, shortcutKeyMap[i].keyCode2, shortcutKeyMap[i].isDoublePress, @event.ExeDown, @event.Exe, @event.ExeUp);
            shortcutKeys.Add(instShortcutKey);
        }
    }
}
[Serializable]
public class ShortcutKeyTable
{
    public KeyCode keyCode;
    public KeyCode keyCode2;
    public bool isDoublePress;
    public string eventID;
}