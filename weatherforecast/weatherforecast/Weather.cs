using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherforecast;

public class Weather
{
    public string CityId { get; set; }

    public string Time { get; set; }

    public string Direction { get; set; }

    public string Speed { get; set; }

    public int Temperature { get; set; }
}
