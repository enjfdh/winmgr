using System;

namespace WinMgr
{
    public class WorkArea : IWorkAreaOrganiser
    {
        private IWindowController _controller;
        private IScreen _screen;
        private WorkAreaState _currentState;

        public WorkArea(IWindowController controller, IScreen screen)
        {
            _controller = controller;
            _screen = screen;
            _currentState = new EqualState(_screen.Width, _screen.Height);
        }

        public IWindow LeftWindow
        {
            get; private set;
        }

        public IWindow RightWindow
        {
            get; private set;
        }

        public void Activate()
        {
            if (LeftWindow != null) _controller.ShowWindow(LeftWindow.Pointer);
            if (RightWindow != null) _controller.ShowWindow(RightWindow.Pointer);
        }

        public void Deactivate()
        {
            if (LeftWindow != null) _controller.MinimiseWindow(LeftWindow.Pointer);
            if (RightWindow != null) _controller.MinimiseWindow(RightWindow.Pointer);
        }

        public void Left()
        {
            _currentState = _currentState.Left();
            AlignToCurrentState();
        }

        public void Right()
        {
            _currentState = _currentState.Right();
            AlignToCurrentState();
        }

        private void AlignToCurrentState()
        {
            if (LeftWindow != null)
            {
                _controller.SetWindowLocation(LeftWindow.Pointer,
                    _currentState.XLocation1,
                    _currentState.YLocation,
                    _currentState.Width1,
                    _currentState.Height);
            }

            if (RightWindow != null)
            {
                _controller.SetWindowLocation(RightWindow.Pointer,
                    _currentState.XLocation2,
                    _currentState.YLocation,
                    _currentState.Width2,
                    _currentState.Height);
            }
        }

        public void SetLeftWindow(IWindow window)
        {
            LeftWindow = window;
            AlignToCurrentState();
        }

        public void SetRightWindow(IWindow window)
        {
            RightWindow = window;
            AlignToCurrentState();
        }

        #region Work Area States

        public abstract class WorkAreaState
        {
            public WorkAreaState(WorkAreaState prior)
            {
                TotalWidth = prior.TotalWidth;
                Height = prior.Height;
            }

            public WorkAreaState(int width, int height)
            {
                TotalWidth = width;
                Height = height;
            }

            public int XLocation1 { get { return 0; } }
            public int TotalWidth { get; private set; }
            public abstract int Width1 { get; }

            public int XLocation2 { get { return Width1; } }
            public int Width2 { get { return TotalWidth - Width1; } }

            public int Height { get; protected set; }
            public int YLocation { get { return 0; } }

            public abstract WorkAreaState Right();
            public abstract WorkAreaState Left();
            
            protected int Third() { return TotalWidth / 3; }
            protected int TwoThirds() { return TotalWidth - Third(); }
            protected int Half() { return TotalWidth / 2; }
        }

        public class EqualState : WorkAreaState
        {
            public EqualState(WorkAreaState prior) : base(prior) { }

            public EqualState(int width, int height) : base(width, height) { }

            public override int Width1
            {
                get
                {
                    return Half();
                }
            }

            public override WorkAreaState Left()
            {
                return new RightEmphasisedState(this);
            }

            public override WorkAreaState Right()
            {
                return new LeftEmphasisedState(this);
            }
        }

        public class RightEmphasisedState : WorkAreaState
        {
            public RightEmphasisedState(WorkAreaState prior) : base(prior) { }

            public override int Width1
            {
                get
                {
                    return Third();
                }
            }

            public override WorkAreaState Left()
            {
                return this;
            }

            public override WorkAreaState Right()
            {
                return new EqualState(this);
            }
        }

        public class LeftEmphasisedState : WorkAreaState
        {
            public LeftEmphasisedState(WorkAreaState prior) : base(prior) { }

            public override int Width1
            {
                get
                {
                    return TwoThirds();
                }
            }

            public override WorkAreaState Left()
            {
                return new EqualState(this);
            }

            public override WorkAreaState Right()
            {
                return this;
            }
        }

        #endregion
    }
}