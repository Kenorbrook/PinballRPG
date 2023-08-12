using UnityEngine.Purchasing;

public interface IPurchaseProvider : IService
{
    void AddListenerToRemoveAds(System.Action action);
    void RemoveListenerToRemoveAds(System.Action action);
    void CheckPurchases();
    public void IAPInitialization();
    bool IsIAPInitialized();
    bool CheckPurchase(string productId);
    void OnInitialized(IStoreController controller, IExtensionProvider extensions);
    void BuyProductID(string productId, UnityEngine.Events.UnityEvent action);
    PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent);
    void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason);
    void BuyRemoveAds();
}