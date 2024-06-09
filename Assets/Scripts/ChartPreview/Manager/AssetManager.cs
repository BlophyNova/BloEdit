using UnityEngine;
using Blophy.Chart;
using System.Collections.Generic;
using UnityEngine.UI;

public class AssetManager : MonoBehaviourSingleton<AssetManager>
{

    [Header("音乐播放")]
    public AudioSource musicPlayer;

    [Header("方框以及他们的爹爹~")]
    public Transform box;
    public BoxController boxController;

    [Header("音符萌~")]
    public NoteController[] noteControllers;

    [Header("打击特效的预制件")]
    public HitEffectController hitEffect;

    [Header("方框波纹特效预制件")]
    public RippleController ripple;

    [Header("打击音效预制件")]
    public HitSoundController hitSoundController;

    [Header("背景")]
    public Image background;
}
