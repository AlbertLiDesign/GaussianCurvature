using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Threading.Tasks;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace GaussianCurvature
{
    public class GaussianCurvatureComponent : GH_Component
    {
        public GaussianCurvatureComponent() : base("Gaussian Curvature", "Gaussian Curvature", "Compute the gaussian curvature of a given mesh.", "Example", "Discrete Geometry") { }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddMeshParameter("Mesh", "M", "Input a mesh model.", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("Curvature", "C", "Output the gaussian curvatures.", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Mesh mesh = new Mesh();
            if (!DA.GetData("Mesh", ref mesh)) return;

            Libigl igl = new Libigl(mesh);
            DA.SetDataList("Curvature", igl.GaussianCurvature());
        }
        protected override Bitmap Icon => null;
        public override Guid ComponentGuid { get; } = new Guid("{DA2D7C01-D45A-4AB5-9DAF-92531C1CA7D0}");
    }
}