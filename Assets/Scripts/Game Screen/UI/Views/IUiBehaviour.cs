using GameScreen.Character;

namespace GameScreen.UI.View
{
    public interface IUiBehaviour
    {
        void UpdateInfo(StatsInfo info);
        void SetActive(bool active);
    }
}
