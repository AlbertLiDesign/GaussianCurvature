using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Rhino.Geometry;

namespace GaussianCurvature
{
    public class Libigl
    {
        private double[] V;
        private int[] F;

        private int VertexCount = 0;
        private int FaceCount = 0;

        public Libigl(Mesh mesh)
        {
            mesh.Faces.ConvertQuadsToTriangles();

            VertexCount = mesh.Vertices.Count;
            FaceCount = mesh.Faces.Count;

            V = new double[mesh.Vertices.Count * 3];
            F = new int[mesh.Faces.Count * 3];

            for (int i = 0; i < VertexCount; i++)
            {
                V[i] = mesh.Vertices[i].X;
                V[i + VertexCount] = mesh.Vertices[i].Y;
                V[i + VertexCount * 2] = mesh.Vertices[i].Z;
            }

            for (int i = 0; i < mesh.Faces.Count; i++)
            {
                F[i] = mesh.Faces[i].A;
                F[i + FaceCount] = mesh.Faces[i].B;
                F[i + FaceCount * 2] = mesh.Faces[i].C;
            }
        }

        public double[] GaussianCurvature()
        {
            double[] k = new double[VertexCount];
            ComputeGaussianCurvature(V, F, VertexCount, FaceCount, k);
            return k;
        }

        [DllImport("Wrapper.dll")]
        public static extern void ComputeGaussianCurvature(double[] V, int[] F, int VertexCount, int FaceCount, double[] K);
    }
}
