
public static class FieldStateHelper
{
    public enum ViewState
    { 
        TopView,
        SideView,
        BackView,

        None
    }

    public static ViewState CollectState(FieldState state)
    {
        ViewState view = ViewState.None;

        switch (state)
        {
            case FieldState.Up: view = ViewState.TopView;
                break;
            case FieldState.Down: view = ViewState.TopView;
                break;
            case FieldState.Right: view = ViewState.SideView;
                break;
            case FieldState.Left: view = ViewState.SideView;
                break;
            case FieldState.Forward: view = ViewState.BackView;
                break;
            case FieldState.Behind: view = ViewState.BackView;
                break;
        }

        return view;
    }
}
