using System;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class PurchaseProvider : IPurchaseProvider, IDetailedStoreListener
{
    private static UnityEvent purchased;

    private static IStoreController StoreController { get; set; }

    private static IExtensionProvider storeExtensionProvider;
    
    private const string REMOVE_ADS = "remove_ads";

    private Action _removeAdsAction;
    public PurchaseProvider()
    {
        IAPInitialization();
    }

    

    public void CheckPurchases()
    {
        if (!IsIAPInitialized()) return;
        if (CheckPurchase(REMOVE_ADS))
            RemoveAds();
    }

    public async void IAPInitialization()
    {
        await UnityServices.InitializeAsync();
        Debug.Log("Begin init IAP");
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(REMOVE_ADS, ProductType.Consumable);
        Debug.Log("Builder added products"); 
        UnityPurchasing.Initialize(this, builder);
        Debug.Log("UnityPurchasing successful Initialize");
    }

    public bool IsIAPInitialized()
    {
        return StoreController != null && storeExtensionProvider != null;
    }

    public bool CheckPurchase(string productId)
    {
        Product product = StoreController.products.WithID(productId);
        return product.hasReceipt;
    }

    public void AddListenerToRemoveAds(Action action)
    {
        _removeAdsAction += action;
    }

    public void RemoveListenerToRemoveAds(Action action)
    {
        _removeAdsAction -= action;
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        StoreController = controller;
        storeExtensionProvider = extensions;
        CheckPurchases();
    }

    

    public void BuyProductID(string productId, UnityEvent action)
    {
        Debug.Log("Try to buy: " + productId);

        if (!IsIAPInitialized()) return;
        purchased = action;
        Product product = StoreController.products.WithID(productId);
        if (product is {availableToPurchase: true})
        {
            StoreController.InitiatePurchase(product);
        }
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        purchased?.Invoke();
        purchased = null;
        return PurchaseProcessingResult.Complete;
    }
    
    public void BuyRemoveAds()
    {
        UnityEvent _event = new UnityEvent();
        _event.AddListener(RemoveAds);
        BuyProductID(REMOVE_ADS, _event);
    }
    private void RemoveAds()
    {
        _removeAdsAction.Invoke();
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("Initialization IAP fallen with error: "+ error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        
        Debug.Log("Initialization IAP fallen with message: "+ message);
    }
    void IPurchaseProvider.OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("Initialization IAP fallen with error: " + failureReason);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        Debug.Log("Initialization IAP fallen with error: "+ failureDescription);
    }
    
    void IStoreListener.OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("Initialization IAP fallen with error: "+ failureReason);
    }
    
}