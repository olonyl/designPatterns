using System;
using System.ComponentModel;

namespace WeakEventPattern
{
    public class Button
    {
        public event EventHandler Clicked;

        public void Fire()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }
    public class Window
    {   public Window(Button button)
        {
            //WeakEventManager<Button, EventArgs>
            //       .AddHandler(button, "Clicked", ButtonOnClicked);
            button.Clicked += ButtonOnClicked;
        }

        private void ButtonOnClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Button clicked (Window handler)");
        }


        ~Window() {
            Console.WriteLine("Window finalized");
        }
     
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var btn = new Button();
            var window = new Window(btn);
            btn.Fire();

            Console.WriteLine("Setting window to null");
            window = null;

            FireGC();

        }

        private static void FireGC()
        {
            Console.WriteLine("Starting GC");

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Console.WriteLine("GC is Done");
        }

    }
}
