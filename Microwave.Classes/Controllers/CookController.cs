using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Controllers
{
    public class CookController : ICookController
    {
        // Since there is a 2-way association, this cannot be set until the UI object has been created
        // It also demonstrates property dependency injection
        public IUserInterface UI { set; private get; }

        private bool isCooking = false;

        private IDisplay myDisplay;
        private IPowerTube myPowerTube;
        private ITimer myTimer;

        public CookController(
            ITimer timer,
            IDisplay display,
            IPowerTube powerTube,
            IUserInterface ui) : this(timer, display, powerTube)
        {
            UI = ui;
        }

        public CookController(
            ITimer timer,
            IDisplay display,
            IPowerTube powerTube)
        {
            myTimer = timer;
            myDisplay = display;
            myPowerTube = powerTube;

            timer.Expired += new EventHandler(OnTimerExpired);
            timer.TimerTick += new EventHandler(OnTimerTick);
        }

        public void StartCooking(int power, int time)
        {
            myPowerTube.TurnOn(power);
            myTimer.Start(time);
            isCooking = true;
        }

        public void IncreaseTime()
        {
            if (isCooking)
            {
                myTimer.IncreaseTime();
            }
        }

        public void DecreaseTime()
        {
            if (isCooking)
            {
                myTimer.DecreaseTime();
            }
        }

        public void Stop()
        {
            isCooking = false;
            myPowerTube.TurnOff();
            myTimer.Stop();
        }

        public void OnTimerExpired(object sender, EventArgs e)
        {
            if (isCooking)
            {
                isCooking = false;
                myPowerTube.TurnOff();
                UI.CookingIsDone();
            }
        }

        public void OnTimerTick(object sender, EventArgs e)
        {
            if (isCooking)
            {
                int remaining = myTimer.TimeRemaining;
                if (((remaining / 60) <= 0) && ((remaining % 60) <= 0))
                {
                    myDisplay.ShowTime(0, 0);
                }
                else
                {
                    myDisplay.ShowTime(remaining / 60, remaining % 60);

                }
            }
        }
    }
}