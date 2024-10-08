using UnityEngine.EventSystems;

namespace HT
{
    public interface ICommandable : IPointerClickHandler
    {
        void OnSelect() { }
        void OnDeSelect() { }
        void OnExecuteCommandable(AnimalAction command) { }
    }

    public interface IClickable
    {
        void OnClick() { }
    }

    public interface IHoverable : IPointerEnterHandler, IPointerExitHandler
    {
        void OnHighlight() { }

        void OnDeHightlight() { }
    }
}
