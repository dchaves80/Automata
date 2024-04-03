using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    private AnimatorStateInfo animStateInfo;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float NTime = animStateInfo.normalizedTime;
        if (NTime>1f) Destroy(transform.parent.gameObject);
       
    }
}
