using UnityEngine;
using Blophy.Chart;
using Event = Blophy.Chart.Event;
using System.Collections;
using static UnityEngine.Mathf;
using System;
using System.Collections.Generic;
using TMPro;

public class BoxController : MonoBehaviour
{
    public Transform squarePosition;//方框的位置
    public Camera mainCamera;

    public DecideLineController[] decideLineControllers;//所有的判定线控制器
    public SpriteRenderer[] spriteRenderers;//所有的渲染组件
    public ObjectPoolQueue<RippleController> ripples;

    public Box box;//谱面，单独这个box的谱面

    public int sortSeed = 0;//层级顺序种子
    public SpriteMask spriteMask;//遮罩
    public int thisBoxIndex;
    public TextMeshPro boxNumberText;

    public float currentScaleX;
    public float currentScaleY;
    public float currentAlpha;        //默认值
    public float currentCenterX;    //默认值
    public float currentCenterY;    //默认值
    public float currentLineAlpha;    //默认值
    public float currentMoveX;        //默认值
    public float currentMoveY;        //默认值
    public float currentRotate;       //默认值
    public float boxFineness;

    public float default_currentScaleX;
    public float default_currentScaleY;
    public float default_currentAlpha;        //默认值
    public float default_currentCenterX;    //默认值
    public float default_currentCenterY;    //默认值
    public float default_currentLineAlpha;    //默认值
    public float default_currentMoveX;        //默认值
    public float default_currentMoveY;        //默认值
    public float default_currentRotate;       //默认值

    public float last_currentScaleX;
    public float last_currentScaleY;
    public float last_currentAlpha;
    public float last_currentCenterX;
    public float last_currentCenterY;
    public float last_currentLineAlpha;
    public float last_currentMoveX;
    public float last_currentMoveY;
    public float last_currentRotate;
    public float last_boxFineness;

    public int index_currentScaleX;
    public int index_currentScaleY;
    public int index_currentAlpha;
    public int index_currentCenterX;
    public int index_currentCenterY;
    public int index_currentLineAlpha;
    public int index_currentMoveX;
    public int index_currentMoveY;
    public int index_currentRotate;

    public Vector2 raw_center;
    public Vector2 center;
    public Color alpha;
    public Color lineAlpha;
    public Vector2 move;
    public Vector2 scale;
    public Quaternion rotation;
    public Vector2 horizontalFineness;
    public Vector2 verticalFineness;
    /// <summary>
    /// 设置遮罩种子
    /// </summary>
    /// <param name="sortSeed">种子开始</param>
    /// <returns>返回自身</returns>
    public BoxController SetSortSeed(int sortSeed, int thisBoxIndex)
    {
        this.sortSeed = sortSeed;//设置我自己的遮罩到我自己
        spriteMask.frontSortingOrder = sortSeed + ValueManager.Instance.noteRendererOrder - 1;//遮罩种子+一共多少层-1（这个1是我自己占用了，所以减去）
        spriteMask.backSortingOrder = sortSeed - 1;//遮罩的优先级是前包容后不包容，所以后的遮罩层级向下探一个
        for (int i = 0; i < spriteRenderers.Length; i++)//赋值渲染层级到组成渲染的各个组件们
        {
            spriteRenderers[i].sortingOrder = sortSeed;//赋值
        }
        this.thisBoxIndex = thisBoxIndex;
        boxNumberText.text = thisBoxIndex.ToString();
        return this;//返回自己
    }
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="thisBox">这个方框</param>
    /// <returns></returns>
    public BoxController Init(Box thisBox)
    {
        box = thisBox;//赋值thisBox到box
        mainCamera = Camera.main;
        int length_decideLineControllers = decideLineControllers.Length;//获得到当前判定线的数量
        for (int i = 0; i < length_decideLineControllers; i++)//遍历
        {
            decideLineControllers[i].ThisLine = box.lines[i];//将line的源数据赋值过去
        }
        boxFineness = ValueManager.Instance.boxFineness;
        ripples = new(AssetManager.Instance.ripple, 0, squarePosition);
        return this;//返回自身
    }
    public void PlayRipple() => StartCoroutine(Play());
    public IEnumerator Play()
    {
        RippleController ripple = ripples.PrepareObject().Init(currentScaleX, currentScaleY);
        yield return new WaitForSeconds(1.1f);//打击特效时长是0.5秒，0.6秒是为了兼容误差
        ripples.ReturnObject(ripple);
    }
    private void Update()
    {
        UpdateEvents();
    }
    void UpdateEvents()
    {
        float currentTime = (float)ProgressManager.Instance.CurrentTime;

        UpdateCenterAndRotation();
        UpdateAlpha();
        UpdateLineAlpha();
        UpdateMove();
        UpdateScale();
        CalculateAllEventCurrentValue(ref currentTime);
    }

