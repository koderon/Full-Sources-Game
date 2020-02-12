
namespace Gui
{
    
    public interface IWindow
    {
        void Setup(IWindowManager windowManager, params string[] args);

        EWindowType GetWindowType();

        void SetPrevWindow(EWindowType window);

        void SetBackground(string background);

        EWindowType GetPrevWindow();

        void Show(bool active = true);
    }

}