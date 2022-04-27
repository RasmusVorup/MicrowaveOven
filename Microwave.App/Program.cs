using System;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;

namespace Microwave.App
{
    class Program
    {
        static void Main(string[] args)
        {
            int powerTubeConfig = 700;
            Button startCancelButton = new Button();
            Button powerButton = new Button();
            Button timeButton = new Button();
            Button decreaseTimeButton = new Button();


            Door door = new Door();

            Output output = new Output();

            Display display = new Display(output);

            PowerTube powerTube = new PowerTube(output);

            Light light = new Light(output);

            Microwave.Classes.Boundary.Timer timer = new Timer();

            CookController cooker = new CookController(timer, display, powerTube);

            Buzzer buzzer = new Buzzer(output);

            UserInterface ui = new UserInterface(powerButton, timeButton, decreaseTimeButton,startCancelButton, door, display, light, cooker, buzzer);

            // Finish the double association
            cooker.UI = ui;

            // Simulate a simple sequence

            powerButton.Press();

            timeButton.Press();

            startCancelButton.Press();

            // The simple sequence should now run

            //System.Console.WriteLine("When you press enter, the program will stop");
            // Wait for input

            //System.Console.ReadLine();

            System.Console.WriteLine("When you press 'x', the program will stop\nWhen you press 'i', the time will increase by 30 sec,\nWhen you press 'd', the time will decrease by 30 sec");
            var cont = true;
            while (cont)
            {
                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case 'x':
                    case 'X':
                        cont = false;
                        break;
                    case 'i':
                    case 'I':
                        timeButton.Press();
                        break;
                    case 'd':
                    case 'D':
                        decreaseTimeButton.Press();
                        break;

                }
            }
        }
    }
}
