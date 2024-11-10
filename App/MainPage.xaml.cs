using OxyPlot;
using OxyPlot.Series;

namespace App
{
    public partial class MainPage : TabbedPage
    {
        private PlotModel _plotModelA, _plotModelG, _plotModelM;
        private LineSeries _xAxisSeriesA, _yAxisSeriesA, _zAxisSeriesA;
        private LineSeries _xAxisSeriesG, _yAxisSeriesG, _zAxisSeriesG;
        private LineSeries _xAxisSeriesM, _yAxisSeriesM, _zAxisSeriesM;
        private int _dataPointCountA = 0;
        private int _dataPointCountG = 0;
        private int _dataPointCountM = 0;

        public MainPage()
        {
            InitializeComponent();
            InitializePlotA();
            InitializePlotG();
            InitializePlotM();
        }

        private void InitializePlotA()
        {
            _plotModelA = new PlotModel { Title = "Acelerômetro" };

            _xAxisSeriesA = new LineSeries { Title = "Eixo X", Color = OxyColors.Red };
            _yAxisSeriesA = new LineSeries { Title = "Eixo Y", Color = OxyColors.Green };
            _zAxisSeriesA = new LineSeries { Title = "Eixo Z", Color = OxyColors.Blue };

            _plotModelA.Series.Add(_xAxisSeriesA);
            _plotModelA.Series.Add(_yAxisSeriesA);
            _plotModelA.Series.Add(_zAxisSeriesA);

            AccelerometerPlotView.Model = _plotModelA;
        }

        private void InitializePlotG()
        {
            _plotModelG = new PlotModel { Title = "Giroscópio" };

            _xAxisSeriesG = new LineSeries { Title = "Eixo X", Color = OxyColors.Red };
            _yAxisSeriesG = new LineSeries { Title = "Eixo Y", Color = OxyColors.Green };
            _zAxisSeriesG = new LineSeries { Title = "Eixo Z", Color = OxyColors.Blue };

            _plotModelG.Series.Add(_xAxisSeriesG);
            _plotModelG.Series.Add(_yAxisSeriesG);
            _plotModelG.Series.Add(_zAxisSeriesG);

            GyroscopePlotView.Model = _plotModelG;
        }

        private void InitializePlotM()
        {
            _plotModelM = new PlotModel { Title = "Magnetômetro" };

            _xAxisSeriesM = new LineSeries { Title = "Eixo X", Color = OxyColors.Red };
            _yAxisSeriesM = new LineSeries { Title = "Eixo Y", Color = OxyColors.Green };
            _zAxisSeriesM = new LineSeries { Title = "Eixo Z", Color = OxyColors.Blue };

            _plotModelM.Series.Add(_xAxisSeriesM);
            _plotModelM.Series.Add(_yAxisSeriesM);
            _plotModelM.Series.Add(_zAxisSeriesM);

            MagnetometerPlotView.Model = _plotModelM;
        }

        private void UpdatePlotA(double x, double y, double z)
        {
            _xAxisSeriesA.Points.Add(new DataPoint(_dataPointCountA, x));
            _yAxisSeriesA.Points.Add(new DataPoint(_dataPointCountA, y));
            _zAxisSeriesA.Points.Add(new DataPoint(_dataPointCountA, z));

            _dataPointCountA++;
            _plotModelA.InvalidatePlot(true); // Atualiza o gráfico para mostrar os novos dados
        }

        private void UpdatePlotG(double x, double y, double z)
        {
            _xAxisSeriesG.Points.Add(new DataPoint(_dataPointCountG, x));
            _yAxisSeriesG.Points.Add(new DataPoint(_dataPointCountG, y));
            _zAxisSeriesG.Points.Add(new DataPoint(_dataPointCountG, z));

            _dataPointCountG++;
            _plotModelG.InvalidatePlot(true); // Atualiza o gráfico para mostrar os novos dados
        }

        private void UpdatePlotM(double x, double y, double z)
        {
            _xAxisSeriesM.Points.Add(new DataPoint(_dataPointCountM, x));
            _yAxisSeriesM.Points.Add(new DataPoint(_dataPointCountM, y));
            _zAxisSeriesM.Points.Add(new DataPoint(_dataPointCountM, z));

            _dataPointCountM++;
            _plotModelM.InvalidatePlot(true); // Atualiza o gráfico para mostrar os novos dados
        }

