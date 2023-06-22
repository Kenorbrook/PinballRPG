using UnityEngine;

namespace ProjectFiles.MainMenu
{
    public class MenuAnimationsController : MonoBehaviour
    {
        [SerializeField]
        private Animator _menuAnimator;

        private static readonly int setting = Animator.StringToHash("setting");


        public void ChangeSetting()
        {
            _menuAnimator.SetBool(setting, !_menuAnimator.GetBool(setting));
        }
    }
}
