using System;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class CookControllerTest
    {
        private CookController uut;

        private IUserInterface ui;
        private ITimer timer;
        private IDisplay display;
        private IPowerTube powerTube;

        [SetUp]
        public void Setup()
        {
            ui = Substitute.For<IUserInterface>();
            timer = Substitute.For<ITimer>();
            display = Substitute.For<IDisplay>();
            powerTube = Substitute.For<IPowerTube>();

            uut = new CookController(timer, display, powerTube, ui);
        }

        [Test]
        public void IncreaseTimeCalled()
        {
            uut.StartCooking(50, 60);

            uut.IncreaseTime();

            timer.Received(1).IncreaseTime();
        }

        [Test]
        public void DecreaseTimeCalled()
        {
            uut.StartCooking(50, 60);

            uut.DecreaseTime();

            timer.Received(1).DecreaseTime();
        }

        [Test]
        public void StartCooking_ValidParameters_TimerStarted()
        {
            uut.StartCooking(50, 60);

            timer.Received().Start(60);
        }

        [Test]
        public void StartCooking_ValidParameters_PowerTubeStarted()
        {
            uut.StartCooking(50, 60);

            powerTube.Received().TurnOn(50);
        }

        [TestCase(115,1,55)]
        [TestCase(0, 0, 0)]
        [TestCase(-10, 0, 0)]
        [TestCase(150, 2, 30)]
        public void Cooking_TimerTick_DisplayCalled(int remaining, int min, int sec)
        {
            uut.StartCooking(50, 60);

            timer.TimeRemaining.Returns(remaining);
            timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            display.Received().ShowTime(min, sec);
        }

        [Test]
        public void Cooking_TimerExpired_PowerTubeOff()
        {
            uut.StartCooking(50, 60);

            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            powerTube.Received().TurnOff();
        }

        [Test]
        public void Cooking_TimerExpired_UICalled()
        {
            uut.StartCooking(50, 60);

            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            ui.Received().CookingIsDone();
        }

        [Test]
        public void Cooking_Stop_PowerTubeOff()
        {
            uut.StartCooking(50, 60);
            uut.Stop();

            powerTube.Received().TurnOff();
        }

    }
}