
public static class FieldStateHelper
{
    public enum State
    { 
        TopView,
        SideView,
        BackView,

        None
    }

    public static State CollectState(FieldState state)
    {
        State view = State.None;

        switch (state)
        {
            case FieldState.Up: view = State.TopView;
                break;
            case FieldState.Down: view = State.TopView;
                break;
            case FieldState.Right: view = State.SideView;
                break;
            case FieldState.Left: view = State.SideView;
                break;
            case FieldState.Forward: view = State.BackView;
                break;
            case FieldState.Behind: view = State.BackView;
                break;
        }

        return view;
    }
}
