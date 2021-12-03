#pragma once

#include <igl/gaussian_curvature.h>
#include <igl/massmatrix.h>
#include <igl/invert_diag.h>

extern "C" __declspec(dllexport) void ComputeGaussianCurvature(double* v, int* f, int VertexCount, int FaceCount, double* k);