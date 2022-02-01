namespace SharpEngine.Engine
{
    class Vector
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector Zero
        {
            get
            {
                return new Vector(0, 0, 0);
            }
        }
    }
}
