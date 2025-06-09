using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegates_events
{
    public class Button
    {

        public delegate void ClickEventHandler(object sender, EventArgs e);

        public event ClickEventHandler Click;

        protected virtual void OnClick(EventArgs e)
        {
            Click?.Invoke(this, e);
        }

        public void SimulateClick()
        {
            Console.WriteLine("Button clicked!");
            OnClick(EventArgs.Empty);
        }

    }
}
