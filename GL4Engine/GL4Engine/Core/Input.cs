using OpenTK;
using OpenTK.Input;
using System;

namespace GL4Engine.Core
{
    public enum CursorState
    {
        DEFAULT,
        LOCKED
    };

    class Input
    {
        public static CursorState CursorState = CursorState.DEFAULT;
        public static float mouseWheelValue = 0f;

        public static float mouseDeltaX = 0f;
        public static float mouseDeltaY = 0f;

        private static KeyboardState currentKeyState;
        private static KeyboardState previousKeyState;
        private static MouseState currentMouseState;
        private static MouseState previousMouseState;

        public static void UpdateCurrentState()
        {
            currentKeyState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
        }

        public static void UpdatePreviousState()
        {
            previousKeyState = currentKeyState;
            previousMouseState = currentMouseState;
        }

        public static void UpdateMouseDelta()
        {
            mouseDeltaX = previousMouseState.X - currentMouseState.X;
            mouseDeltaY = previousMouseState.Y - currentMouseState.Y;
        }

        public static float GetHorizontalAxisRaw()
        {
            float deltaX = previousMouseState.X - currentMouseState.X;

            if (deltaX < 0) return -1;
            else if (deltaX > 0) return 1;
            else return 0;
        }

        public static float GetVerticalAxisRaw()
        {
            float deltaY = previousMouseState.Y - currentMouseState.Y;

            if (deltaY < 0) return -1;
            else if (deltaY > 0) return 1;
            else return 0;
        }

        public static void UpdateCursorState()
        {
            if (CursorState == CursorState.LOCKED) Mouse.SetPosition(GL4Window.WIDTH / 2, GL4Window.HEIGHT / 2);
        }

        public static bool GetKey(Key key)
        {
            return currentKeyState.IsKeyDown(key);
        }

        public static bool GetKeyDown(Key key)
        {
            return currentKeyState.IsKeyDown(key) && previousKeyState.IsKeyUp(key);
        }

        public static bool GetKeyUp(Key key)
        {
            return currentKeyState.IsKeyUp(key) && previousKeyState.IsKeyDown(key);
        }

        public static bool GetMouse(MouseButton button)
        {
            return currentMouseState.IsButtonDown(button);
        }

        public static bool GetMouseDown(MouseButton button)
        {
            return currentMouseState.IsButtonDown(button) && previousMouseState.IsButtonUp(button);
        }

        public static bool GetMouseUp(MouseButton button)
        {
            return currentMouseState.IsButtonUp(button) && previousMouseState.IsButtonDown(button);
        }

        public static int GetMouseX()
        {
            return currentMouseState.X;
        }

        public static int GetMouseY()
        { 
            return currentMouseState.Y;
        }


    }
}
