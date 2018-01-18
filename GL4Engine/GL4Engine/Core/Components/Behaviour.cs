namespace GL4Engine.Core
{
    abstract class Behaviour : Component
    {
        private bool enabled;
        public bool Enabled { get { return enabled; } set { SetEnabled(value); } }

        public Behaviour()
        {
            Enabled = true;
        }

        public Behaviour(bool enabled)
        {
            Enabled = enabled;
        }

        public virtual void OnEnable() { }
        public virtual void OnDisable() { }

        private void SetEnabled(bool value)
        {
            if (enabled == value) return;

            enabled = value;

            if (enabled)
            {
                OnEnable();
            }
            else
            {
                OnDisable();
            }
        }
    }
}
