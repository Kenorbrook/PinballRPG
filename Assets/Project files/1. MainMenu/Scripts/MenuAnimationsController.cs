using System;
using Michsky.MUIP;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectFiles.MainMenu
{
    public class MenuAnimationsController : MonoBehaviour
    {
        [SerializeField]
        private Animator _menuAnimator;

        [SerializeField]
        private ButtonManager _removeAds;

        private IPurchaseProvider _purchaseProvider;

        private static readonly int setting = Animator.StringToHash("setting");

        private void Awake()
        {
            _purchaseProvider = AllServices.Container.GetSingle<IPurchaseProvider>();
            if (false)
            {
                RemoveAds();
            }
            else
            {
                _removeAds.onClick.AddListener(_purchaseProvider.BuyRemoveAds);
            }
        }

        private void OnEnable()
        {
            _purchaseProvider.AddListenerToRemoveAds(RemoveAds);
        }

        private void OnDisable()
        {
            _purchaseProvider.RemoveListenerToRemoveAds(RemoveAds);
        }

        private void RemoveAds()
        {
            _removeAds.gameObject.SetActive(false);
        }

        public void ChangeSetting()
        {
            _menuAnimator.SetBool(setting, !_menuAnimator.GetBool(setting));
        }
    }
}