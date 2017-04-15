using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedTouch : MonoBehaviour {
    public Animator anim;
    bool toOpen = true;
    private void OnMouseDown()
    {
        Debug.Log("on animatedtouch"+gameObject);
    
        anim.SetBool("toOpen", toOpen);
        toOpen = !toOpen;
    }
}
