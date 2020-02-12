
namespace Gui
{


    public interface IWindowManager
    {
        IWindow SetGameScreen(EWindowType windowType, EWindowType prevWindowType = EWindowType.None, params string[] args);

        void GotoPrevWindow();

        EWindowType GetWindowType();

        IWindow GetWindow(EWindowType windowType);
    }

}