using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivasok;

public class Call
{
    public int StartHour { get; set; }
    public int StartMinute { get; set; }
    public int StartSecond { get; set; }
    public int EndHour { get; set; }
    public int EndMinute { get; set; }
    public int EndSecond { get; set; }
    public string PhoneNumber { get; set; }

    public bool IsMobile()
    {
        string prefix = PhoneNumber.Substring(0, 2);
        return prefix == "39" || prefix == "41" || prefix == "71";
    }

    public int GetBilledMinutes()
    {
        int startInSeconds = StartHour * 3600 + StartMinute * 60 + StartSecond;
        int endInSeconds = EndHour * 3600 + EndMinute * 60 + EndSecond;
        int durationInSeconds = endInSeconds - startInSeconds;
        
        int minutes = durationInSeconds / 60;
        if (durationInSeconds % 60 > 0)
        {
            minutes++;
        }
        
        return minutes;
    }

    public bool IsPeakTime()
    {
        int startInSeconds = StartHour * 3600 + StartMinute * 60 + StartSecond;
        int peakStart = 7 * 3600; 
        int peakEnd = 18 * 3600;  
        
        return startInSeconds >= peakStart && startInSeconds < peakEnd;
    }

    public double GetCost()
    {
        int minutes = GetBilledMinutes();
        bool isPeak = IsPeakTime();
        bool isMobile = IsMobile();

        double ratePerMinute;
        
        if (isMobile)
        {
            ratePerMinute = isPeak ? 69.175 : 46.675;
        }
        else
        {
            ratePerMinute = isPeak ? 30 : 15;
        }

        return minutes * ratePerMinute;
    }

    public override string ToString()
    {
        return $"{StartHour} {StartMinute} {StartSecond} {EndHour} {EndMinute} {EndSecond} {PhoneNumber}";
    }
}
