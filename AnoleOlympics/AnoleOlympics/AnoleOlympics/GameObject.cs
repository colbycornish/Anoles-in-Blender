using System;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnoleOlympics
{
    public class GameObject
    {
        private Model model;
        private string name;
        private Vector3 position;
        private Vector3 rotation;
        private Vector3 scale;
        private Color color;
        private BoundingSphere bounds;

        public GameObject() 
        {
            this.scale = new Vector3(1, 1, 1);
            this.model = null;
            this.name = "";
            this.position = Vector3.Zero;
            this.rotation = Vector3.Zero;
            this.color = Color.Gray;
            this.bounds = new BoundingSphere();
        }

        public GameObject Clone()
        {
            GameObject go = new GameObject();
            go.Model = Model;
            go.Position = Position;
            go.rotation = Rotation;
            go.Scale = Scale;
            go.Color = Color;
            go.Name = Name;
            return go;
        }

        public void Draw(Matrix view, Matrix projection)
        {
//            if (frustum.Intersects(bounds))
            {
                Matrix world =
                    Matrix.CreateScale(scale) *
                    Matrix.CreateFromYawPitchRoll(rotation.Y, rotation.Z, rotation.X) *
                    Matrix.CreateTranslation(position);

                {
                    foreach (ModelMesh mesh in Model.Meshes)
                    {
                        foreach (BasicEffect effect in mesh.Effects)
                        {
                            effect.EnableDefaultLighting();
                            effect.DirectionalLight2.DiffuseColor = Color.White.ToVector3();
                            effect.PreferPerPixelLighting = true;
                            effect.World = world;
                            effect.Projection = projection;
                            effect.View = view;
                        }
                        mesh.Draw();
                    }
                }
            }
        }

        public bool Intersects(BoundingFrustum frustrum)
        {
            return (frustrum.Intersects(bounds));
        }

        public Model Model
        {
            get { return this.model; }
            set { this.model = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public Vector3 Position
        {
            get { return this.position; }
            set 
            { 
                this.position = value;
                this.bounds = new BoundingSphere(position, 30);
            }
        }

        public Vector3 Rotation
        {
            get { return this.rotation; }
            set { this.rotation = value; }
        }
        public void RotateY(float theta)
        {
            this.rotation.Y += theta;
        }
        public void RotateZ(float beta)
        {     
            this.rotation.Z += beta;
        }
        public void RotateX(float alpha)
        {
            this.rotation.X += alpha;
        }


        public float ScaleFactor
        {
            get { return this.scale.X; }
            set { this.scale = new Vector3(value, value, value); }
        }

        public Vector3 Scale
        {
            get { return this.scale; }
            set { this.scale = value; }
        }

        public Color Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public void ToXML(StreamWriter xml)
        {
            xml.WriteLine("   <building " +
                "model='" + Name + "' " +
                "position='" + Vector3ToString(Position) + "' " +
                "rotation='" + Vector3ToString(Rotation) + "' " +
                "scale='" + Vector3ToString(Scale) + "' />");
            xml.WriteLine();
        }

        private string Vector3ToString(Vector3 v)
        {
            return v.X + "," + v.Y + "," + v.Z;
        }

        private Vector3 StringToVector3(string s)
        {
            if (s == null) return Vector3.Zero;
            string[] values = s.Split(',');
            if (values.Length == 3)
            {
                float x = (float)Convert.ToDouble(values[0]);
                float y = (float)Convert.ToDouble(values[1]);
                float z = (float)Convert.ToDouble(values[2]);
                return new Vector3(x, y, z);
            }
            else
            {
                return Vector3.Zero;
            }
        }
    }
}
