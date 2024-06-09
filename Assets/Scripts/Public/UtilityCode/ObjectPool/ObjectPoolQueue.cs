using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 使用Queue队列的GameObject对象池 </summary>
[Serializable]
public class ObjectPoolQueue<T> : ObjectPoolBase<T> where T : MonoBehaviour
{
    private readonly Queue<T> _pool;
    public int PoolLength => _pool.Count;
    public ObjectPoolQueue(T @object, int poolLength, int sortSeed, Transform parent = null) : base(@object, poolLength, sortSeed, parent)
    {
        _pool = new Queue<T>();
        for (int i = 0; i < poolLength; i++)
        {
            T obj = CreateNote();
            _pool.Enqueue(obj);
        }
    }
    public ObjectPoolQueue(T @object, int poolLength, Transform parent = null) : base(@object, poolLength, parent)
    {
        _pool = new Queue<T>();
        for (int i = 0; i < poolLength; i++)
        {
            T obj = CreateObject();
            _pool.Enqueue(obj);
        }
    }
    protected override T GetNote() => _pool.Count > 0 ? _pool.Dequeue() : CreateNote(); // 如果池子空了就重新创建物体

    public T PrepareNote() // 取出物体
    {
        T obj = GetNote();
        obj.gameObject.SetActive(true);
        return obj;
    }

    public override void ReturnNote(T obj) // 回收物体
    {
        obj.gameObject.SetActive(false);
        if (obj) _pool.Enqueue(obj);
    }
    protected override T GetObject() => _pool.Count > 0 ? _pool.Dequeue() : CreateObject(); // 如果池子空了就重新创建物体

    public T PrepareObject() // 取出物体
    {
        T obj = GetObject();
        obj.gameObject.SetActive(true);
        return obj;
    }

    public override void ReturnObject(T obj) // 回收物体
    {
        obj.gameObject.SetActive(false);
        if (obj) _pool.Enqueue(obj);
    }
}
