using System.Threading;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
   public class Buzzer: IBuzzer
   {
      private IOutput _myOutput;

      public Buzzer(IOutput myOutput)
      {
         _myOutput = myOutput;
      }

      public void TurnOn()
      {
         for (int i = 0; i < 3; i++)
         {
            _myOutput.OutputLine("Buzz");
            Thread.Sleep(500);
         }
      }
   }
}