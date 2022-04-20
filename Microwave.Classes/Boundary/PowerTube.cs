﻿using System;
using System.Collections.Generic;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
	

	public class PowerTube : IPowerTube
	{

		private IOutput myOutput;

		private bool IsOn = false;

		private int _powerTubeMax;

		public PowerTube(IOutput output, int powerTubeMax)
        {
            myOutput = output;
            _powerTubeMax = powerTubeMax;
        }

        public void TurnOn(int power)
        {
            PowerTubeValueCheck(_powerTubeMax);
            if (power < 1 || _powerTubeMax < power)
            {
                throw new ArgumentOutOfRangeException("power", power, "Must be between 1 and " + _powerTubeMax + " (incl.)");
            }

            if (IsOn)
            {
                throw new ApplicationException("PowerTube.TurnOn: is already on");
            }

            myOutput.OutputLine($"PowerTube works with {power}");
            IsOn = true;
        }

        public void TurnOff()
        {
            if (IsOn)
            {
                myOutput.OutputLine($"PowerTube turned off");
            }

            IsOn = false;
        }

        public void PowerTubeValueCheck(int powerTubeInput)
        {
	        List<int> validPowerTubeConfigs = new List<int>() {500,700,800,1000};

	        if (validPowerTubeConfigs.Contains(powerTubeInput) == false)
	        {
		        throw new ArgumentOutOfRangeException("Max power", powerTubeInput,
			        "must be either 500,700,800 or 1000.");
            }
	        
            

        }

	}
}