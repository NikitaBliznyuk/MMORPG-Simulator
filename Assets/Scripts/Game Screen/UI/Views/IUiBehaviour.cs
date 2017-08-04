using Game.Character;

namespace Game.UI.View
{
    public interface IUiBehaviour
    {
        void UpdateInfo(StatsInfo info);
        void SetActive(bool active);
    }
}
