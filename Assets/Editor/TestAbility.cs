using System;
using System.Collections;
using Game.Character;

namespace TestNamespace
{
    [Serializable]
    public class TestAbility : IAbility, IHit
    {
        public AbilityInfo abilityInfo;
        public HitInfo hitInfo;

        public TestAbility(AbilityInfo abilityInfo, HitInfo hitInfo)
        {
            this.abilityInfo = abilityInfo;
            this.hitInfo = hitInfo;
            ClassName = GetType().FullName;
        }

        public string ClassName { get; private set; }

        public AbilityInfo AbilityInfo
        {
            get { return abilityInfo; }
        }

        public HitInfo HitInfo
        {
            get { return hitInfo; }
        }

        public void Invoke(CharacterInfoController invoker, CharacterInfoController target)
        {
            throw new NotImplementedException();
        }

        public IEnumerator Cooldown(float time)
        {
            throw new NotImplementedException();
        }

    }
}
