using System;
using System.Drawing;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.ClassicOnline.MScreen;

namespace Proline.ClassicOnline.MRendering
{
    public static class MRenderingAPI
    {  

        public static void DrawDebugText3D(string text, Vector3 vector3, float scale2, int font)
        {
            var camCoords = API.GetGameplayCamCoords();
            var distance = API.Vdist2(camCoords.X, camCoords.Y, camCoords.Z, vector3.X, vector3.Y, vector3.Z);
            var scale = 1 / distance * scale2;
            var fov = 1 / API.GetGameplayCamFov() * 75;
            scale = scale * fov * 1f;
            float x = 0f;
            float y = 0f;

            var xx = API.World3dToScreen2d(vector3.X, vector3.Y, vector3.Z, ref x, ref y);
            var p = new PointF(x, y);//(x / 1280) * 1f, (y / 720) * 1f);

            if (p == PointF.Empty) return;
            ScreenAPI.DrawDebugText2D(text, p, scale, font);
        }

        public static void DrawEntityBoundingBox(int ent, int r, int g, int b, int a)
        {
            var box = GetEntityBoundingBox(ent);
            DrawBoundingBox(box, r, g, b, a);
        }


        public static void DrawBoundingBox(Vector3 start, Vector3 end, int r, int g, int b, int a)
        {
            var box = GetBoundingBox(start, end);
            DrawBoundingBox(box, r, g, b, a);
        }


        public static void DrawBoundingBoxFromPoints(Vector3[] points, int r, int g, int b, int a)
        {
            DrawBoundingBox(points, r, g, b, a);
        }


        public static void DrawBoundingPlaneFromPoints(Vector3[] points, int r, int g, int b, int a)
        {
            DrawBoundingPlane(points, r, g, b, a);
        }

        /// <summary>
        /// Gets the bounding box of the entity model in world coordinates, used by <see cref="DrawEntityBoundingBox(Entity, int, int, int, int)"/>.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal static Vector3[] GetBoundingBox(Vector3 start, Vector3 end, float heading = 0f)
        {
            Vector3 min = Vector3.Zero;
            Vector3 max = Vector3.Zero;

            //// API.GetModelDimensions((uint)// API.GetEntityModel(entity), ref min, ref max);
            var center = (start + end) / 2;
            min = center - start;
            max = center - end;

            var dx = center.X - end.X;
            var dy = center.Y - end.Y;
            //var heading = // API.GetHeadingFromVector_2d(dx, dy);
            //heading = // API.GetGameplayCamRelativeHeading();
            //const float pad = 0f;
            const float pad = 0.001f;

            var retval = new Vector3[8]
            {

                ConvertLocalToWorld(center, heading, new Vector3(min.X, min.Y, min.Z)),
                ConvertLocalToWorld(center, heading,new Vector3(max.X, min.Y, min.Z)),
                ConvertLocalToWorld(center, heading,new Vector3(max.X, max.Y, min.Z)),
                ConvertLocalToWorld(center, heading,new Vector3(min.X, max.Y, min.Z)),


                ConvertLocalToWorld(center, heading,new Vector3(min.X, min.Y, max.Z)),
                ConvertLocalToWorld(center, heading,new Vector3(max.X, min.Y, max.Z)),
                ConvertLocalToWorld(center, heading,new Vector3(max.X, max.Y, max.Z)),
                ConvertLocalToWorld(center, heading,new Vector3(min.X, max.Y, max.Z))
            };
            return retval;
        }

        public static Vector3 ConvertWorldToLocal(Vector3 origin, float originRotation, Vector3 Worldposition)
        {
            var radi = -Math.PI / 180 * originRotation;
            var newVector = Vector3.Subtract(Worldposition, origin);
            var rotation = Quaternion.RotationAxis(Vector3.ForwardLH, (float)radi);
            var rotatedVector = Vector3.Transform(newVector, rotation);
            return rotatedVector;
        }

