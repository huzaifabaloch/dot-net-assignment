## Problem 1

Write console app. User should be able to input hours and minutes of the analogue clock. 
Program must calculate lesser angle in degrees between hours arrow and minutes arrow and provide 
output in the console window.


### Problem Solution
### =================
#### Important Points
> We have this 12 hour clock where one clock cycle is 360 degree.
> Hour arrow moves 30 degree per hour.....   i.e => 360/12 = 30
> Minute arrow moves 6 degree per minute.... i.e => 360/60 = 6
> Hour arrow moved 0.5 degree per minute.... i.e => 30/60 = 0.5

#### We calculate hour angle first from above calculation and then minute angle, we then subtract minute angle from hour angle

			hourAngle - minuteAngle



