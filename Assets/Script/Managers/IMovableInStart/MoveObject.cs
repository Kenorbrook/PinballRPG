using UnityEngine;

namespace Script.Managers.IMovableInStart
{
    [RequireComponent(typeof(Animation))]
    public class MoveObject : MovableInStart
    {
        public override void Move()
        {
            var _anim = GetComponent<Animation>();
            _anim.Play();
            Destroy(gameObject, _anim.clip.length);
        }
    }
}