        public static Vector3 ConvertLocalToWorld(Vector3 origin, float originRotation, Vector3 localPosition)
        {
            var radi = Math.PI / 180 * originRotation;
            var rotation = Quaternion.RotationAxis(Vector3.ForwardLH, (float)radi);
            var rotatedVector = Vector3.Transform(localPosition, rotation);
            var newVector = Vector3.Add(rotatedVector, origin);
            return newVector;
        }

        /// <summary>
        /// Gets the bounding box of the entity model in world coordinates, used by <see cref="DrawEntityBoundingBox(Entity, int, int, int, int)"/>.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal static Vector3[] GetEntityBoundingBox(int entity)
        {
            Vector3 min = Vector3.Zero;
            Vector3 max = Vector3.Zero;

            API.GetModelDimensions((uint)API.GetEntityModel(entity), ref min, ref max);
            //const float pad = 0f;
            const float pad = 0.001f;
            var retval = new Vector3[8]
            {
                 //Bottom
                 API.GetOffsetFromEntityInWorldCoords(entity, min.X - pad, min.Y - pad, min.Z - pad),
                 API.GetOffsetFromEntityInWorldCoords(entity, max.X + pad, min.Y - pad, min.Z - pad),
                 API.GetOffsetFromEntityInWorldCoords(entity, max.X + pad, max.Y + pad, min.Z - pad),
                 API.GetOffsetFromEntityInWorldCoords(entity, min.X - pad, max.Y + pad, min.Z - pad),

                 //Top
                 API.GetOffsetFromEntityInWorldCoords(entity, min.X - pad, min.Y - pad, max.Z + pad),
                 API.GetOffsetFromEntityInWorldCoords(entity, max.X + pad, min.Y - pad, max.Z + pad),
                 API.GetOffsetFromEntityInWorldCoords(entity, max.X + pad, max.Y + pad, max.Z + pad),
                 API.GetOffsetFromEntityInWorldCoords(entity, min.X - pad, max.Y + pad, max.Z + pad)
            };
            return retval;
        }

        /// <summary>
        /// Draws the edge poly faces and the edge lines for the specific box coordinates using the specified rgba color.
        /// </summary>
        /// <param name="box"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        private static void DrawBoundingBox(Vector3[] box, int r, int g, int b, int a)
        {
            var polyMatrix = GetBoundingBoxPolyMatrix(box);
            var edgeMatrix = GetBoundingBoxEdgeMatrix(box);

            DrawPolyMatrix(polyMatrix, r, g, b, a);
            DrawEdgeMatrix(edgeMatrix, 255, 255, 255, 255);
        }

        /// <summary>
        /// Draws the edge poly faces and the edge lines for the specific box coordinates using the specified rgba color.
        /// </summary>
        /// <param name="box"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        private static void DrawBoundingPlane(Vector3[] box, int r, int g, int b, int a)
        {
            var polyMatrix = GetBoundingPlanePolyMatrix(box);
            var edgeMatrix = GetBoundingPlaneEdgeMatrix(box);

            DrawPolyMatrix(polyMatrix, r, g, b, a);
            DrawEdgeMatrix(edgeMatrix, 255, 255, 255, 255);
        }

        /// <summary>
        /// Gets the coordinates for all poly box faces.
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        private static Vector3[][] GetBoundingBoxPolyMatrix(Vector3[] box)
        {
            return new Vector3[12][]
            {
                new Vector3[3] { box[2], box[1], box[0] },
                new Vector3[3] { box[3], box[2], box[0] },

                new Vector3[3] { box[4], box[5], box[6] },
                new Vector3[3] { box[4], box[6], box[7] },

                new Vector3[3] { box[2], box[3], box[6] },
                new Vector3[3] { box[7], box[6], box[3] },

                new Vector3[3] { box[0], box[1], box[4] },
                new Vector3[3] { box[5], box[4], box[1] },

                new Vector3[3] { box[1], box[2], box[5] },
                new Vector3[3] { box[2], box[6], box[5] },

                new Vector3[3] { box[4], box[7], box[3] },
                new Vector3[3] { box[4], box[3], box[0] }
            };
        }

