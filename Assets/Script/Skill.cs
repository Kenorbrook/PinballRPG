using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Create Skill", fileName = "Skill", order = 0)]
public class Skill: ActiveSkill
{
    
    public override async Task UseSkill()
    {
        effect.Invoke();
        await Task.Delay((int)(duration[currentLevel]*1000));
        DisableSkill();
    }

    public override void DisableSkill()
    {
        disableEffect.Invoke();
    }
}