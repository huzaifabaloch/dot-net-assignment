using System;

namespace Problem1
{
    class Program
    {
        public const int hour = 12;
        public const int minute = 59;

        /// <summary>
        /// User should be able to input hours and minutes of the analogue clock.
        /// Program must calculate lesser angle in degrees between hours arrow and minutes arrow and provide
        /// output in the console window.
        /// </summary>
        /// <param name="hour">hours</param>
        /// <param name="minute">minutes</param>
        /// <returns>degree</returns>
        public static double CalculateLesserAngleBetweenHourAndMinuteArrow(int hour, int minute)
        {
            // we have this 12 hour clock where one clock cycle is 360 degree.
            // hour arrow moves 30 degree per hour.....   i.e => 360/12 = 30
            // minute arrow moves 6 degree per minute.... i.e => 360/60 = 6
            // hour arrow moved 0.5 degree per minute.... i.e => 30/60 = 0.5

            // first we calculate the hour angle
            double hourAngle = 0.5 * (hour * 60 + minute);

            // now calculate the minute angle
            double minuteAngle = 6 * minute;

            // difference between hour and minute angle
            double angleDifference = Math.Abs(hourAngle - minuteAngle);

            // if angle is greater than 180 degree, we subtract angle from 360.
            if (angleDifference > 180)
                angleDifference = 360 - angleDifference;

            double degree = angleDifference;

            return degree;
        }

        public static void ClockAngle()
        {
            int hourInput = GetInput("Hour", hour);
            int minuteInput = GetInput("Minute", minute);

            Console.WriteLine(CalculateLesserAngleBetweenHourAndMinuteArrow(hourInput, minuteInput));
        }

        private static int GetInput(string InputFor, int maxValue)
        {
            int result;
            do
            {
                Console.WriteLine(@"Enter {0} [0 - {1}]: ", InputFor, maxValue);
                result = int.Parse(Console.ReadLine());

            } while (result > maxValue || result < 0);

            return result;
        }

        static void Main(string[] args)
        {
            string Continue;
            do
            {
                ClockAngle();
                Console.WriteLine("Do you want to Continue? (Y/N)] : ");
                Continue = Console.ReadLine();

            } while (Continue != "N" && Continue != "n");
        }
    }
}