    /// <summary>
    /// 根据谱面数据更新当前所有事件
    /// </summary>
    /// <param name="currentTime">当前时间</param>
    void CalculateAllEventCurrentValue(ref float currentTime)
    {
        last_currentMoveX = currentMoveX;
        last_currentMoveY = currentMoveY;
        last_currentCenterX = currentCenterX;
        last_currentCenterY = currentCenterY;
        last_currentScaleX = currentScaleX;
        last_currentScaleY = currentScaleY;
        last_currentRotate = currentRotate;
        last_currentAlpha = currentAlpha;
        last_currentLineAlpha = currentLineAlpha;
        currentMoveX = CalculateCurrentValue(box.boxEvents.moveX, ref currentTime, ref default_currentMoveX);
        currentMoveY = CalculateCurrentValue(box.boxEvents.moveY, ref currentTime, ref default_currentMoveY);
        currentCenterX = CalculateCurrentValue(box.boxEvents.centerX, ref currentTime, ref default_currentCenterX);
        currentCenterY = CalculateCurrentValue(box.boxEvents.centerY, ref currentTime, ref default_currentCenterY);
        currentRotate = CalculateCurrentValue(box.boxEvents.rotate, ref currentTime, ref default_currentRotate);
        currentAlpha = CalculateCurrentValue(box.boxEvents.alpha, ref currentTime, ref default_currentAlpha);
        currentLineAlpha = CalculateCurrentValue(box.boxEvents.lineAlpha, ref currentTime, ref default_currentLineAlpha);
        currentScaleX = CalculateCurrentValue(box.boxEvents.scaleX, ref currentTime, ref default_currentScaleX);
        currentScaleY = CalculateCurrentValue(box.boxEvents.scaleY, ref currentTime, ref default_currentScaleY);
    }
    /// <summary>
    /// 计算当前数值
    /// </summary>
    /// <param name="events"></param>
    /// <returns></returns>
    public static float CalculateCurrentValue(List<Event> events, ref float currentTime, ref float defaultValue)
    {
        if (events.Count <= 0 || currentTime < events[0].startTime) return defaultValue;
        //int eventIndex = Algorithm.BinarySearch(events, IsCurrentEvent, true, ref currentTime);//找到当前时间下，应该是哪个事件
        int eventIndex = Algorithm.BinarySearch(events, m => ProgressManager.Instance.CurrentTime >= m.startTime, true);//找到当前时间下，应该是哪个事件

        if (currentTime > events[eventIndex].endTime)
            if (events[eventIndex].endValue == 0) return -.0001f;
            else return events[eventIndex].endValue;
        return GameUtility.GetValueWithEvent(events[eventIndex], currentTime);//拿到事件后根据时间Get到当前值
    }
    private void UpdateCenterAndRotation()
    {
        if (last_currentCenterX == currentCenterX && last_currentCenterY == currentCenterY && last_currentRotate == currentRotate) return;
        raw_center.x = currentCenterX;
        raw_center.y = currentCenterY;
        center = mainCamera.ViewportToWorldPoint(raw_center);
        rotation = Quaternion.Euler(Vector3.forward * currentRotate);
        transform.SetPositionAndRotation(center, rotation);
    }

    private void UpdateAlpha()
    {
        if (last_currentAlpha == currentAlpha) return;
        alpha.a = currentAlpha;
        spriteRenderers[0].color =
        spriteRenderers[1].color =
        spriteRenderers[2].color =
        spriteRenderers[3].color = alpha;//1234根线赋值，这里的0，0，0就是黑色的线
    }

    private void UpdateLineAlpha()
    {
        if (last_currentLineAlpha == currentLineAlpha) return;
        lineAlpha.a = currentLineAlpha;
        spriteRenderers[4].color = lineAlpha;
    }

    private void UpdateMove()
    {
        if (last_currentMoveX == currentMoveX && last_currentMoveY == currentMoveY) return;
        move.x = currentMoveX;
        move.y = currentMoveY;
        squarePosition.localPosition = move;
    }

    private void UpdateScale()
    {
        if (last_currentScaleX == currentScaleX && last_currentScaleY == currentScaleY) return;
        scale.x = currentScaleX;
        scale.y = currentScaleY;
        UpdateFineness();
        squarePosition.localScale = scale;
    }
    void UpdateFineness()
    {
        horizontalFineness.x = 2 - (boxFineness / currentScaleX);
        horizontalFineness.y = boxFineness / currentScaleY;
        //缩放图片，保持视觉上相等
        spriteRenderers[0].transform.localScale =//第125根线都是水平的
            spriteRenderers[1].transform.localScale =
            spriteRenderers[4].transform.localScale = horizontalFineness;

        verticalFineness.x = 2 + (boxFineness / currentScaleY);
        verticalFineness.y = boxFineness / currentScaleX;
        spriteRenderers[2].transform.localScale =//第34都是垂直的
            spriteRenderers[3].transform.localScale = verticalFineness;
        //这里的2是初始大小*2得到的结果，初始大小就是Prefabs里的
    }
}
