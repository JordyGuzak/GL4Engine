
using GL4Engine.Core;
using System;


namespace GL4Engine.Core
{
    sealed class EventManager
    {
        private static readonly EventManager instance = new EventManager();

        public static EventManager Instance
        {
            get
            {
                return instance;
            }
        }

        public static event EventHandler<ScriptAddedEventArgs> ScriptAdded;
        public static event EventHandler<ScriptRemovedEventArgs> ScriptRemoved;
        public static event EventHandler<DestroyEventArgs> Destroy;
        public static event EventHandler<LightAddedEventArgs> LightAdded;
        public static event EventHandler<LightRemovedEventArgs> LightRemoved;

        public void OnScriptAdded(object sender, ScriptAddedEventArgs e)
        {
            ScriptAdded?.Invoke(sender, e);
        }

        public void OnScriptRemoved(object sender, ScriptRemovedEventArgs e)
        {
            ScriptRemoved?.Invoke(sender, e);
        }

        public void OnDestroy(object sender, DestroyEventArgs e)
        {
            Destroy?.Invoke(sender, e);
        }

        public void OnLightAdded(object sender, LightAddedEventArgs e)
        {
            LightAdded?.Invoke(sender, e);
        }

        public void OnLightRemoved(object sender, LightRemovedEventArgs e)
        {
            LightRemoved?.Invoke(sender, e);
        }

    }

    class ScriptAddedEventArgs : EventArgs
    {
        public Script Script { get; set; }
    }

    class ScriptRemovedEventArgs : EventArgs
    {
        public Script Script { get; set; }
    }

    class DestroyEventArgs : EventArgs
    {
        public Object Object { get; set; }
    }

    class LightAddedEventArgs : EventArgs
    {
        public Light Light { get; set; }
    }

    class LightRemovedEventArgs : EventArgs
    {
        public Light Light { get; set; }
    }
}
