﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : SingletonMonoBehaviour<EventManager>
{
    [SerializeField,Header("イベントのリスト")]
    private List<EventSystem> m_EventList = new List<EventSystem>();

    [SerializeField, Header("イベントのDictionaly")]
    private Dictionary<string, List<EventSystem>> m_EventDict = new Dictionary<string, List<EventSystem>>();
    private int m_DictCount = 0;//Dictionary用のカウント変数

    [SerializeField, Header("アクティブイベントの数")]
    private int m_ActiveEvent = 0;
    public int ActiveEventNum
    {
        get { return m_ActiveEvent;}
    }
    // Start is called before the first frame update
    void Start()
    {
        m_ActiveEvent = m_EventList.Count;
    }

    // Update is called once per frame
    void Update()
    {
        CheckActiveEvent();//イベントのアクティブをチェック
    }

    private void CheckActiveEvent()
    {
        //アクティブなイベントをチェックして
        //非アクティブなものはリストから削除
        for (int i = 0; i < m_EventList.Count; i++)
        {
            //イベントを一括で検索->1回限定、もしくは普通のイベントのフラグが立っていれば実行
            if (m_EventList[i].NowEventFlag||m_EventList[i].OnceFlag)
            {
                m_EventList[i].Event();
            }
            else
            {
                m_EventList.Remove(m_EventList[i]);
            }
        }
        m_ActiveEvent = m_EventList.Count;//現在のアクティブなイベントの個数をセット
        //foreach (var evsystem in m_EventList)
        //{
        //    if (!evsystem.NowEventFlag)
        //    {
        //        //アクティブじゃなければリストから削除
        //        m_EventList.Remove(evsystem);
        //        continue;
        //    }
        //    else//アクティブならイベントを更新
        //    {
        //        evsystem.Event();
        //    }
        //}
    }

    public void StartEvent(EventSystem eventsystem)//イベントの開始
    {
        eventsystem.NowEventFlag = true;//イベントのフラグを立てる
        m_EventList.Add(eventsystem);//イベントリストにイベントを追加

        //EventSystem[] ev = new EventSystem[1];
        //ev[0] = eventsystem;

        List<EventSystem> addSystemList = new List<EventSystem>();
        addSystemList.Add(eventsystem);

        ////同じkeyがあるかを判定して同じkeyがあれば配列の2盤目以降に追加
        if(m_EventDict.ContainsKey(eventsystem.EventName))
        {
            m_EventDict[eventsystem.EventName].Add(eventsystem);   
        }
        else
        {
            m_EventDict.Add(eventsystem.EventName, addSystemList);
        }
        
    }

    public void StartOnceEvent(EventSystem eventsystem)//1回限定のイベントを開始
    {
        eventsystem.OnceFlag = true;//1回限定用のフラグを立てる
        m_EventList.Add(eventsystem);

        List<EventSystem> addSystemList = new List<EventSystem>();
        addSystemList.Add(eventsystem);
        //同じkeyがあるかを判定して同じkeyがあれば配列の2盤目以降に追加
        if (m_EventDict.ContainsKey(eventsystem.EventName))
        {
            m_EventDict[eventsystem.EventName].Add(eventsystem);
        }
        else
        {
            m_EventDict.Add(eventsystem.EventName, addSystemList);
        }
    }

    public List<EventSystem> GetActiveEvent()//アクティブ状態のイベントを取得
    {
        return m_EventList;
    }
}
