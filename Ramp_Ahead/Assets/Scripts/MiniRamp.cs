using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MiniRamp : SpeedUp
{
    [Header("MATERIAL")]
    public Material miniRampMaterial;
    [Header("MATERIAL OFFSET AND DURATION")]
    public Vector2 initialOffset;
    public Vector2 materialOffset;
    public float duration;
    [Header("FLIP")]
    public Flip flip;
    [Header("RAMP SPEED ADDED")]
    public bool isMiniRampSpeedAdded = false;
    public enum Flip
    {
        None,
        HorizontalRight,
        HorizontalLeft,
        VerticalUp,
        VerticalDown
    }
    private void Start()
    {
        miniRampMaterial.mainTextureOffset = initialOffset;
        AnimateRampTexture();
    }

    private void AnimateRampTexture()
    {
        miniRampMaterial.DOOffset(materialOffset, duration).SetLoops(-1,LoopType.Incremental);
;    }
}
