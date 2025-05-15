using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFloat : TriBehaviour
{
    private TextMeshPro text;
    protected override void Start()
    {
        base.Start();
        text = GetComponent<TextMeshPro>(); 
    }
    public override void OnEnable()
    {
        base.OnEnable();
        text.alpha = 1f;
    }
}
