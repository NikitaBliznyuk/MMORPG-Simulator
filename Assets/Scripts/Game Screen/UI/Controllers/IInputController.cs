using GameScreen.Character;

namespace GameScreen.UI.Controllers
{
    public interface IInputController
    {
        CharacterInfoController CurrentObservableInfo { get; set; }
    }
}
