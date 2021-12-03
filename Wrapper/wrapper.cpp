#include "Wrapper.h"

void ComputeGaussianCurvature(double* v, int* f, int VertexCount, int FaceCount, double* k)
{
	Eigen::MatrixXd V(VertexCount, 3);
	Eigen::MatrixXi F(FaceCount, 3);

	for (int i = 0; i < VertexCount; i++)
	{
		V(i, 0) = v[i];
		V(i, 1) = v[i + VertexCount];
		V(i, 2) = v[i + VertexCount * 2];
	}

	for (int i = 0; i < FaceCount; i++)
	{
		F(i, 0) = f[i];
		F(i, 1) = f[i + FaceCount];
		F(i, 2) = f[i + FaceCount * 2];
	}

	Eigen::VectorXd K;
	// Compute integral of Gaussian curvature
	igl::gaussian_curvature(V, F, K);

	// Compute mass matrix
	Eigen::SparseMatrix<double> M, Minv;
	igl::massmatrix(V, F, igl::MASSMATRIX_TYPE_DEFAULT, M);
	igl::invert_diag(M, Minv);
	// Divide by area to get integral average
	K = (Minv * K).eval();

	for (size_t i = 0; i < VertexCount; i++)
	{
		k[i] = K.data()[i];
	}
}