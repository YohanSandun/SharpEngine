namespace SharpEngine.Engine
{
    class Vector
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public float U { get; set; }
        public float V { get; set; }

        public Vector(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
            U = 0;
            V = 0;
        }
        public Vector(float x, float y, float z, float u, float v)
        {
            X = x;
            Y = y;
            Z = z;
            U = u;
            V = v;
        }
        public static Vector Zero
        {
            get
            {
                return new Vector(0, 0, 0);
            }
        }

        public Vector UV(float u, float v)
        {
            return new Vector(X, Y, Z, u, v);
        }
    }
}
