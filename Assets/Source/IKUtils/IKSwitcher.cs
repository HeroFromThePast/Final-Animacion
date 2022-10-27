using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IKSwitcher : MonoBehaviour
{
    [SerializeField][Range(0,1)] private float weigth;
    [SerializeField] private MultiParentConstraint[] targetChain;

    public void SetConstraintWeigth()
    {
        if (targetChain == null || targetChain.Length < 0) return;
        for(int i= 0; i < targetChain.Length; i++)
        {
            targetChain[i].weight = weigth;
        }
    }

    private void OnValidate()
    {
        SetConstraintWeigth();
    }
}
