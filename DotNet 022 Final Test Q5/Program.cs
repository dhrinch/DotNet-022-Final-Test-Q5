namespace DotNet_022_Final_Test_Q5
{    
    public class TemperatureChangedEventArgs : EventArgs
    {
        public double NewTemperature { get; }

        public TemperatureChangedEventArgs(double newTemperature)
        {
            NewTemperature = newTemperature;
        }
    }
 
    public delegate void TemperatureChangedEventHandler(object sender, TemperatureChangedEventArgs args);
    
    public class TemperatureSensor
    {
        public event TemperatureChangedEventHandler TemperatureChanged;

        private double currentTemperature;

        public double CurrentTemperature
        {
            get { return currentTemperature; }
            set
            {
                if (value != currentTemperature)
                {
                    currentTemperature = value;
                    OnTemperatureChanged(new TemperatureChangedEventArgs(value));
                }
            }
        }

        protected virtual void OnTemperatureChanged(TemperatureChangedEventArgs e)
        {
            TemperatureChanged?.Invoke(this, e);
        }
    }
    
    public class TemperatureMonitor
    {
        public TemperatureMonitor(TemperatureSensor sensor)
        {
            sensor.TemperatureChanged += Sensor_TemperatureChanged;
        }

        private void Sensor_TemperatureChanged(object sender, TemperatureChangedEventArgs args)
        {
            Console.WriteLine($"Temperature changed: {args.NewTemperature}");     
        }
    }
    
    class Program
    {
        static void Main()
        {
            TemperatureSensor sensor = new TemperatureSensor();
            TemperatureMonitor monitor = new TemperatureMonitor(sensor);
            
            sensor.CurrentTemperature = 25.5;            
        }
    }
}
