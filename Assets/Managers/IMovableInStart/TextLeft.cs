using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animation))]
public class TextLeft : MovebleInStart
{
    public override void Move()
    {
        var anim = GetComponent<Animation>();
        anim.Play();
        Destroy(gameObject, anim.clip.length);

    }
}
