namespace WinFormsAppsolar
{
    public partial class Form1 : Form
    {

        private List<CelestialBody> celestialBodies;
        private System.Windows.Forms.Timer animationTimer;
        private int animationSpeed = 1;

        public Form1()
        {
            InitializeComponent();
            InitializeSolarSystem();
            InitializeAnimationTimer();
        }

        private void InitializeAnimationTimer()
        {
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 50; 
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }

        private void InitializeSolarSystem()
        {
            celestialBodies = new List<CelestialBody>
            {
                new CelestialBody("Sun", 50, Color.Yellow, 0, 0, 0),
                new CelestialBody("Mercury", 10, Color.Gray, 100, 0, 2),
                new CelestialBody("Venus", 15, Color.Orange, 150, 0, 1),
                new CelestialBody("Earth", 15, Color.Blue, 200, 0, 0.5),
                new CelestialBody("Mars", 12, Color.Red, 300, 0, 0.2),
            };
        }
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            foreach (var body in celestialBodies)
            {
                body.UpdatePosition(animationSpeed);
            }

            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            foreach (var body in celestialBodies)
            {
                body.Draw(e.Graphics);
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                animationSpeed++;
            else if (e.KeyCode == Keys.Down && animationSpeed > 1)
                animationSpeed--;

            
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (InvokeRequired)
            {

                Invoke(new Action(UpdateUI));
                return;
            }


        }


        public class CelestialBody
        {
            public string Name { get; }
            public int Radius { get; }
            public Color Color { get; }
            public double DistanceFromSun { get; }
            public double Angle { get; set; }
            public double AngularSpeed { get; }
            public Image PlanetImage { get; }
            public CelestialBody(string name, int radius, Color color, double distance, double angle, double angularSpeed)
            {
                Name = name;
                Radius = radius;
                Color = color;
                DistanceFromSun = distance;
                Angle = angle;
                AngularSpeed = angularSpeed;


            }


            public void UpdatePosition(int speed)
            {

                Angle += AngularSpeed * speed;
            }

            public void Draw(Graphics graphics)
            {
                int x = (int)(DistanceFromSun * Math.Cos(Angle));
                int y = (int)(DistanceFromSun * Math.Sin(Angle));


                graphics.FillEllipse(new SolidBrush(Color), x - Radius, y - Radius, 2 * Radius, 2 * Radius);
                if (PlanetImage != null)
                {
                    graphics.DrawImage(PlanetImage, x - Radius, y - Radius, 2 * Radius, 2 * Radius);
                }

                graphics.DrawEllipse(Pens.Gray, 0, 0, (int)(2 * DistanceFromSun), (int)(2 * DistanceFromSun));
            }
        }

    }
}







    