        // Evento disparado pelo botão do Acelerômetro
        private void BTNA_Clicked(object sender, EventArgs e)
        {
            if (!Accelerometer.IsMonitoring && Accelerometer.IsSupported)
            {
                Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
                Accelerometer.Start(SensorSpeed.UI);
            }
        }

        // Evento disparado pelo botão do Giroscópio
        private void BTNG_Clicked(object sender, EventArgs e)
        {
            if (!Gyroscope.IsMonitoring && Gyroscope.IsSupported)
            {
                Gyroscope.ReadingChanged += Gyroscope_ReadingChanged;
                Gyroscope.Start(SensorSpeed.UI);
            }
        }

        // Evento disparado pelo botão do Magnetômetro
        private void BTNM_Clicked(object sender, EventArgs e)
        {
            if (!Magnetometer.IsMonitoring && Magnetometer.IsSupported)
            {
                Magnetometer.ReadingChanged += Magnetometer_ReadingChanged;
                Magnetometer.Start(SensorSpeed.UI);
            }
        }

        // Atualiza os dados do acelerômetro na label
        private void Accelerometer_ReadingChanged(object? sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading;
            XAccelLabel.Text = $"X={data.Acceleration.X:F2}";
            YAccelLabel.Text = $"Y={data.Acceleration.Y:F2}";
            ZAccelLabel.Text = $"Z={data.Acceleration.Z:F2}";
            UpdatePlotA(data.Acceleration.X, data.Acceleration.Y, data.Acceleration.Z);
        }

        // Atualiza os dados do giroscópio na label
        private void Gyroscope_ReadingChanged(object? sender, GyroscopeChangedEventArgs e)
        {
            var data = e.Reading;
            XGyroLabel.Text = $"X={data.AngularVelocity.X:F2}";
            YGyroLabel.Text = $"Y={data.AngularVelocity.Y:F2}";
            ZGyroLabel.Text = $"Z={data.AngularVelocity.Z:F2}";
            UpdatePlotG(data.AngularVelocity.X, data.AngularVelocity.Y, data.AngularVelocity.Z);
        }

        // Atualiza os dados do magnetômetro na label
        private void Magnetometer_ReadingChanged(object? sender, MagnetometerChangedEventArgs e)
        {
            var data = e.Reading;
            XMagnetoLabel.Text = $"X={data.MagneticField.X:F2}";
            YMagnetoLabel.Text = $"Y={data.MagneticField.Y:F2}";
            ZMagnetoLabel.Text = $"Z={data.MagneticField.Z:F2}";
            UpdatePlotM(data.MagneticField.X, data.MagneticField.Y, data.MagneticField.Z);
        }

        // Parar os sensores e remover eventos quando a página desaparecer
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (Accelerometer.IsMonitoring)
            {
                Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;
                Accelerometer.Stop();
            }

            if (Gyroscope.IsMonitoring)
            {
                Gyroscope.ReadingChanged -= Gyroscope_ReadingChanged;
                Gyroscope.Stop();
            }

            if (Magnetometer.IsMonitoring)
            {
                Magnetometer.ReadingChanged -= Magnetometer_ReadingChanged;
                Magnetometer.Stop();
            }
        }

        private void BTNO_Clicked(object sender, EventArgs e)
        {
            if (Accelerometer.IsMonitoring)
            {
                Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;
                Accelerometer.Stop();
                XAccelLabel.Text = "X=0";
                YAccelLabel.Text = "Y=0";
                ZAccelLabel.Text = "Z=0";
            }
        }

        private void BTNF_Clicked(object sender, EventArgs e)
        {
            if (Gyroscope.IsMonitoring)
            {
                Gyroscope.ReadingChanged -= Gyroscope_ReadingChanged;
                Gyroscope.Stop();
                XGyroLabel.Text = "X=0";
                YGyroLabel.Text = "Y=0";
                ZGyroLabel.Text = "Z=0";
            }
        }

        private void BTNN_Clicked(object sender, EventArgs e)
        {
            if (Magnetometer.IsMonitoring)
            {
                Magnetometer.ReadingChanged -= Magnetometer_ReadingChanged;
                Magnetometer.Stop();
                XMagnetoLabel.Text = "X=0";
                YMagnetoLabel.Text = "Y=0";
                ZMagnetoLabel.Text = "Z=0";
            }
        }
    }
}