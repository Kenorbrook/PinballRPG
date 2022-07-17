namespace Script
{
   public interface IBoss
   {
      public float GetDamage(int hp, int maxHp, float currentScale);
      public void StartAnim();
      public void EndAnim();
   }
}
