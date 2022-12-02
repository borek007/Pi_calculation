using Microsoft.VisualBasic;
using Pi_calculation;

var xd =new PiCalc(100000000);
Console.Write(xd.GetPi());

namespace Pi_calculation
{
    using Extreme.Mathematics;
 


    public class PiCalc
    {
        private BigInteger digits;
        private BigInteger binaryDigits;
        private BigFloat correctionDigits;
        private AccuracyGoal goal;
        public PiCalc(long digits=100000000 )
        {goal= AccuracyGoal.Absolute(digits + 5);
            binaryDigits=(int)(8 + digits * Math.Log(10, 2));
            this.digits=digits;
     
        }

        public BigFloat GetPi()
        {   BigFloat pi = BigFloat.Zero;
            BigFloat sqrt2 = BigFloat.Sqrt(2, goal, RoundingMode.TowardsNearest);
            BigFloat y = sqrt2 - BigFloat.One;
            BigFloat a = new BigFloat(6, goal) - BigFloat.ScaleByPowerOfTwo(sqrt2, 2);
            BigFloat y2 = y * y, y3, y4 = y2 * y2;
            BigFloat da;
            correctionDigits = 0;
            for (int k = 1; 4 * correctionDigits < digits; k++)
            {
                BigFloat qrt = BigFloat.Root(1 - y4, 4);
                y = BigFloat.Subtract(1, qrt, goal, RoundingMode.TowardsNearest)
                    / BigFloat.Add(1, qrt, goal, RoundingMode.TowardsNearest);
                // y = BigFloat.Divide(1 - qrt, 1 + qrt, AccuracyGoal.InheritAbsolute, RoundingMode.TowardsNearest);
                y2 = y * y;
                y3 = y * y2;
                y4 = y2 * y2;
                da = (BigFloat.ScaleByPowerOfTwo(y + y3, 2) + (6 * y2 + y4)) * a
                     - BigFloat.ScaleByPowerOfTwo(y + y2 + y3, 2 * k + 1);
                da = da.RestrictPrecision(goal, RoundingMode.TowardsNearest);
                a += da;
                correctionDigits = -da.GetDecimalDigits();
                Console.WriteLine("Iteration {0}: {1:F1} digits", k, correctionDigits);
            }
            pi = BigFloat.Inverse(a);
            return pi;



        }

    
    }
}