        /// <summary>
        /// Gets the coordinates for all edge coordinates.
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        private static Vector3[][] GetBoundingBoxEdgeMatrix(Vector3[] box)
        {
            return new Vector3[12][]
            {
                new Vector3[2] { box[0], box[1] },
                new Vector3[2] { box[1], box[2] },
                new Vector3[2] { box[2], box[3] },
                new Vector3[2] { box[3], box[0] },

                new Vector3[2] { box[4], box[5] },
                new Vector3[2] { box[5], box[6] },
                new Vector3[2] { box[6], box[7] },
                new Vector3[2] { box[7], box[4] },

                new Vector3[2] { box[0], box[4] },
                new Vector3[2] { box[1], box[5] },
                new Vector3[2] { box[2], box[6] },
                new Vector3[2] { box[3], box[7] }
            };
        }


        /// <summary>
        /// Gets the coordinates for all poly box faces.
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        private static Vector3[][] GetBoundingPlanePolyMatrix(Vector3[] box)
        {
            return new Vector3[12][]
            {
                new Vector3[3] { box[2], box[1], box[0] },
                new Vector3[3] { box[3], box[2], box[0] },

                new Vector3[3] { box[4], box[5], box[6] },
                new Vector3[3] { box[4], box[6], box[7] },

                new Vector3[3] { box[2], box[3], box[6] },
                new Vector3[3] { box[7], box[6], box[3] },

                new Vector3[3] { box[0], box[1], box[4] },
                new Vector3[3] { box[5], box[4], box[1] },

                new Vector3[3] { box[1], box[2], box[5] },
                new Vector3[3] { box[2], box[6], box[5] },

                new Vector3[3] { box[4], box[7], box[3] },
                new Vector3[3] { box[4], box[3], box[0] }
            };
        }

        /// <summary>
        /// Gets the coordinates for all edge coordinates.
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        private static Vector3[][] GetBoundingPlaneEdgeMatrix(Vector3[] box)
        {
            return new Vector3[12][]
            {
                new Vector3[2] { box[0], box[1] },
                new Vector3[2] { box[1], box[2] },
                new Vector3[2] { box[2], box[3] },
                new Vector3[2] { box[3], box[0] },

                new Vector3[2] { box[4], box[5] },
                new Vector3[2] { box[5], box[6] },
                new Vector3[2] { box[6], box[7] },
                new Vector3[2] { box[7], box[4] },

                new Vector3[2] { box[0], box[4] },
                new Vector3[2] { box[1], box[5] },
                new Vector3[2] { box[2], box[6] },
                new Vector3[2] { box[3], box[7] }
            };
        }

        /// <summary>
        /// Draws the poly matrix faces.
        /// </summary>
        /// <param name="polyCollection"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        private static void DrawPolyMatrix(Vector3[][] polyCollection, int r, int g, int b, int a)
        {
            foreach (var poly in polyCollection)
            {
                float x1 = poly[0].X;
                float y1 = poly[0].Y;
                float z1 = poly[0].Z;

                float x2 = poly[1].X;
                float y2 = poly[1].Y;
                float z2 = poly[1].Z;

                float x3 = poly[2].X;
                float y3 = poly[2].Y;
                float z3 = poly[2].Z;
                // API.DrawPoly(x1, y1, z1, x2, y2, z2, x3, y3, z3, r, g, b, a);
            }
        }

        /// <summary>
        /// Draws the edge lines for the model dimensions.
        /// </summary>
        /// <param name="linesCollection"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        private static void DrawEdgeMatrix(Vector3[][] linesCollection, int r, int g, int b, int a)
        {
            foreach (var line in linesCollection)
            {
                float x1 = line[0].X;
                float y1 = line[0].Y;
                float z1 = line[0].Z;

                float x2 = line[1].X;
                float y2 = line[1].Y;
                float z2 = line[1].Z;

                // API.DrawLine(x1, y1, z1, x2, y2, z2, r, g, b, a);
            }
        } 
    }
}
