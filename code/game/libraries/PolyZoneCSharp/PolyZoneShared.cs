using CitizenFX.Core;

namespace PolyZoneCSharp
{
    internal class PolyOptions
    {
        public string name;
        public Vector2[] points;
        public Vector3 center;
        public float size;
        public float max;
        public float min;
        public float area;
        public float? minZ;
        public float? maxZ;
        public bool useGrid;
        public bool lazyGrid;
        public int gridDivisions;
        public object debugColors;
        public bool debugPoly;
        public bool debugGrid;
        public object data;
        public bool isPolyZone;
    }

    internal class Poly
    {
       public string name;
       public Vector2[] points;
       public Vector3 center;
       public float size;
       public float max;
       public float min;
       public float area;
       public float? minZ;
       public float? maxZ;
       public bool useGrid;
       public bool lazyGrid;
       public int gridDivisions;
       public object debugColors;
       public bool debugPoly;
       public bool debugGrid;
       public object data;
       public bool isPolyZone;
    }

    internal class PolyZoneShared
    {
        public static Poly New(Vector2[] points, PolyOptions options)
        {
            if (points == null)
            {
                //print("[PolyZone] Error: Passed nil points table to PolyZone:Create() {name="..options.name.."}");
                return null;
            }
            if (points.Length < 3)
            {
               // print("[PolyZone] Warning: Passed points table with less than 3 points to PolyZone:Create() {name="..options.name.."}");
            }

            options = options != null ? options : new PolyOptions();
            var useGrid = options.useGrid;
            if (useGrid == null) { useGrid = true; }
            var lazyGrid = options.lazyGrid;
            if (lazyGrid == null) { lazyGrid = true; }
            var poly = new Poly()
            {
                name = string.IsNullOrEmpty(options.name) ? options.name : null,
                points = points,
                center = options.center,
                size = options.size,
                max = options.max,
                min = options.min,
                area = options.area,
                minZ = options.minZ != null ? options.minZ : null,
                maxZ = options.maxZ != null ? options.maxZ : null,
                useGrid = useGrid,
                lazyGrid = lazyGrid,
                gridDivisions = options.gridDivisions != null ? options.gridDivisions : 30,
                debugColors = options.debugColors != null ? options.debugColors : new { },
                debugPoly = options.debugPoly != null ? options.debugPoly : false,
                debugGrid = options.debugGrid != null ? options.debugGrid : false,
                data = options.data != null ? options.data : new { },
                isPolyZone = true,
            };
            if (poly.debugGrid) { poly.lazyGrid = false; }
            _calculatePoly(poly, options);
            setmetatable(poly, self);
            this.__index = self;
            return poly;
        }

        public static object Create(object points, object options)
        {
            var poly = New(points, options);
            _initDebug(poly, options);
            return poly;
        }

        public static object isPointInside(object point)
        {
            if (self.destroyed)
            {
                print("[PolyZone] Warning: Called isPointInside on destroyed zone {name="..self.name.."}");
                return false;
            }
            return _pointInPoly(point, self)
        }

        public static object destroy()
        {
            self.destroyed = true;
            if (self.debugPoly || self.debugGrid)
            {
                print("[PolyZone] Debug: Destroying zone {name="..self.name.."}");
            }
        }
    }
}